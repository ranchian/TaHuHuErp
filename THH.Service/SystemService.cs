using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using THH.Core;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;
using THH.Model.Dto;

namespace THH.Service
{
    public class SystemService
    {
        private IRepository<SysMenu> sysMenuRepository = null;
        private UserRightsService userRightsService = null;
        public SystemService()
        {
            sysMenuRepository = new RepositoryBase<SysMenu>();
            userRightsService = new UserRightsService();
        }
        /// <summary>
        /// 获取当前用户菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MenuTree> GetMenuByUserId(int userId)
        {
            Expression<Func<SysMenu, bool>> ex = t => true;
            ex = ex.And(t => t.Id == userId);
            var sysMenu = sysMenuRepository.GetEntities(ex).ToList();
            sysMenu = sysMenuRepository.Entities.ToList();

            List<UserRights> userRights = userRightsService.GetUserRights(userId);
            List<SysMenu> sysMenus = userRights.Select(p => p.SysMenu).ToList();
            List<MenuTree> menuTreeList = new List<MenuTree>();
            sysMenus.ForEach(p =>
            {
                menuTreeList.Add(new MenuTree
                {
                    Id = p.Id,
                    MenuCode = p.MenuCode,
                    Icon = p.Icon,
                    MenuLevel = p.MenuLevel,
                    MenuName = p.MenuName,
                    ParentId = p.ParentId,
                    SortNumber = p.SortNumber,
                    Url = p.Url
                });
            });
            return menuTreeList;
        }
    }
}
