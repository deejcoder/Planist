using SQLite;

namespace Planist.Features.Storage.Entities
{
    public class TodoList : BaseAuditableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;        
    }
}
