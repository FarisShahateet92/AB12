using AB12.Application.Base.Common;
using AB12.Domain.Base.Schema;
using AB12.Services.Application;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AB12.Application.Products.Results
{
    public class ProductResult : SharedEntity, IImageEntity, IMapFrom<Product>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductResult>().ReverseMap();
        }
    }
}
