using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.ApplicationConstants.RoleConstants;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    [Area(AdminRole)]
    [Authorize(Roles = AdminRole)]
    public abstract class BaseController : Controller
    {

    }
}
