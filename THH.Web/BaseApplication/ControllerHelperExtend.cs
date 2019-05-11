using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using THH.Core.Response;
using THH.Web;

namespace System.Web.Mvc
{
    public static class ControllerHelperExtend
    {
        public static SelectList ToSelectList(this List<KeyValuePair<string, string>> value)
        {
            return ToSelectList(value, null);
        }

        public static SelectList ToSelectList(this List<KeyValuePair<string, string>> value, object selectedValue)
        {
            return new SelectList(value, "key", "value", selectedValue);
        }

        #region 通过指定实体的键值名称，然后直接转为SelectList

        /// <summary>
        /// 输入一个实体集合，生成一个SelectList
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keyColumn">键的lambda表达式</param>
        /// <param name="valueColumn">值的lambda表达式</param>
        /// <param name="emptyKey">空键</param>
        /// <param name="emptyValue">空值</param>
        /// <returns></returns>
        public static SelectList ToSelectList<T>(this IEnumerable<T> source, Expression<Func<T, string>> keyColumn, Expression<Func<T, string>> valueColumn, string emptyKey = null, string emptyValue = null)
        {
            return ToSelectList<T, string, string>(source, keyColumn, valueColumn, emptyKey, emptyValue);
        }

        /// <summary>
        /// 输入一个实体集合，生成一个SelectList
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keyColumn">键的lambda表达式</param>
        /// <param name="valueColumn">值的lambda表达式</param>
        /// <param name="emptyKey">空键</param>
        /// <param name="emptyValue">空值</param>
        /// <returns></returns>
        public static SelectList ToSelectList<T, TKey, TValue>(this IEnumerable<T> source, Expression<Func<T, TKey>> keyColumn, Expression<Func<T, TValue>> valueColumn, string emptyKey = null, string emptyValue = null)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (emptyKey != null) result.Add(new KeyValuePair<string, string>(emptyKey, emptyValue));

            var keyName = (keyColumn.Body as MemberExpression).Member.Name;
            var valueName = (valueColumn.Body as MemberExpression).Member.Name;

            var keyProperty = typeof(T).GetProperty(keyName);
            var valueProperty = typeof(T).GetProperty(valueName);

            foreach (var item in source)
            {
                var key = keyProperty.GetValue(item, null).ToString();
                var value = valueProperty.GetValue(item, null).ToString();
                result.Add(new KeyValuePair<string, string>(key, value));
            }

            return result.ToSelectList();
        }

        /// <summary>
        /// 通过集合生成选择项
        /// </summary>
        /// <param name="sourceList">源数据</param>
        /// <param name="emptyKey">空键</param>
        /// <param name="emptyValue">空值</param>
        /// <returns></returns>
        public static SelectList ToSelectList(IList<string> sourceList, string emptyKey = null, string emptyValue = null)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (emptyKey != null) result.Add(new KeyValuePair<string, string>(emptyKey, emptyValue));

