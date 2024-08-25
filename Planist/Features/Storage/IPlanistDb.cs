using Planist.Interfaces;
using SQLite;

namespace Planist.Features.Storage
{
    public interface IPlanistDb
    {
        void InitializeDatabase();
        Task<int> InsertOrReplaceAsync<TEntity>(TEntity item) where TEntity : IEntity;
        Task<int> InsertAsync<TEntity>(TEntity item) where TEntity : IEntity;
        Task<int> UpdateAsync<TEntity>(TEntity item) where TEntity : IEntity;
        AsyncTableQuery<TEntity> TableAsync<TEntity>() where TEntity : IEntity, new();

        Task SoftDeleteAsync<TEntity>(AsyncTableQuery<TEntity> query) where TEntity : IEntity, new();
    }
}
