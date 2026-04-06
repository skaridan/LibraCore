using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Services.Services;
using LibraCore.Services.Services.Interfaces;
using LibraCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Favorite;

namespace LibraCore.Web.Controllers
{
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService favoriteService;
        private readonly ILogger<FavoriteController> logger;

        public FavoriteController(IFavoriteService favoriteService, ILogger<FavoriteController> logger)
        {
            this.favoriteService = favoriteService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId()!;

            IEnumerable<FavoriteViewModel> favoriteViewModels = await favoriteService
                .GetUserBooksOrderedByTitleAsync(userId);

            return View(favoriteViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute(Name = "id")] Guid bookId)
        {
            string userId = GetUserId()!;

            try
            {
                await favoriteService.AddToFavoritesAsync(userId, bookId);
            }
            catch (EntityAlreadyExistsException eaee)
            {
                logger.LogError(eaee, string.Format(BookAlreadyInFavoritesMessage, bookId, userId));

                return BadRequest();
            }
            catch (EntityNotFoundException enfe)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(AddToFavoritesFailureMessage));

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            return View();
        }
    }
}
