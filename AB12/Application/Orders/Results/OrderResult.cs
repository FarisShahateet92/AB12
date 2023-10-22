using AB12.Application.Base.Common;
using AB12.Domain.Base.Schema;
using AB12.Services.Application;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AB12.Application.Orders.Results;

public class OrderResult : SharedEntity, IMapFrom<Order>
{
    [Required(AllowEmptyStrings = false)]
    public string ClientName { get; set; }
    public OrderStatus Status { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderResult>().ReverseMap();
    }
}
