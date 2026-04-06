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
            Guid userGuid = Guid.Parse(userId);

            return await DbContext
                .UsersBooks
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(ub => ub.UserId == userGuid && ub.IsDeleted == false)
                .Include(ub => ub.Book)
                .ThenInclude(ub => ub.Author)
                .ToArrayAsync();
        }

        public async Task<bool> AddUserBookAsync(UserBook userBook)
        {
            DbContext.UsersBooks.Add(userBook);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
        public async Task<bool> SoftDeleteUserBookAsync(UserBook userBook)
        {
            userBook.IsDeleted = true;

            DbContext.UsersBooks.Update(userBook);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<UserBook?> GetUserBookAsync(string userId, Guid bookId)
        {
            Guid userGuid = Guid.Parse(userId);

            UserBook? userBook = await DbContext
                 .UsersBooks
                 .IgnoreQueryFilters()
                 .SingleOrDefaultAsync(ub => ub.UserId == userGuid &&
                 ub.BookId == bookId);

            return userBook;
        }

        public async Task<bool> UpdateUserBookAsync(UserBook userBook)
        {
            DbContext.UsersBooks.Update(userBook);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
