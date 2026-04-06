
using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Infrastructure.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author?> AuthorExistsByNameAsync(string name);

        Task<Author?> GetAuthorByIdAsync(Guid id);

        Task<bool> AddAuthorAsync(Author author);

        Task<bool> SoftDeleteAuthorAsync(Author author);
    }
}
