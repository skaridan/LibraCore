using LibraCore.ViewModels;

namespace LibraCore.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteViewModel>> GetUserBooksOrderedByTitleAsync(string userId);

        Task AddToFavoritesAsync(string userId, Guid bookId);

        Task RemoveBookFromFavoritesAsync(string userId, Guid bookId);
    }
}
