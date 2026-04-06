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

                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException eaee)
            {
                logger.LogError(eaee, string.Format(AuthorAlreadyExistsMessage, model.Name));
                ModelState.AddModelError(string.Empty, string.Format(AuthorAlreadyExistsMessage, model.Name));
                return View(model);
            }
            catch (EntityPersistFailureException epfe)
            {
                logger.LogError(epfe, AddAuthorFailureMessage);
                ModelState.AddModelError(string.Empty, AddAuthorFailureMessage);
                return View(model);
            }
        }
    }
}
