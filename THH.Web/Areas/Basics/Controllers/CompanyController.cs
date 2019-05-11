using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Service.CompanyService;
using THH.Core.TablePage;
using THH.Model.ReportModel.Dto;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Basics.Controllers
{
    public class CompanyController : ServicedController<CompanyService>
    {
        // GET: Basics/Company
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCompanyGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<CompanyDto> companyDtos = Service.GetCompanyGrid(gp);
            return Json(new { total = gp.TotalCount, rows = companyDtos }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CompanyDto companydto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (companydto != null)
            {
                companydto.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                companydto.CredateTime = DateTime.Now;
                if (!Service.Add(companydto))
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
        public JsonResult Edit(CompanyDto companydto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (companydto != null)
            {
                companydto.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                companydto.UpdateTime = DateTime.Now;
                if (!Service.Update(companydto))
                {
                    resultJsonInfo.Success = false;
                    resultJsonInfo.ErrorMsg = "更新失敗";
                }
            }
            return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }

    }
}