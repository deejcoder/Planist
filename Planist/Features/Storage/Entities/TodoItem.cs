using SQLite;

namespace Planist.Features.Storage.Entities
{
    public class TodoItem : BaseAuditableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TodoListId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
