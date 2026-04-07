using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsByBookIdAsync(Guid bookId);

        Task<Review?> GetReviewByIdAsync(Guid id);

        Task<bool> AddReviewAsync(Review review);

        Task<bool> SoftDeleteReviewAsync(Review review);
    }
}
