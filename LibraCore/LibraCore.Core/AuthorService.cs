using LibraCore.GCommon.Exceptions;
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

        public async Task AddAuthorAsync(AuthorInputModel model)
        {
            Author? authorExists = await authorRepository.AuthorExistsByNameAsync(model.Name);
            if (authorExists != null)
            {
                throw new EntityAlreadyExistsException();
            }

            Author author = new Author
            {
                Name = model.Name
            };

            bool success = await authorRepository.AddAuthorAsync(author);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task SoftDeleteAuthorAsync(Guid id)
        {
            Author? author = await authorRepository
                .GetAuthorByIdAsync(id);
            if (author == null)
            {
                throw new EntityNotFoundException();
            }

            bool success = await authorRepository.SoftDeleteAuthorAsync(author);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task<AuthorViewModel?> GetAuthorByIdAsync(Guid id)
        {
            Author? author = await authorRepository
                .GetAuthorByIdAsync(id);
            if (author == null)
            {
                return null;
            }

            AuthorViewModel authorViewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name
            };

            return authorViewModel;
        }
    }
}
