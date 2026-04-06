using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<UserBook>> GetAllUserBooksAsync(string userId);

        Task<UserBook?> GetUserBookAsync(string userId, Guid bookId);

        Task<bool> AddUserBookAsync(UserBook userBook);

        Task<bool> SoftDeleteUserBookAsync(UserBook userBook);

        Task<bool> UpdateUserBookAsync(UserBook userBook);
    }
}
