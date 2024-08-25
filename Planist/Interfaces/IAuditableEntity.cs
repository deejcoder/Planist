namespace Planist.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Version { get; set; }
    }
}
