using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class ReviewController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
