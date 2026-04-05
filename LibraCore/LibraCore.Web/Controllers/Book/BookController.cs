using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Services.Interfaces;
using LibraCore.Services.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Book;

namespace LibraCore.Web.Controllers.Book
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;

        private readonly ILogger<BookController> logger;

        public BookController(IBookService bookService, IGenreService genreService, ILogger<BookController> logger)
        {
            this.bookService = bookService;
            this.genreService = genreService;

            this.logger = logger;
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            BookInputFormModel formModel = new BookInputFormModel()
            {
                Genres = (await genreService.FetchGenresAsync()).ToArray()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookInputFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }

            try
            {
                await bookService.AddBookAsync(formModel);

                return RedirectToAction(nameof(Index));
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Add)));
                ModelState.AddModelError(string.Empty, string.Format(CrudBookFailureMessage, nameof(Add)));

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);
                ModelState.AddModelError(string.Empty, GeneralError);

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            BookInputFormModel? formModel = await bookService
                .GetFormModelByIdAsync(id);
            if (formModel == null)
            {
                return NotFound();
            }

            formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookInputFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }

            try
            {
                await bookService.EditBookAsync(id, formModel);

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Edit)));
                ModelState.AddModelError(string.Empty, PersistFailureMessage);

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);
                ModelState.AddModelError(string.Empty, GeneralError);

                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            BookDetailsViewModel? viewModel = await bookService
                .GetBookDetailsByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, BookDetailsViewModel? viewModel)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await bookService.SoftDeleteBookAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Delete)));
                ModelState.AddModelError(string.Empty, PersistFailureMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);

                return RedirectToAction(nameof(Details), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
