using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Review;

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

            BookReviewsViewModel model = new BookReviewsViewModel
            {
                BookId = bookId,
                Reviews = reviews
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(Guid bookId)
        {
            ReviewInputModel model = new ReviewInputModel
            {
                BookId = bookId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid bookId, ReviewInputModel model)
        {
            model.BookId = bookId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string userId = GetUserId()!;

                await reviewService.AddReviewAsync(model, userId);

                return RedirectToAction(nameof(Index), new { bookId = model.BookId });
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, AddReviewFailureMessage);

                ModelState.AddModelError(string.Empty, AddReviewFailureMessage);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);

                ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

                return View(model);
            }
        }
    }
}
