using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.DBModel;
using THH.Model.ReportModel.Dto;
using THH.Service.ReportService;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Report.Controllers
{
    public class StoreReportController : ServicedController<StoreReportService>
    {
        // GET: System/StoreReport
        public ActionResult Index()
        {
            return View();
        }

        #region 商户列表(Grid)
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetStoreGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<StoreReportDto> storeReportDtos = Service.GetStoreReportGrid(gp);
            return Json(new { total = gp.TotalCount, rows = storeReportDtos }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(StoreReportDto storereportdto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (storereportdto != null)
            {
                storereportdto.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                storereportdto.CredateTime = DateTime.Now;
                if (!Service.Add(storereportdto))
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
        public JsonResult Edit(StoreReportDto storereportdto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (storereportdto != null)
            {
                storereportdto.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                storereportdto.UpdateTime = DateTime.Now;
                if (!Service.Update(storereportdto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "更新失敗";
                }
            }
            return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(string ids)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            try
            {
                if (!Service.UpdateIsDelete(ids))
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


    }
}