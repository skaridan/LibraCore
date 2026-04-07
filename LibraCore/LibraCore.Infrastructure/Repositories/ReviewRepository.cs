using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(LibraCoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByBookIdAsync(Guid bookId)
        {
            return await DbContext
                .Reviews
                .Where(r => r.BookId == bookId && r.IsDeleted == false)
                .Include(r => r.User)
                .Include(r => r.Book)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(Guid id)
        {
            return await DbContext
                .Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .SingleOrDefaultAsync(r => r.Id == id && r.IsDeleted == false);
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            await DbContext.Reviews.AddAsync(review);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteReviewAsync(Review review)
        {
            review.IsDeleted = true;

            DbContext.Reviews.Update(review);

            int resultCount = await SaveChangesAsync();
            return resultCount == 1;
        }
    }
}
