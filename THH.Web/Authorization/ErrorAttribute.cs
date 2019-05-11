using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THH.Web.Models;

namespace THH.Web.Authorization
{
    public class ErrorAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString(); ;
            ErrorMsg model = new ErrorMsg();
            model.msg = filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message : filterContext.Exception.Message;

            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Shared/Errorpage.cshtml",
                ViewData = new ViewDataDictionary<ErrorMsg>(model),
                MasterName = Master,
                TempData = filterContext.Controller.TempData
            };
            //如果没有处理该异常
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Exception.Message.Contains("Sorry,threre is an error in your web server."))
                {
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Write("这是一个自定义异常处理过滤器");
                }
            }
        }
    }
}