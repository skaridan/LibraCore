using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<bool> AddBookAsync(Book book);
    }
}
