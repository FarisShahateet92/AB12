using AB12.Application.Base.Common;
using AB12.Domain.Base.Common;

namespace AB12.Domain.Base.Schema
{
    public class Product : AuditableEntity, IImageEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
    }
}
