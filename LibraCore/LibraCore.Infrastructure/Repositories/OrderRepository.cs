using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(LibraCoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
        {
            return await DbContext
                .Orders
                .Where(o => o.UserId == userId)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await DbContext
                .Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            await DbContext.Orders.AddAsync(order);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
