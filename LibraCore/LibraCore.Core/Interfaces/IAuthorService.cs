using LibraCore.Infrastructure.Data.Entities;
using LibraCore.ViewModels.Author;

namespace LibraCore.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAuthorsOrderedByNameAsync();
    }
}
