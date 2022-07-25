using PropertyApp.Domain.Entities;

namespace PropertyApp.Domain.Common
{
    public class AuditableEntity
    {
        public virtual User? CreatedBy { get; set; }
        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
