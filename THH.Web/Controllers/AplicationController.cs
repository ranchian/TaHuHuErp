using System.Web.Mvc;
using System.Web.Security;
using THH.Service;
using THH.Web.Authorization;
using THH.Web.BaseApplication;
using THH.Web.Models;

namespace THH.Web.Controllers
{
    public class AplicationController : ServicedController<SystemService>
    {
        // GET: Aplication
        public ActionResult Index()
        {
            ViewBag.User = ServiceHelper.GetCurrentUser().LoginName;
            int userId = ServiceHelper.GetCurrentUser().UserID;
            var menuList = Service.GetMenuByUserId(userId);
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.errorMsg = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            string errorMsg = "";
            if (ModelState.IsValid)
            {
                bool isLoginSc = AuthenticationModule.AuthenticateUser(model.LoginName, model.Password, ref errorMsg);
                if (isLoginSc)
                {
                    return RedirectToAction("Index", "Aplication");
                }
                ViewBag.errorMsg = errorMsg;
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Aplication");
        }
    }
}