using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<UserBook>> GetAllUserBooksAsync(string userId); 
    }
}
