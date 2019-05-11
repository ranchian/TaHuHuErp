using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.Dto;
using THH.Service.ReportService;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Areas.Basics.Controllers
{
    public class MachineController : ServicedController<MachineService>
    {
        // GET: Basics/Machine
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetMachineGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<MachineDto> machineDtos = Service.GetMachineGrid(gp);
            return Json(new { total = gp.TotalCount, rows = machineDtos }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MachineDto Machinedto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (Machinedto != null)
            {
                Machinedto.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                Machinedto.CredateTime = DateTime.Now;
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
        public ActionResult Edit(MachineDto Machinedto)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (Machinedto != null)
            {
                Machinedto.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                Machinedto.UpdateTime = DateTime.Now;
            }
            return Json(new { resultJsonInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}