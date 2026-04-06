using LibraCore.Services.ViewModels.Book;

namespace LibraCore.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync(string? userId);

        Task AddBookAsync(BookInputFormModel bookInputFormModel);

        Task EditBookAsync(Guid id, BookInputFormModel formModel);

        Task SoftDeleteBookAsync(Guid id);

        Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(Guid id);

        Task<BookInputFormModel?> GetFormModelByIdAsync(Guid id);

    }
}
