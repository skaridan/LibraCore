using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class FavoriteRepository : BaseRepository, IFavoriteRepository
    {
        public FavoriteRepository(LibraCoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserBook>> GetAllUserBooksAsync(string userId)
        {
            return await DbContext
                .UsersBooks
                .AsNoTracking()
                .Where(ub => ub.UserId.ToString().ToLower() == userId.ToLower())
                .Include(ub => ub.Book)
                .ThenInclude(ub => ub.Author)
                .ToArrayAsync();
        }
    }
}
