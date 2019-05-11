using System.Web.Mvc;
using THH.Web.Authorization;

namespace THH.Web.Controllers
{
    [SysAuthorize]
    public class BaseController : Controller
    {
    }
}