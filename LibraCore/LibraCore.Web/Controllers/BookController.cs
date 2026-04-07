using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId()!;

            IEnumerable<BookIndexViewModel> bookIndexViewModels = await bookService
                .GetAllBooksOrderedByTitleAsync(userId);

            return View(bookIndexViewModels);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            BookDetailsViewModel? book = await bookService
                .GetBookDetailsByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
