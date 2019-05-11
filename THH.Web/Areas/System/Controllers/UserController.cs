using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Core.Excel.Npoi;
using THH.Model.Dto;
using THH.Service;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;
using THH.Web.Models.ExportModel;

namespace THH.Web.Areas.System.Controllers
{
    public class UserController : ServicedController<UserService>
    {
        // GET: System/User
        public ActionResult Index()
        {
            return View();
        }
        #region 列表(Grid)
        /// <summary>
        /// User列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserGrid(int limit = 0, int offset = 0, string userName = "", string loginName = "")
        {
            List<UserDto> userDtos = Service.GetUserGrid(limit, offset, userName, loginName);
            return Json(new { total = userDtos.Count, rows = userDtos }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserDto userDto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (ModelState.IsValid)
            {

            }
            if (userDto != null)
            {
                userDto.CreateUser = ServiceHelper.GetCurrentUser().LoginName;
                userDto.CredateTime = DateTime.Now.Date;
                if (!Service.Add(userDto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "添加失败！";
                }
            }
            return this.Ajax_JGClose();
            //  return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = Service.Find(id);
            ViewData.Model = entity;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserDto userDto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            string result = "";
            //result += "var index = parent.layer.getFrameIndex(window.name);console.log(index);"; 
            result += "layer.closeAll():";
            //result += "layer.open({";
            //result += "title: '提示',";
            //result += string.Format("content: '{0}',", "成功");
            //result += "move: false,";
            //result += "btn: '知道了',";
            //result += "yes: function() {";
            //result += "parent.layer.close(index);";
            //result += " },";
            //result += "cancel: function() {";
            //result += " parent.layer.close(index);";
            //result += "}";
            //result += "});";
            //result += string.Format("parent.showSucceedMsg('{0}');", message);
            ////result += string.Format("self.parent.reloadJqGrid('{0}');", gridID);
            //result += extParam;
            result += "";
            // return this.Content("<script>layer.closeAll():</script>");
            //   return this.Content(result, "application/x-javascript");
            //return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
            return this.Ajax_JGClose();

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
        [HttpPost]
        public JsonResult ExportExcel(string ids)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            List<UserExport> userExportModels = new List<UserExport>();
            List<UserDto> list = Service.GetAllUsers();
            //if (!string.IsNullOrEmpty(ids))
            //{
            //    list = Service.Find(ids);
            //}
            //else
            //{

            //}
            foreach (var item in list)
            {
                userExportModels.Add(new UserExport()
                {
                    用户名 = item.RealName,
                    账号 = item.LogingName,
                    邮箱 = item.Email
                });
            }
            if (list == null || list.Count == 0)
            {
                resultJsonInfo.Success = false;
                resultJsonInfo.ErrorMsg = "暂未数据";
            }
            else
            {
                string fileName = "用户导出.xls";
                ExportExcelHelper<UserExport>.ExportListToExcel_MvcResult(userExportModels, ref fileName);
                var path = "/Export/Temp/" + fileName;
                resultJsonInfo.Data = path;
            }
            return Json(resultJsonInfo);
        }

    }
}