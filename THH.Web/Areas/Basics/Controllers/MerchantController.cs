using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.DBModel;
using THH.Model.ReportModel.Dto;
using THH.Service.MerchantService;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Basics.Controllers
{
    public class MerchantController : ServicedController<MerchantService>
    {
        // GET: Basics/Merchant
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetMerchantGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<MerchantDto> MerchantDtos = Service.GetMerchantGrid(gp);
            return Json(new { total = gp.TotalCount, rows = MerchantDtos }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MerchantDto Merchantdto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (Merchantdto != null)
            {
                Merchantdto.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                Merchantdto.CredateTime = DateTime.Now;
            }
            return this.Ajax_JGClose();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var entity = Service.Find(id);
            //ViewData.Model = entity;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(MerchantDto Merchantdto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (Merchantdto != null)
            {
                Merchantdto.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                Merchantdto.UpdateTime = DateTime.Now;
            }
            return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}