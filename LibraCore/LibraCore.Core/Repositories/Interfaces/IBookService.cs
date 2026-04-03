using LibraCore.Services.ViewModels.Book;

namespace LibraCore.Services.Repositories.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync();

        Task AddBookAsync(BookInputFormModel bookInputFormModel);

        Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(Guid id);
    }
}
