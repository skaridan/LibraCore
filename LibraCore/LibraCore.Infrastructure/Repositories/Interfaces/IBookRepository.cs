using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<Book?> GetBookByIdAsync(Guid id);

        Task<bool> AddBookAsync(Book book);

        Task<bool> EditBookAsync(Book book);

        Task<bool> SoftDeleteBookAsync(Book book);
    }
}
