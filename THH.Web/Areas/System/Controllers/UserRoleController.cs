using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Service;
using THH.Web.BaseApplication;

namespace THH.Web.Areas.System.Controllers
{
    public class UserRoleController : ServicedController<UserRoleService>
    {
        // GET: System/UserRole
        public ActionResult Index()
        {
            return View();
        }
    }
}