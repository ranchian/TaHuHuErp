using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using THH.Core;
using THH.Core.TablePage;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;
using THH.Model.Dto;

namespace THH.Service
{
    public class SysMenuService : BaseService
    {
        private IRepository<SysMenu> sysMenuRepository = null;
        public SysMenuService()
        {
            sysMenuRepository = new RepositoryBase<SysMenu>();
        }
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<MenuTree> GetMenuGrid(TablePageParameter gp)
        {
            var sysMenu = sysMenuRepository.Entities;
            if (gp == null)
            {
                return Mapper.Map<List<SysMenu>, List<MenuTree>>(sysMenu.ToList());
            }
            return Mapper.Map<List<SysMenu>, List<MenuTree>>(GetTablePagedList(sysMenu, gp));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public bool Add(MenuTree menuDto)
        {
            var menu = Mapper.Map<MenuTree, SysMenu>(menuDto);
            return sysMenuRepository.Insert(menu) > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Deletes(string ids)
        {
            int[] ida = ids.StrToIntArray();
            return sysMenuRepository.Delete(ida) > 0;
        }
    }
}
