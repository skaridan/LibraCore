using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class ReviewController : BaseAdminController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            ReviewViewModel? review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, ReviewViewModel model)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await reviewService.SoftDeleteReviewAsync(id);

                return RedirectToAction("Index", "Review", new { area = "", bookId = model.BookId });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, "Failed to delete review.");
                return RedirectToAction("Index", "Review", new { area = "", bookId = model.BookId });
            }
        }
    }
}
