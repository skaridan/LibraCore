using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(Guid bookId)
        {
            IEnumerable<ReviewViewModel> reviews = await reviewService
                .GetAllReviewsByBookIdAsync(bookId);

            return View(reviews);
        }
    }
}
