using LibraCore.ViewModels.Book;

namespace LibraCore.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<BookGenreViewModel>> FetchGenresAsync();
    }
}
