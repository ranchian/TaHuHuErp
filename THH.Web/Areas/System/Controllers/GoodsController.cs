using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class GoodsController : ServicedController<GoodsService>
    {
        // GET: System/Goods
        public ActionResult Index()
        {
            return View();
        }

        #region 列表(Grid)
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetGoodsGrid(int limit = 0, int offset = 0, string userName = "", string loginName = "")
        {
            ///   int supplierID = ServiceHelper.GetCurrentUser().UserID;
            List<GoodsDto> goodsDtos = Service.GetGoodsGrid(limit, offset, userName, loginName);
            return Json(new { total = goodsDtos.Count, rows = goodsDtos }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(GoodsDto goods)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (goods != null)
            {
                if (Service.FindByGoodsName(goods.GoodsName) == null)
                {
                    goods.CreateUser = ServiceHelper.GetCurrentUser().UserName;
                    goods.CredateTime = DateTime.Now;
                    if (!Service.Add(goods))
                    {
                        resultJsonInfo.Success = false;
                        resultJsonInfo.ErrorMsg = "添加失敗";
                        return Content("<script>alert('添加失敗');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('该商品已存在');history.go(-1);</script>");

                }
            }
            return Content("<script>alert('添加成功');</script>");
            //return this.Ajax_JGCloseAndMessage("保存成功");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = Service.Find(id);
            ViewData.Model = entity;
            return View();
        }

        [HttpPost]
        public JsonResult Edit(GoodsDto goods)
        {
            ResultJsonInfo resultJsonInfo = new ResultJsonInfo() { Data = "", ErrorMsg = "", Success = true };
            if (goods != null)
            {
                goods.UpdateUser = ServiceHelper.GetCurrentUser().UserName;
                goods.UpdateTime = DateTime.Now;
                if (!Service.Update(goods))
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
            List<GoodsExport> goodsExportModels = new List<GoodsExport>();
            List<GoodsDto> list = Service.GetAllGoods();
            //if (!string.IsNullOrEmpty(ids))
            //{
            //    list = Service.Find(ids);
            //}
            //else
            //{

            //}
            foreach (var item in list)
            {
                goodsExportModels.Add(new GoodsExport()
                {
                    商品名称 = item.GoodsName,
                    原价 = item.Price,
                    会员价 = item.VipPrice,
                    是否上架 = item.IfShelves == 1 ? "是" : "否",
                    是否促销 = item.IfPromotion == 1 ? "是" : "否",
                    创建时间 = item.CredateTime.ToString()
                });
            }
            if (list == null || list.Count == 0)
            {
                resultJsonInfo.Success = false;
                resultJsonInfo.ErrorMsg = "暂未数据";
            }
            else
            {
                string fileName = "商品导出.xls";
                ExportExcelHelper<GoodsExport>.ExportListToExcel_MvcResult(goodsExportModels, ref fileName);
                var path = "/Export/Temp/" + fileName;
                resultJsonInfo.Data = path;
            }
            return Json(resultJsonInfo);
        }

    }
}
