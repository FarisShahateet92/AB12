using AB12.Application.Orders.Results;
using AB12.Domain.Base.Schema;
using AB12.Infrastructure.Components;
using AutoMapper;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace AB12.Services.Components;

[ScopedService]
public class OrderService
{
    private readonly IMapper _mapper;
    private readonly OrderRepo _repo;

    public OrderService(IMapper mapper, OrderRepo repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<OrderResult> Create(OrderResult order)
    {
        var entity = _mapper.Map<Order>(order);
        var result = await _repo.CreateAsync(entity);
        return _mapper.Map<OrderResult>(result);
    }

    public async Task<List<OrderResult>> GetAllAsync(bool includeSoftDeleted = false)
    {
        var orders = await _repo.GetAllAsync(includeSoftDeleted);
        return _mapper.Map<List<OrderResult>>(orders);
    }

    public async Task<List<OrderResult>> GetAllWithPagingAsync(int pageNumber, int pageSize, bool includeSoftDeleted = false)
    {
        var orders = await _repo.GetAllWithPagingAsync(pageNumber, pageSize, includeSoftDeleted);
        return _mapper.Map<List<OrderResult>>(orders);
    }

    public async Task<OrderResult> GetByIdAsync(string id)
    {
        var order = await _repo.GetByIdAsync(id);
        return _mapper.Map<OrderResult>(order);
    }

    public async Task<OrderResult> Update(OrderResult entity)
    {
        var order = _mapper.Map<Order>(entity);
        var result = await _repo.UpdateAsync(order);
        return _mapper.Map<OrderResult>(result);
    }

    public async Task<bool> DeleteById(string id, bool softDelete = false)
    {
        bool deleteResult = false;

        var order = await _repo.GetByIdAsync(id, true);

        if (softDelete)
            deleteResult = await _repo.SoftDeleteAsync(order);
        else
            deleteResult = await _repo.DeleteAsync(order);

        return deleteResult;
    }
}
