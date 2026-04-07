using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Review;

namespace LibraCore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsByBookIdAsync(Guid bookId)
        {
            IEnumerable<Review> reviews = await reviewRepository
                .GetAllReviewsByBookIdAsync(bookId);

            IEnumerable<ReviewViewModel> reviewViewModels = reviews
                 .Select(r => new ReviewViewModel
                 {
                     Id = r.Id,
                     BookId = r.BookId,
                     BookTitle = r.Book.Title,
                     UserName = r.User.UserName!,
                     Rating = r.Rating,
                     Comment = r.Comment
                 })
                 .ToArray();

            return reviewViewModels;
        }
    }
}
