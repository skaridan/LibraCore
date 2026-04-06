using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AuthorViewModel> authorViewModels = await authorService
                .GetAllAuthorsOrderedByNameAsync();

            return View(authorViewModels);
        }
    }
}
