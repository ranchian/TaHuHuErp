using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Model.Dto;
using THH.Service;
using THH.Web.BaseApplication;

namespace THH.Web.Areas.System.Controllers
{
    public class RoleController : ServicedController<RoleService>
    {
        // GET: System/Role
        public ActionResult Index()
        {
            return View();
        }
        #region 列表(Grid)
        /// <summary>
        /// 采购订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRoleGrid(int limit = 0, int offset = 0)
        {
            List<RoleDto> roleDtoDtos = Service.GetRoleGrid(limit, offset);
            return Json(new { total = roleDtoDtos.Count, rows = roleDtoDtos }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}