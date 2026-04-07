using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserViewModel> users = await userService.GetAllUsersAsync();

            return View(users);
        }
    }
}
