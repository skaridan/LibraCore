
using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author?> AuthorExistsAsync(string name);

        Task<bool> AddAuthorAsync(Author author);
    }
}
