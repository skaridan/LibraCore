using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Author;

namespace LibraCore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsOrderedByNameAsync()
        {
            IEnumerable<Author> allAuthors = await authorRepository
                .GetAllAuthorsAsync();

            IEnumerable<AuthorViewModel> authorViewModels = allAuthors
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .OrderBy(a => a.Name)
                .ToArray();

            return authorViewModels;
        }
    }
}
