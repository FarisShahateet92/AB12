using AB12.Application.Base.Common;
using AB12.Domain.Base.Schema;
using AB12.Services.Application;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AB12.Application.OrderItems.Results;

public class OrderItemResult : SharedEntity, IMapFrom<OrderItem>
{
    [Required(AllowEmptyStrings = false)]
    public string ProductID { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string OrderID { get; set; }

    public int Quantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderItem, OrderItemResult>().ReverseMap();
    }
}
