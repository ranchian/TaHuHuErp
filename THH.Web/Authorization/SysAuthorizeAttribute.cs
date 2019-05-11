using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THH.Web.Authorization
{
    public class SysAuthorizeAttribute : AuthorizeAttribute
    {
        public const string URL_Login = "/Aplication/Login"; //小写
        // 忽略URL
        private List<string> IgnoreURLs
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(URL_Login.ToLower());
                return value;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
            string controller = (string)filterContext.RouteData.Values["Controller"];
            string action = (string)filterContext.RouteData.Values["Action"];
            string url = urlHelper.Action(action, controller, new { id = "" }); //不验证参数
            if (IgnoreURLs.Contains(url.ToLower())) return;// 忽略URL
            // 必须登录
            if (filterContext.HttpContext.User.Identity == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ContentResult Content = new ContentResult();
                Content.Content = string.Format("<script type='text/javascript'>top.location.href='{0}';</script>", "/Aplication/Login");
                filterContext.Result = Content;
            }
            else
            {
                Principal principal = new Principal(HttpContext.Current.User.Identity.Name);
                HttpContext.Current.User = principal;
                //权限判断
                if (!ServiceHelper.AllowedAccess(url))
                {
                    if (filterContext.HttpContext.Request.Url.Host != "localhost")
                    {
                        //后期做权限验证
                        string message = string.Format("您没有此操作权限！");
                        filterContext.HttpContext.Response.Redirect(string.Format("~/Shared/Error?message={0}", message));
                    }
                }
            }
        }
    }
}