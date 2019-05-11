using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using THH.Core;
using THH.Core.TablePage;
using THH.DAL.ReportContext;
using THH.DAL.Repository;
using THH.Model.ReportModel.DBModel;
using THH.Model.ReportModel.Dto;

namespace THH.Service.ReportService
{
    public class StoreReportService : ReportBaseService
    {
        private IRepository<StoreReport> storeReportRepository = null;
        public StoreReportService()
        {
            storeReportRepository = new ReportRepository<StoreReport>();
        }
        /// <summary>
        /// 获取StoreReport列表
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<StoreReportDto> GetStoreReportGrid(TablePageParameter gp)
        {
            Expression<Func<StoreReport, bool>> ex = t => true;
            ex = ex.And(t => !t.IsDelete);
            var storeReportList = storeReportRepository.GetEntities(ex);
            if (gp == null)
            {
                return Mapper.Map<List<StoreReport>, List<StoreReportDto>>(storeReportList.ToList());
            }
            else
            {
                return Mapper.Map<List<StoreReport>, List<StoreReportDto>>(GetTablePagedList(storeReportList, gp));
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(List<StoreReportDto> storeReportDtos)
        {
            var StoreReports = Mapper.Map<List<StoreReportDto>, List<StoreReport>>(storeReportDtos);
            return storeReportRepository.Insert(StoreReports) > 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(StoreReportDto storeReportDtos)
        {
            var StoreReports = Mapper.Map<StoreReportDto, StoreReport>(storeReportDtos);
            return storeReportRepository.Insert(StoreReports) > 0;
        }

        public StoreReportDto Find(int id)
        {
            StoreReport storeReport = storeReportRepository.Find(id);
            return Mapper.Map<StoreReport, StoreReportDto>(storeReport);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="goodsDto"></param>
        /// <returns></returns>
        public bool Update(StoreReportDto storeReportDtos)
        {
            var storeReport = Mapper.Map<StoreReportDto, StoreReport>(storeReportDtos);
            return storeReportRepository.Update(storeReport) > 0;
        }


        /// <summary>
        /// 批量更新(实则更新是否删除字段)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool UpdateIsDelete(string ids)
        {
            int[] ida = ids.StrToIntArray();
            List<StoreReportDto> storeReportDtos = new List<StoreReportDto>();
           List<StoreReport> storeReports= storeReportRepository.Entities.Where(p => ida.Contains(p.Id)).ToList();
            storeReports.ForEach(p =>
            {
                p.IsDelete = true;
            });
            return storeReportRepository.Update(storeReports) > 0;
        }

  
        public void test()
        {
            try
            {
                var storeReportList = storeReportRepository.Entities.ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
    }
}
