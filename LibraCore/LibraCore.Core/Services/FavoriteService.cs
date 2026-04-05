using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Services.Interfaces;
using LibraCore.ViewModels;

namespace LibraCore.Services.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<FavoriteViewModel>> GetUserBooksOrderedByTitleAsync(string userId)
        {
            IEnumerable<UserBook> userBooks = await favoriteRepository
                .GetAllUserBooksAsync(userId);

            IEnumerable<FavoriteViewModel> favoriteViewModels = userBooks
                .Select(ub => new FavoriteViewModel
                {
                    Id = ub.BookId,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author.Name,
                    Price = ub.Book.Price,
                    ImageUrl = ub.Book.ImageUrl
                })
                .OrderBy(f => f.Title)
                .ToArray();

            return favoriteViewModels;
        }

        public async Task AddToFavoritesAsync(string userId, Guid bookId)
        {
            Guid userGuid = Guid.Parse(userId);

            await favoriteRepository.AddUserBookAsync(userGuid, bookId);
        }
    }
}
