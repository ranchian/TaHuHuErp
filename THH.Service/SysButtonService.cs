using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model.BaseModule;
using THH.Model.Dto;

namespace THH.Service
{
    public class SysButtonService
    {
        private IRepository<SysButton> sysButtonRepository = null;
        public SysButtonService()
        {
            sysButtonRepository = new RepositoryBase<SysButton>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<ButtonDto> GetButtonGrid(int limit, int offset)
        {
            var sysButtons = sysButtonRepository.Entities.ToList();
            return Mapper.Map<List<SysButton>, List<ButtonDto>>(sysButtons);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="buttonDto"></param>
        /// <returns></returns>
        public bool Add(ButtonDto buttonDto)
        {
            var sysButtons = Mapper.Map<ButtonDto, SysButton>(buttonDto);
            return sysButtonRepository.Insert(sysButtons) > 0;
        }
    }
}
