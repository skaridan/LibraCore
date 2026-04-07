using LibraCore.ViewModels.Review;

namespace LibraCore.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllReviewsByBookIdAsync(Guid bookId);

        Task<ReviewViewModel?> GetReviewByIdAsync(Guid id);

        Task AddReviewAsync(ReviewInputModel model, string userId);

        Task SoftDeleteReviewAsync(Guid id);
    }
}
