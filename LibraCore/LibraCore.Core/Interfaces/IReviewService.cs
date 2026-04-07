using LibraCore.ViewModels.Review;

namespace LibraCore.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllReviewsByBookIdAsync(Guid bookId);

        Task AddReviewAsync(ReviewInputModel model, string userId);
    }
}
