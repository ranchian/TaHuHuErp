using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using THH.Web.Authorization;

namespace THH.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OverrideAuthorization]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Table()
        {
            return View();
        }
        public JsonResult Tables()
        {
            List<BadPurchaseOrderGrid> badOrderGridList = new List<BadPurchaseOrderGrid>() { new BadPurchaseOrderGrid() {
                BadDescription ="sd",
                BadQuantity =1,
                 BadType="bu",
                  Id="12",
                   Img="",
                    InvoiceNo="123",
                     ProductName="ce",
                      PurchaseOrderNo="123123",
                       Remark="12312",
                        SKU="12",
                         Status="1"
            } };
            return Json(new { total = badOrderGridList.Count, rows = badOrderGridList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BootStrapTable()
        {
            return View();
        }
        public JsonResult GetDepartment(int limit, int offset, string departmentname, string statu)
        {
            var lstRes = new List<Department>();
            for (var i = 0; i < 50; i++)
            {
                var oModel = new Department();
                oModel.ID = Guid.NewGuid().ToString();
                oModel.Name = "销售部" + i;
                oModel.Level = i.ToString();
                oModel.Desc = "暂无描述信息";
                lstRes.Add(oModel);
            }

            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
    }
    /// <summary>
    /// 不良品订单
    /// </summary>
    public class BadPurchaseOrderGrid
    {
        public string Id { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// SKU编号
        /// </summary>
        public string SKU { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseOrderNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 发货单号
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 不良类型
        /// </summary>
        public string BadType { get; set; }
        /// <summary>
        /// 不良描述
        /// </summary>
        public string BadDescription { get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public int BadQuantity { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    public class Department
    {
        public string ID { set; get; }

        public string Name { set; get; }

        public string ParentName { set; get; }

        public string Level { set; get; }

        public string Desc { set; get; }
    }
}