using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Book;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class BookController : BaseAdminController
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

                TempData["Success"] = AddBookSuccessMessage;

                return RedirectToAction("Index", "Book", new { area = "" });
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Add)));

                TempData["Error"] = string.Format(CrudBookFailureMessage, nameof(Add));

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);

                TempData["Error"] = GeneralError;

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
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

                TempData["Success"] = EditBookSuccessMessage;

                return RedirectToAction("Details", "Book", new { area = "", id });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Edit)));

                TempData["Error"] = PersistFailureMessage;

                formModel.Genres = (await genreService.FetchGenresAsync()).ToArray();
                return View(formModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);

                TempData["Error"] = GeneralError;

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
        public async Task<IActionResult> Delete(Guid id, BookDetailsViewModel? viewModel)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await bookService.SoftDeleteBookAsync(id);

                TempData["Success"] = DeleteBookSuccessMessage;

                return RedirectToAction("Index", "Book", new { area = "" });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, string.Format(CrudBookFailureMessage, nameof(Delete)));
                
                TempData["Error"] = PersistFailureMessage;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, GeneralError);

                TempData["Error"] = GeneralError;
            }

            return RedirectToAction("Index", "Book", new { area = "" });
        }
    }
}
