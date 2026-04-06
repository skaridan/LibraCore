using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static LibraCore.ApplicationConstants.RoleConstants;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    [Area(AdminRole)]
    [Authorize(Roles = AdminRole)]
    public abstract class BaseAdminController : Controller
    {
        public string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
