using LibraCore.GCommon.Exceptions;
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

        public async Task AddReviewAsync(ReviewInputModel model, string userId)
        {
            Review newReview = new Review
            {
                BookId = model.BookId,
                Rating = model.Rating,
                Comment = model.Comment,
                UserId = Guid.Parse(userId)
            };

            bool success = await reviewRepository.AddReviewAsync(newReview);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
