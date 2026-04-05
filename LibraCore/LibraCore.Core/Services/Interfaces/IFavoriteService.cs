using LibraCore.ViewModels;

namespace LibraCore.Services.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteViewModel>> GetUserBooksOrderedByTitleAsync(string userId);
    }
}
