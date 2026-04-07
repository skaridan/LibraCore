using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraCore.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        public string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
