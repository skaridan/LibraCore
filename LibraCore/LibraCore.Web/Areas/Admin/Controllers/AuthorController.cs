using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Author;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class AuthorController : BaseAdminController
    {
        private readonly IAuthorService authorService;
        private readonly ILogger<AuthorController> logger;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            this.authorService = authorService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AuthorViewModel> authors = await authorService
                .GetAllAuthorsOrderedByNameAsync();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await authorService.AddAuthorAsync(model);

                TempData["Success"] = AddAuthorSuccessMessage;

                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException eaee)
            {
                logger.LogError(eaee, string.Format(AuthorAlreadyExistsMessage, model.Name));

                TempData["Error"] = string.Format(AuthorAlreadyExistsMessage, model.Name);

                return View(model);
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, AddAuthorFailureMessage);

                TempData["Error"] = AddAuthorFailureMessage;

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            AuthorViewModel? model = await authorService.GetAuthorByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, AuthorViewModel model)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await authorService.SoftDeleteAuthorAsync(id);

                TempData["Success"] = DeleteAuthorSuccessMessage;

                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, DeleteAuthorFailureMessage);

                TempData["Error"] = DeleteAuthorFailureMessage;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
