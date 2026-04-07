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
        public async Task<IActionResult> Index(string? searchTerm, int page = 1)
        {
            string userId = GetUserId()!;

            IEnumerable<BookIndexViewModel> books = await bookService
                .GetAllBooksOrderedByTitleAsync(userId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                books = books.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            int pageSize = 5;
            int totalBooks = books.Count();
            int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            if (page < 1)
            {
                page = 1;
            }

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;
            }

            var pagedBooks = books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedBooks);
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
