using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.Dto;
using THH.Service;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.System.Controllers
{
    public class MenuController : ServicedController<SysMenuService>
    {
        // GET: System/Menu
        public ActionResult Index()
        {
            return View();
        }
        #region 列表(Grid)
        /// <summary>
        /// 菜单订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMenuGrid(int limit = 0, int offset = 0, string sort = "", string sortOrder = "")
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<Model.Dto.MenuTree> menuDtoDtos = Service.GetMenuGrid(gp);
            return Json(new { total = gp.TotalCount, rows = menuDtoDtos }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Model.Dto.MenuTree menuDto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (ModelState.IsValid)
            {
                menuDto.CreateUser = ServiceHelper.GetCurrentUser().LoginName;
                menuDto.CredateTime = DateTime.Now.Date;
                if (Service.Add(menuDto))
                {
                    return this.Ajax_JGClose();
                }
                else
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "添加失败！";
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult Delete(string ids)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            try
            {
                if (!Service.Deletes(ids))
                {
                    resultJsonInfo.Success = false;
                }
            }
            catch (Exception ex)
            {
                resultJsonInfo.Success = false;
                resultJsonInfo.ErrorMsg = ex.Message;
            }
            return Json(resultJsonInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}