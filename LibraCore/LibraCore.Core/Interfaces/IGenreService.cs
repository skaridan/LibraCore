using LibraCore.Services.ViewModels.Book;

namespace LibraCore.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<BookGenreViewModel>> FetchGenresAsync();
    }
}
