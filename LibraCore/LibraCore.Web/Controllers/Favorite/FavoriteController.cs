using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Services.Services;
using LibraCore.Services.Services.Interfaces;
using LibraCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers.Favorite
{
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
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
        public async Task<IActionResult> Add(Guid id)
        {
            string userId = GetUserId()!;

            await favoriteService.AddToFavoritesAsync(userId, id);

            return RedirectToAction(nameof(Index), nameof(Book));
        }
    }
}
