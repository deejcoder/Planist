using Planist.Interfaces;
using SQLite;
using System.Reflection;

namespace Planist.Features.Storage
{
    public class PlanistDb : IPlanistDb
    {
        private const string DatabaseFileName = "planist.db3";
        private static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
        private const SQLite.SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        private SQLiteAsyncConnection ConnectionAsync { get; }        

        public PlanistDb()
        {
            // create the connection
            ConnectionAsync = new SQLiteAsyncConnection(DatabasePath, Flags);
        }

        public void InitializeDatabase()
        {
            CreateTables();
        }

        private void CreateTables()
        {
            if (ConnectionAsync == null) return;

            Type entityType = typeof(IEntity);

            Assembly assembly = Assembly.GetExecutingAssembly();

            // get database tables that need to be created based on classes that implement the IEntity interface
            List<Type> tablesToCreate = assembly.GetTypes()
                .Where(t => entityType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (Type type in tablesToCreate)
            {
                ConnectionAsync.CreateTableAsync(type);
            }
        }

        #region Insert / Update

        public async Task<int> InsertOrReplaceAsync<TEntity>(TEntity item)
            where TEntity : IEntity
        {
            CheckAndUpdateAuditableFields(item);

            return await ConnectionAsync.InsertOrReplaceAsync(item);
        }     
        
        public async Task<int> InsertAsync<TEntity>(TEntity item)
            where TEntity : IEntity
        {
            CheckAndUpdateAuditableFields(item);

            return await ConnectionAsync.InsertAsync(item);
        }

        public async Task<int> UpdateAsync<TEntity>(TEntity item)
            where TEntity : IEntity
        {
            CheckAndUpdateAuditableFields(item);

            return await ConnectionAsync.UpdateAsync(item);
        }

        #endregion

        #region Query

        public AsyncTableQuery<TEntity> TableAsync<TEntity>()
            where TEntity : IEntity, new()
        {
            return ConnectionAsync.Table<TEntity>();
        }

        #endregion

        #region Deletion

        /// <summary>
        /// Updates the deleted date of all items that match the provided query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task SoftDeleteAsync<TEntity>(AsyncTableQuery<TEntity> query)
            where TEntity : IEntity, new()
        {
            List<TEntity> itemsToDelete = await query.ToListAsync();            

            foreach(TEntity item in itemsToDelete)
            {
                if(item is IAuditableEntity auditable)
                {
                    auditable.DeletedDate = DateTime.Now;
                }
            }

            await ConnectionAsync.UpdateAllAsync(itemsToDelete);
        }

        #endregion

        /// <summary>
        /// Checks if the entity is auditable, if it is, updates the required members
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckAndUpdateAuditableFields(IEntity entity)            
        {
            if (entity is IAuditableEntity auditable)
            {
                auditable.ModifiedDate = DateTime.Now;
                auditable.Version++;

                return true;
            }

            return false;
        }
    }
}
