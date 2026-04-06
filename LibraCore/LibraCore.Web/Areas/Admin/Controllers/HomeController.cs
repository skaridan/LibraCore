using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
