using AB12.Application.Base.Common;

namespace AB12.Domain.Base.Common
{
    public abstract class AuditableEntity : SharedEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
