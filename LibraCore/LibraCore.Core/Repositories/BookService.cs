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

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksOrderedByTitleAsync()
        {
            IEnumerable<Book> allBooks = await bookRepository.GetAllBooksAsync();

            IEnumerable<BookIndexViewModel> bookIndexViewModels = allBooks
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl ?? DefaultImageUrl
                })
                .OrderBy(b => b.Title)
                .ToArray();

            return bookIndexViewModels;
        }
    }
}
