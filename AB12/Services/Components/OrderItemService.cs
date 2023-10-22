using AB12.Application.OrderItems.Results;
using AB12.Application.Orders.Results;
using AB12.Domain.Base.Schema;
using AB12.Infrastructure.Components;
using AutoMapper;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace AB12.Services.Components;

[ScopedService]
public class OrderItemService
{
    private readonly IMapper _mapper;
    private readonly OrderItemRepo _repo;

    public OrderItemService(IMapper mapper, OrderItemRepo repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<OrderItemResult> Create(OrderItem entity)
    {
        var orderItem = await _repo.CreateAsync(entity);
        return _mapper.Map<OrderItemResult>(orderItem);
    }
}
