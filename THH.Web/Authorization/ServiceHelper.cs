using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using THH.Model;
using THH.Service;

namespace THH.Web.Authorization
{
    public static class ServiceHelper
    {
        public static UserIdentity GetCurrentUser()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return null;
            IPrincipal user = context.User;
            if (user == null)
                return null;
            return (user.Identity as UserIdentity);
        }
        // 是否管理员
        public static bool IsAdmin()
        {
            return GetCurrentUser().IsAdmin;
        }
        //权限判断按钮权限
        public static bool AllowedAuthorizes(string authorize)
        {
            UserIdentity user = GetCurrentUser();
            if (user != null)
            {
                if (user.IsAdmin) return true;
                return user.Authorizes.Contains(authorize);
            }
            else
            {
                return false;
            }
        }
        //权限判断Url权限
        public static bool AllowedAccess(string url)
        {
            if (string.IsNullOrEmpty(url) || url.Trim() == string.Empty) return true;
            url = url.Trim();
            UserIdentity user = GetCurrentUser();
            ////查找用户权限
            if (user.IsAdmin) return true;
            UserRightsService rightsSvc = new UserRightsService();
            List<UserRights> rightList = rightsSvc.GetListByUrl(url);
            if (rightList == null || rightList.Count == 0) return false; //如果找不到的url，则可以访问
            user.Authorizes = rightList.Select(p => p.SysButton.ButtonCode).ToList();
            return false;
        }
    }
}