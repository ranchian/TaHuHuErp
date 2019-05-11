using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Model.Dto;
using THH.Service;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.System.Controllers
{
    public class ButtonController : ServicedController<SysButtonService>
    {
        // GET: System/Button
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
        public JsonResult GetButtonGrid(int limit = 0, int offset = 0)
        {
            List<ButtonDto> buttonDtoDtos = Service.GetButtonGrid(limit, offset);
            return Json(new { total = buttonDtoDtos.Count, rows = buttonDtoDtos }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ButtonDto buttonDto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (ModelState.IsValid)
            {
                buttonDto.CreateUser = ServiceHelper.GetCurrentUser().LoginName;
                buttonDto.CredateTime = DateTime.Now.Date;
                if (!Service.Add(buttonDto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "添加失败！";
                }
                return this.Ajax_JGCloseAndMessage("保存成功");
            }
            else
            {
                return View();
            }
            //  return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}