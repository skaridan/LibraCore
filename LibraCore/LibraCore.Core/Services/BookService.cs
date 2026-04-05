using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Services.Interfaces;
using LibraCore.Services.ViewModels.Book;

using static LibraCore.GCommon.ApplicationConstants;

namespace LibraCore.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IFavoriteRepository favoriteRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IFavoriteRepository favoriteRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync(string? userId)
        {
            IEnumerable<Book> allBooks = await bookRepository.GetAllBooksAsync();

            IEnumerable<UserBook> userFavorites = new List<UserBook>();

            if (userId != null)
            {
                userFavorites = await favoriteRepository.GetAllUserBooksAsync(userId);
            }



            IEnumerable<BookIndexViewModel> bookIndexViewModels = allBooks
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl ?? DefaultImageUrl,
                    IsFavorite = userFavorites.Any(f => f.BookId == b.Id)

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

        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(Guid id)
        {
            Book? book = await bookRepository
                .GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }

            BookDetailsViewModel model = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Genre = book.Genre.Name,
                Description = book.Description,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate.ToString(DateFormat),
                ImageUrl = book.ImageUrl ?? DefaultImageUrl
            };

            return model;
        }

        public async Task EditBookAsync(Guid id, BookInputFormModel formModel)
        {
            Book? bookDb = await bookRepository.GetBookByIdAsync(id);

            if (bookDb == null)
            {
                throw new EntityNotFoundException();
            }

            Author? author = await authorRepository
                .AuthorExistsAsync(formModel.Author);

            if (author == null)
            {
                author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = formModel.Author
                };

                await authorRepository.AddAuthorAsync(author);
            }

            bookDb.Title = formModel.Title;
            bookDb.Description = formModel.Description;
            bookDb.Price = formModel.Price;
            bookDb.ImageUrl = formModel.ImageUrl;
            bookDb.GenreId = formModel.GenreId;
            bookDb.AuthorId = author.Id;
            bookDb.ReleaseDate = DateOnly.FromDateTime(formModel.ReleaseDate);

            bool editSuccess = await bookRepository.EditBookAsync(bookDb);
            if (!editSuccess)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task<BookInputFormModel?> GetFormModelByIdAsync(Guid id)
        {
            Book? book = await bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return null;
            }

            return new BookInputFormModel
            {
                Title = book.Title,
                Author = book.Author.Name,
                Description = book.Description,
                GenreId = book.GenreId,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate.ToDateTime(TimeOnly.MinValue),
                ImageUrl = book.ImageUrl
            };
        }
        public async Task SoftDeleteBookAsync(Guid id)
        {
            Book? book = await bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new EntityNotFoundException();
            }

            bool success = await bookRepository.SoftDeleteBookAsync(book);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
