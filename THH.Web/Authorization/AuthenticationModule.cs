using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;
using THH.Service;

namespace THH.Web.Authorization
{
    public class AuthenticationModule : IHttpModule
    {
        private const int AUTHENTICATION_TIMEOUT = 20;

        public AuthenticationModule() { }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(Context_AuthenticateRequest);
        }
        public void Dispose()
        {

        }
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool AuthenticateUser(string loginName, string password, ref string errorMsg, bool rememberMe = false, string currentIp = null)
        {
            var userService = new UserService();
            try
            {
                string curSite = HttpContext.Current.Request.Url.Host;
                Model.User user = userService.Login(loginName, password);// EncryptHelper.MD5Encrypt32(password)
                if (user != null)
                {
                    UserIdentity userIdentity = new UserIdentity(user, true);
                    HttpContext.Current.User = new Principal(userIdentity);
                    string accountJson = JsonConvert.SerializeObject(userIdentity);
                    //创建用户票据
                    var ticket = new FormsAuthenticationTicket(1, userIdentity.UserName, DateTime.Now, DateTime.Now.AddDays(1), false, accountJson);
                    //加密
                    string encryptAccount = FormsAuthentication.Encrypt(ticket);
                    //创建cookie
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptAccount)
                    {
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL,
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath
                    };
                    //写入Cookie
                    HttpContext.Current.Response.Cookies.Remove(cookie.Name);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return true;
                }
                else
                {
                    errorMsg = "账号或密码有误,请重新输入";
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("登陆失败 {0}:{1}", loginName, e.Message), e);
            }
        }
        /// <summary>
        ///用户注销.
        /// </summary>
        public static void Logout()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }
        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            HttpApplication app = (HttpApplication)sender;
            try
            {
                if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
                {
                    if (string.IsNullOrEmpty(app.Context.User.Identity.Name))
                        return;
                    FormsIdentity id = (FormsIdentity)app.Context.User.Identity;
                    FormsAuthenticationTicket authTicket = id.Ticket;
                    if (string.IsNullOrEmpty(authTicket.UserData) || authTicket.UserData.Trim() == string.Empty)
                    {
                        Model.User user = userService.GetUser(Convert.ToInt32(app.Context.User.Identity.Name));
                        UserIdentity userIdentity = new UserIdentity(user, true);
                        app.Context.User = new Principal(userIdentity);
                    }
                }
            }
            catch
            {
                app.Context.User = new Principal();
                FormsAuthentication.SignOut();
            }
        }
    }
}