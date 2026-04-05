using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<UserBook>> GetAllUserBooksAsync(string userId);

        Task AddUserBookAsync(Guid userId, Guid bookId);

        Task<bool> IsBookFavoriteAsync(Guid userId, Guid bookId);
    }
}
