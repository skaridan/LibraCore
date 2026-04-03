using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
    }
}
