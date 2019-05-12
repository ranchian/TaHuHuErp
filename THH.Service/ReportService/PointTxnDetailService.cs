using System.Collections.Generic;
using THH.Core.TablePage;
using THH.DAL.ReportContext;
using THH.DAL.Repository;
using THH.Model.ReportModel.DBModel;

namespace THH.Service.ReportService
{
    public class PointTxnDetailService : BaseService
    {
        private IRepository<PointTxnDetail> pointTxnDetailtRepository = null;
        public PointTxnDetailService()
        {
            pointTxnDetailtRepository = new ReportRepository<PointTxnDetail>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pointTxnDetail"></param>
        /// <returns></returns>
        public bool Add(List<PointTxnDetail> pointTxnDetails)
        {
            return pointTxnDetailtRepository.Insert(pointTxnDetails) > 0;
        }
        /// <summary>
        /// 获取PointTxnDetail列表
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<PointTxnDetail> GetPointTxnDetailGrid(TablePageParameter gp)
        {
            var storeReportList = pointTxnDetailtRepository.Entities;
            return GetTablePagedList(storeReportList, gp);
        }
    }
}
