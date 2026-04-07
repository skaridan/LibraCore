using LibraCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraCore.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 404)
            {
                return View("NotFound");
            }

            if(statusCode == 500 || statusCode == 400)
            {
                return View("InternalError");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
