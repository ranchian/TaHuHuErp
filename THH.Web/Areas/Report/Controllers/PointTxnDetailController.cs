using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Core.TablePage;
using THH.Model.ReportModel.DBModel;
using THH.Service.ReportService;
using THH.Web.BaseApplication;

namespace THH.Web.Areas.Report.Controllers
{
    public class PointTxnDetailController : ServicedController<PointTxnDetailService>
    {
        // GET: Report/PointTxnDetail
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
        public JsonResult GetPointTxnDetailGrid(int limit, int offset, string sort, string sortOrder)
        {
            TablePageParameter gp = new TablePageParameter() { Limit = limit, Offset = offset, SortName = sort, SortOrder = sortOrder };
            List<PointTxnDetail> pointTxnDetails = Service.GetPointTxnDetailGrid(gp);
            return Json(new { total = gp.TotalCount, rows = pointTxnDetails }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}