            foreach (var item in sourceList)
            {
                result.Add(new KeyValuePair<string, string>(item, item));
            }
            return result.ToSelectList();
        }

        #endregion

        public static ContentResult Ajax_RedirectToAction(this Controller cntrl, string actionName, string controller = null)
        {
            string url = cntrl.Url.Action(actionName, controller);
            return new ContentResult() { Content = "<script>self.parent.openPage('" + url + "');</script>" };
        }

        public static ContentResult Ajax_FBClose(this Controller cntrl, string jScript = "")
        {
            if (string.IsNullOrWhiteSpace(jScript))
                return new ContentResult() { Content = "<script>parent.$.fancybox.close();</script>" };
            else
                return new ContentResult() { Content = string.Format("<script>parent.$.fancybox.close();{0}</script>", jScript) };
        }

        /// <summary>
        /// 关闭FancyBox脚本扩展方法
        /// </summary>
        /// <param name="cntrl">所在的控制器</param>
        /// <param name="objId">要赋值的对象ID，用于ID选择器</param>
        /// <param name="val">要赋的值</param>
        /// <returns></returns>
        public static ContentResult Ajax_FBClose(this Controller cntrl, string objId, string val)
        {
            return new ContentResult() { Content = string.Format("<script>parent.$.fancybox.close();parent.$('#{0}').val('{1}');</script>", objId, val) };
        }

        public static ContentResult Ajax_FBCloseToCode(this Controller cntrl, string jsCode)
        {
            return new ContentResult() { Content = string.Format("<script>parent.$.fancybox.close();{0}</script>", jsCode) };
        }

        public static ContentResult Ajax_JGClose(this Controller cntrl, string action = "Index", string extParam = "")
        {
            string result = "<script>";
            //result += "parent.$.fancybox.close();";
            //result += string.Format("self.parent.reloadJqGrid('{0}');", gridID);
            //result += extParam;
            //result += "</script>";
            result = string.Format("<script language='javaScript' type='text/javaScript'>window.parent.window.location.href = '{0}'</script>", action);
            // result = string.Format("<script>window.layer.closeAll()</script>");
            return new ContentResult() { Content = result };
        }
        public static ContentResult Ajax_JGCloseAndMessage(this Controller cntrl, string message, string extParam = "")
        {
            string result = "<script>";
            result += "parent.$.fancybox.close();";
            //  result += string.Format("self.parent.reloadJqGrid('{0}');", gridID);
            result += extParam;
            result += "</script>";

            //string result = "<script>";
            //result += "var index = parent.layer.getFrameIndex(window.name);";
            //result += "layer.open({";
            //result += "title: '提示',";
            //result += string.Format("content: '{0}',", message);
            //result += "move: false,";
            //result += "btn: '知道了',";
            //result += "yes: function() {";
            //result += "parent.layer.close(index);";
            //result += " },";
            //result += "cancel: function() {";
            //result += " parent.layer.close(index);";
            //result += "}";
            //result += "});";
            //result += "parent.$.fancybox.close();";
            ////result += string.Format("parent.showSucceedMsg('{0}');", message);
            //////result += string.Format("self.parent.reloadJqGrid('{0}');", gridID);
            ////result += extParam;
            //result += "</script>";

            return new ContentResult() { Content = result };
        }

        /* begin Json */
        /// <summary>
        /// 用于操作成功后返回。
        /// 返回json数据包含success、message,link
        /// </summary>
        public static JsonResult Json_SuccessResult(this Controller cntrl, string messageText = null, string toUrl = null, string toLink = null)
        {
            return Json_Result(cntrl, messageText, true, toUrl, toLink);
        }
        /// <summary>
        /// 用于操作失败后返回。
        /// 返回json数据包含success、message
        /// </summary>
        public static JsonResult Json_ErrorResult(this Controller cntrl, string errorMessage = null, string toUrl = null)
        {
            return Json_Result(cntrl, errorMessage, false, toUrl);
        }
        /// <summary>
        /// 用于操作失败后返回。
        /// 返回json数据包含success、message
        /// </summary>
        public static JsonResult Json_ErrorResult(this Controller cntrl, Exception e)
        {
            return Json_ErrorResult(cntrl, (e.InnerException == null ? e.Message : (e.InnerException.InnerException == null ? e.InnerException.Message : e.InnerException.InnerException.Message)));
        }
        /// <summary>
        /// 返回json数据包含success、message
        /// </summary>
        public static JsonResult Json_Result(this Controller cntrl, string messageText = null, bool isSuccess = true, string toUrl = null, string toLink = null)
        {
            var result = new JsonResult();
            result.Data = new { success = isSuccess, message = messageText, url = toUrl, link = toLink };
            return result;
        }
        /* end Json */

        /// <summary>
        /// write a client javascript to the server side
        /// </summary>
        /// <param name="cntrl"></param>
        /// <param name="script">javascript string</param>
        /// <param name="scriptInclude">if the script tag is included</param>
        /// <returns></returns>
        public static ContentResult Ajax_ClientScript(this Controller cntrl, string script, bool scriptInclude = false)
        {
            string scriptString = String.Empty;
            if (!scriptInclude)
            {
                scriptString = "<script>" + script + "</script>";
            }
            else
            {
                scriptString = script;
            }
            return new ContentResult() { Content = scriptString };
        }

        public static ContentResult Ajax_WindowClose(this Controller cntrl, string alertInfo = "", string gridID = "gridList", string extParam = "")
        {
            //window.close(); window.opener.reloadJqGrid('gridList');
            string result = "<script>";
            result += alertInfo;
            result += "window.close();";
            result += string.Format("window.opener.reloadJqGrid('{0}');", gridID);
            result += extParam;
            result += "</script>";

            return new ContentResult() { Content = result };
        }
        public static ContentResult Ajax_WindowCloseAndMessage(this Controller cntrl, string message, string alertInfo = "", string gridID = "gridList", string extParam = "")
        {
            //window.close(); window.opener.reloadJqGrid('gridList');
            string result = "<script>";
            result += alertInfo;
            result += "window.close();";
            result += string.Format("window.opener.showSucceedMsg('{0}');", message);
            result += string.Format("window.opener.reloadJqGrid('{0}');", gridID);
            result += extParam;
            result += "</script>";

            return new ContentResult() { Content = result };
        }

        //父JG和子JG同时刷新
        public static ContentResult Ajax_TwoJGClose(this Controller cntrl, string gridIDPar, string gridIDChild, string extParam = "")
        {
            string result = "<script>";
            result += "parent.$.fancybox.close();";
            result += string.Format("self.parent.reloadJqGrid('{0}');", gridIDPar);
            result += string.Format("self.parent.reloadJqGrid('{0}');", gridIDChild);
            result += extParam;
            result += "</script>";

            return new ContentResult() { Content = result };
        }
        /// <summary>
        /// 服务器出错返回对映的错误消息
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="jsonRequestBehavior"></param>
        /// <returns></returns>
        public static JsonResult JsonServerErrorResponse(this Controller controller, string errorMessage = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            var responseData = new BaseResponseData
            {
                ErrorCode = ErrorCodeDefine.ServerError,
                ErrorMessage = errorMessage
            };
            JsonResult jsonResult = new JsonResult
            {
                Data = responseData,
                JsonRequestBehavior = jsonRequestBehavior
            };
            return jsonResult;
        }
        /// <summary>
        /// 返回参数无效的对映信息
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="jsonRequestBehavior"></param>
        /// <returns></returns>
        public static JsonResult JsonParameterInvalidResponse(this Controller controller, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            var responseData = new BaseResponseData
            {
                ErrorCode = ErrorCodeDefine.ParameterRequired,
                ErrorMessage = Msg.ParamerRequired
            };
            var modelStates = controller.ModelState.Values;
            if (modelStates != null && modelStates.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in modelStates)
                {
                    if (item.Errors != null && item.Errors.Count > 0)
                    {
                        foreach (var errorItem in item.Errors)
                        {
                            sb.AppendFormat("{0},", errorItem.ErrorMessage);
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                    }
                    sb.AppendLine();
                }
                responseData.ErrorMessage = string.Format("{0}:", Msg.ParamerRequired, sb.ToString());
            }
            JsonResult jsonResult = new JsonResult
            {
                Data = responseData,
                JsonRequestBehavior = jsonRequestBehavior
            };
            return jsonResult;
        }
        public static ActionResult AjaxExportNullResponse(this Controller controller)
        {
            var script = string.Format("self.parent.showAlertMsg('{0}');", Msg.ExportDataNull);
            return controller.Ajax_ClientScript(script);
        }
        public static ActionResult AjaxErrorBoxMsg(this Controller controller, string msg)
        {
            var script = string.Format("self.parent.showAlertMsg('{0}');", msg);
            return controller.Ajax_ClientScript(script);
        }
    }
}