using LibraCore.Services.Repositories.Interfaces;
using LibraCore.Services.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers.Book
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> bookIndexViewModels = await bookService
                .GetAllBooksOrderedByTitleAsync();

            return View(bookIndexViewModels);
        }
    }
}
