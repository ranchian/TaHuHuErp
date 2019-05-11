using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THH.Core;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;
using THH.Model.Dto;
namespace THH.Service
{
    public class GoodsService
    {
        private IRepository<Goods> GoodsRepository = null;

        public GoodsService()
        {
            GoodsRepository = new RepositoryBase<Goods>();
        }

        public List<GoodsDto> GetAllGoods()
        {
            var goods = GoodsRepository.Entities.ToList();
            return Mapper.Map<List<Goods>, List<GoodsDto>>(goods);
        }

        public List<GoodsDto> GetGoodsGrid(int limit, int offset, string userName, string loginName)
        {
            var goods = GoodsRepository.Entities.ToList();
            return Mapper.Map<List<Goods>, List<GoodsDto>>(goods);
        }

        public bool Add(GoodsDto goodsDto)
        {
            var goods = Mapper.Map<GoodsDto, Goods>(goodsDto);
            return GoodsRepository.Insert(goods) > 0;
        }

        public GoodsDto Find(int id)
        {
            Goods goods = GoodsRepository.Find(id);
            return Mapper.Map<Goods, GoodsDto>(goods);
        }

        public GoodsDto FindByGoodsName(string name)
        {
            Goods goods = GoodsRepository.Entities.Where(P => P.GoodsName == name).FirstOrDefault();
            return Mapper.Map<Goods, GoodsDto>(goods);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="goodsDto"></param>
        /// <returns></returns>
        public bool Update(GoodsDto goodsDto)
        {
            var goods = Mapper.Map<GoodsDto, Goods>(goodsDto);
            return GoodsRepository.Update(goods) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Deletes(string ids)
        {
            int[] ida = ids.StrToIntArray();
            return GoodsRepository.Delete(ida) > 0;
        }

    }
}
