using LibraCore.Services.ViewModels.Book;

namespace LibraCore.Services.Repositories.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync();

        Task AddBookAsync(BookInputFormModel bookInputFormModel);

        Task EditBookAsync(Guid id, BookInputFormModel formModel);

        Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(Guid id);

        Task<BookInputFormModel?> GetFormModelByIdAsync(Guid id);

    }
}
