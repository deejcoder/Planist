using Planist.Interfaces;

namespace Planist.Features.Storage.Entities
{
    public abstract class BaseAuditableEntity : IAuditableEntity
    {
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int Version { get; set; }
    }
}
