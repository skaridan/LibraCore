using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
