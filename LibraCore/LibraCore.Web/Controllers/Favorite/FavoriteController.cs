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
    }
}
