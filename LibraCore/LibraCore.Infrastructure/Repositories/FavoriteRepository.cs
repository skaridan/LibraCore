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

        public async Task AddUserBookAsync(Guid userId, Guid bookId)
        {
            bool alreadyExists = await DbContext
                .UsersBooks
                .AnyAsync(ub => ub.UserId == userId && ub.BookId == bookId);

            if (!alreadyExists)
            {
                UserBook userBook = new UserBook
                {
                    UserId = userId,
                    BookId = bookId
                };

                await DbContext.UsersBooks.AddAsync(userBook);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> IsBookFavoriteAsync(Guid userId, Guid bookId)
        {
            return await DbContext
                .UsersBooks
                .AnyAsync(ub => ub.UserId == userId && ub.BookId == bookId);
        }
    }
}
