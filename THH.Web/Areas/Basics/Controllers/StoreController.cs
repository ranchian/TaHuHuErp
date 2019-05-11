using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.Dto;
using THH.Service.CompanyService;
using THH.Service.StoreService;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Basics.Controllers
{
    public class StoreController : ServicedController<StoreService>
    {
        // GET: Basics/Store
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStoreGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<StoreDto> StoreDtos = Service.GetStoreGrid(gp);
            return Json(new { total = gp.TotalCount, rows = StoreDtos }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(StoreDto storedto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (storedto != null)
            {
                storedto.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                storedto.CredateTime = DateTime.Now;
                if (!Service.Add(storedto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "添加失敗";
                    return Content("<script>alert('添加失敗');history.go(-1);</script>");
                }
            }
            //return Content("<script>alert('添加成功');</script>");
            return this.Ajax_JGClose();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = Service.Find(id);
            ViewData.Model = entity;
            return View();
        }

        [HttpPost]
        public JsonResult Edit(StoreDto storedto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (storedto != null)
            {
                storedto.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                storedto.UpdateTime = DateTime.Now;
                if (!Service.Update(storedto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "更新失敗";
                }
            }
            return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}