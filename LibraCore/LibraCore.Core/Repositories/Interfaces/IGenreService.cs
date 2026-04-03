using LibraCore.Services.ViewModels.Book;

namespace LibraCore.Services.Repositories.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<BookGenreViewModel>> FetchGenresAsync();
    }
}
