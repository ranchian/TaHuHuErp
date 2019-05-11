using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using THH.Core;
using THH.Core.TablePage;
using THH.DAL.ReportContext;
using THH.DAL.Repository;
using THH.Model.ReportModel.DBModel;
using THH.Model.ReportModel.Dto;
using THH.Service.ReportService;

namespace THH.Service.StoreService
{
    public class StoreService : ReportBaseService
    {
        private IRepository<Store> StoreRepository = null;
        public StoreService()
        {
            StoreRepository = new ReportRepository<Store>();
        }

        /// <summary>
        /// 获取StoreReport列表
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<StoreDto> GetStoreGrid(TablePageParameter gp)
        {
            Expression<Func<Store, bool>> ex = t => true;
            ex = ex.And(t => !t.IsDelete);
            var StoreList = StoreRepository.GetEntities(ex);
            if (gp == null)
            {
                return Mapper.Map<List<Store>, List<StoreDto>>(StoreList.ToList());
            }
            else
            {
                return Mapper.Map<List<Store>, List<StoreDto>>(GetTablePagedList(StoreList, gp));
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(List<StoreDto> StoreDtos)
        {
            var Store = Mapper.Map<List<StoreDto>, List<Store>>(StoreDtos);
            return StoreRepository.Insert(Store) > 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(StoreDto StoreDtos)
        {
            var Store = Mapper.Map<StoreDto, Store>(StoreDtos);
            return StoreRepository.Insert(Store) > 0;
        }


        public StoreDto Find(int id)
        {
            Store Store = StoreRepository.Find(id);
            return Mapper.Map<Store, StoreDto>(Store);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="StoreDto"></param>
        /// <returns></returns>
        public bool Update(StoreDto StoreDtos)
        {
            var Store = Mapper.Map<StoreDto, Store>(StoreDtos);
            return StoreRepository.Update(Store) > 0;
        }


    }
}
