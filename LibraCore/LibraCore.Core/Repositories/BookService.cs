using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Repositories.Interfaces;
using LibraCore.Services.ViewModels.Book;

using static LibraCore.GCommon.ApplicationConstants;

namespace LibraCore.Services.Repositories
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync()
        {
            IEnumerable<Book> allBooks = await bookRepository.GetAllBooksAsync();

            IEnumerable<BookIndexViewModel> bookIndexViewModels = allBooks
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl ?? DefaultImageUrl
                })
                .OrderBy(b => b.Title)
                .ToArray();

            return bookIndexViewModels;
        }

        public async Task AddBookAsync(BookInputFormModel model)
        {
            Author? author = await authorRepository
                .AuthorExistsAsync(model.Author);
            if (author == null)
            {
                author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = model.Author
                };

                await authorRepository.AddAuthorAsync(author);
            }

            Book newBook = new Book
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Description = model.Description,
                Author = author,
                Price = model.Price,
                ReleaseDate = DateOnly.FromDateTime(model.ReleaseDate),
                ImageUrl = model.ImageUrl
            };

            bool success = await bookRepository.AddBookAsync(newBook);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
