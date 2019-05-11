using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using THH.Core;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;

namespace THH.Service
{
    public class UserRightsService
    {
        private IRepository<UserRights> userRightsRepository = null;
        public UserRightsService()
        {
            userRightsRepository = new RepositoryBase<UserRights>();
        }
        public List<UserRights> GetUserRights(int userId)
        {
            return userRightsRepository.Entities.ToList();
        }
        /// <summary>
        /// 获取用户请求Url
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserRights> GetListByUrl(string url)
        {
            Expression<Func<UserRights, bool>> ex = t => true;
            ex = ex.And(t => t.SysMenu.Url == url);
            return userRightsRepository.GetEntities(ex, true).ToList();
        }
    }
}
