using AB12.Domain.Base.Schema;
using AB12.Domain.Persistence;
using AB12.Infrastructure.Components.Common;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace AB12.Infrastructure.Components
{
    [ScopedService]
    public class OrderItemRepo : BaseRepo<OrderItem>
    {
        private readonly AppDbContext _context;
        public OrderItemRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderItem>> GetByOrderIdAsync(string orderId)
        {
            var query = _context.Set<OrderItem>().Where(x => x.OrderID == orderId);
            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<OrderItem> GetByProductIdAsync(string productId)
        {
            var query = _context.Set<OrderItem>().Where(x => x.ProductID == productId);
            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

    }
}
