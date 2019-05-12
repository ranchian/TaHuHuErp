using System;
using System.Collections.Generic;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.Dto;
using THH.Service.CompanyService;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Basics.Controllers
{
    public class CompanyController :ServicedController<CompanyService>
    {
        // GET: Basics/Company
        public ActionResult Index()
        {
            ViewBag.DepartmentSelectList = GetSelectList().ToSelectList();
            return View();
        }
        public List<KeyValuePair<string, string>> GetSelectList(string emptyKey = null, string emptyValue = null)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (emptyKey != null) result.Add(new KeyValuePair<string, string>(emptyKey, emptyValue));
            result.Add(new KeyValuePair<string, string>("1", "阿里巴巴"));
            result.Add(new KeyValuePair<string, string>("2", "京东"));
            return result;
        }
        public JsonResult GetCompanyGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<CompanyDto> companyDtos = Service.GetCompanyGrid(gp);
            return Json(new { total = gp.TotalCount }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Edit(CompanyDto companydto)
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