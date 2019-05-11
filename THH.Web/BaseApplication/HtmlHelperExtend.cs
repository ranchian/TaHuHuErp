using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using THH.Web.Authorization;

namespace THH.Web.BaseApplication
{
    public static class HtmlHelperExtend
    {
        public static MvcHtmlString LabelFor(this HtmlHelper html, string lableName, string labelFor)
        {
            return MvcHtmlString.Create(string.Format("<label for=\"{1}\" style=\"white-space:nowrap;\">{0}</label>", lableName, labelFor));
        }
        /// <summary>
        /// 操作成功时显示信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg">显示的信息</param>
        /// <returns></returns>
        public static MvcHtmlString SuccessText(this HtmlHelper html, string msg)
        {
            return MvcHtmlString.Create(string.Format("<div style='color:green;margin-left:5px;'>{0}</div>", msg));
        }
        /// <summary>
        /// 操作成功时显示信息，把信息传入ViewData["success"]
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SuccessText(this HtmlHelper html)
        {
            if (html.ViewData["success"] == null)
            {
                return MvcHtmlString.Empty;
            }
            return SuccessText(html, html.ViewData["success"].ToString());
        }
        /// <summary>
        /// 出错时显示信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg">显示的信息</param>
        /// <returns></returns>
        public static MvcHtmlString ErrorText(this HtmlHelper html, string msg)
        {
            return MvcHtmlString.Create(string.Format("<div style='font-size:14px;color:red;margin-left:5px;'>{0}</div>", msg));
        }
        /// <summary>
        /// 操作成功时显示信息，把信息传入ViewData["error"]
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString ErrorText(this HtmlHelper html)
        {
            if (html.ViewData["error"] == null)
            {
                return MvcHtmlString.Empty;
            }
            return ErrorText(html, html.ViewData["error"].ToString());
        }

        /* BEGIN button */
        #region button
        public static MvcHtmlString Button(this HtmlHelper html, string value, string onclickScript = null, string authorizeCode = null, string parameters = null)
        {
            if (string.IsNullOrWhiteSpace(authorizeCode) || ServiceHelper.AllowedAuthorizes(authorizeCode))
            {
                string result = "<button  type=\"button\"";
                if (!string.IsNullOrEmpty(onclickScript)) result += string.Format(" onclick=\"{0}\" ", onclickScript);
                if (!string.IsNullOrEmpty(parameters)) result += parameters;
                result += string.Format(" >{0}</button>", value);
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Empty;
        }


        public static MvcHtmlString ButtonForGridSearch(this HtmlHelper html, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<button type=\"button\" onclick=\"gridSearch($(this).parent(),'{0}'); \">" + Msg.Search + "</button>", gridID));
        }
        public static MvcHtmlString ButtonForGridSearchMore(this HtmlHelper html)
        {
            return MvcHtmlString.Create("<input  type=\"button\" value=\"更多\" onclick=\" $('[sdm=more]', '.bodySearch').toggle();\"  />");
        }

        /// <summary>
        /// 带搜索框和搜索按钮
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchButtonForGrid(this HtmlHelper html, string gridID = "gridList", string parameters = "", string placeholder = "")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"search\" " + parameters + "onBlur=\"" + "this.value=this.value.replace(/\\s/gi,'')\"" + "   placeholder=\"" + placeholder + "\"onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />" + ButtonForGridSearch(html, gridID));
        }
        /// <summary>
        /// 带搜索框和搜索按钮、带详细搜索按钮
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchButtonMoreForGrid(this HtmlHelper html, string gridID = "gridList", string parameters = "", string placeholder = "")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"search\" " + parameters + "   placeholder=\"" + placeholder + "\"onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />" + ButtonForGridSearch(html, gridID) + ButtonForGridSearchMore(html));
        }
        /// <summary>
        /// 创建搜索框
        /// </summary>
        /// <param name="html"></param>
        /// <param name="textName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchTextForGrid(this HtmlHelper html, string textName, string parameters = "", string textWidth = "200px")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"" + textName + "\" placeholder=\"" + parameters + "\" style=\"width:" + textWidth + "\" onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />");
        }
        public static MvcHtmlString CloseFormButton(this HtmlHelper html, string value)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"$(this).parents('form').hide();\" />", value));
        }

        public static MvcHtmlString SubmitGrid(this HtmlHelper html, string value, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<button type=\"button\" onclick=\"gridSearch($(this).parent(),'{0}'); \">" + value + "</button>", gridID));
        }

        public static MvcHtmlString Submit(this HtmlHelper html, string value, string parameters)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"submit\" value=\"{0}\" {1}/>", value, parameters));
        }
        public static MvcHtmlString Submit(this HtmlHelper html, string value, bool clickToHide = false)
        {
            return Submit(html, value, clickToHide ? "onclick=' $(this).hide();'" : "");
        }


        /// <summary>
        /// submit 添加
        /// </summary>
        /// <param name="html"></param>
        /// <param name="clickToHide"></param>
        /// <param name="text">显示文字</param>
        /// <returns></returns>
        public static MvcHtmlString Submit_Add(this HtmlHelper html, bool clickToHide = false, string text = "")
        {
            var displayText = Msg.Add;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayText = text;
            }
            //edit:17/5/4 yly 添加的时候弹出等待提示
            return Submit(html, displayText, clickToHide ? "onclick=' $(this).hide();'" : "onclick=\"if($('form').valid()){showWaitingMsg();}\"");
        }
        //submit 保存
        public static MvcHtmlString Submit_Save(this HtmlHelper html, bool clickToHide = false)
        {
            return Submit(html, Msg.Save, clickToHide ? "onclick=' $(this).hide();'" : "");
        }

        public static MvcHtmlString SubmitDefault(this HtmlHelper html, string value)
        {
            return Submit(html, value, "class=\"button default\"");
        }

        public static MvcHtmlString ResetButton(this HtmlHelper html, string value, string parameters = "")
        {
            return MvcHtmlString.Create(string.Format("<input type=\"reset\" value=\"{0}\" {1}/>", value, parameters));
        }

        #endregion
        /* END button */

        public static MvcHtmlString Window_Close(this HtmlHelper html, string url = null)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"javascript:window.opener=null;window.close();\"/> ", "关闭"));
        }



        /*bodyButton*/
        #region bodyButton
        /// <summary>
        /// 生成bodyButton里按钮的标签
        /// </summary>      
        /// <param name="text">显示在按钮上的文字</param>
        /// <param name="onclickScript">点击后要执行的脚本</param>
        /// <param name="authorizeCode">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="name">按钮的id和name</param>
        /// <param name="parameters">其它属性</param>
        public static MvcHtmlString BodyButtonA(this HtmlHelper html, string text, string onclickScript, string authorizeCode = null, string className = "default", string name = null, string parameters = null, string title = null)
        {
            if (string.IsNullOrWhiteSpace(authorizeCode) || ServiceHelper.AllowedAuthorizes(authorizeCode))
            {
                string result = "<a ";
                if (!string.IsNullOrEmpty(name)) result += string.Format("id=\"{0}\" name=\"{0}\" ", name);
                if (!string.IsNullOrEmpty(className)) result += string.Format("class=\"{0}\" ", className);
                if (!string.IsNullOrEmpty(onclickScript)) result += string.Format("onclick=\"{0}\" ", onclickScript);
                if (!string.IsNullOrEmpty(parameters)) result += parameters;
                if (!string.IsNullOrEmpty(title)) result += string.Format("title=\"{0}\" ", title);
                result += string.Format(" >{0}</a>", text);
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 生成bodyButton里按钮的标签
        /// </summary>      
        /// <param name="text">显示在按钮上的文字</param>
        /// <param name="onclickScript">点击后要执行的脚本</param>
        /// <param name="authorizeCode">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="name">按钮的id和name</param>
        /// <param name="parameters">其它属性</param>
        public static MvcHtmlString BodyButtonB(this HtmlHelper html, string text, string type, string onclickScript, string authorizeCode = null, string className = "default", string name = null, string parameters = null, string title = null)
        {
            if (string.IsNullOrWhiteSpace(authorizeCode) || ServiceHelper.AllowedAuthorizes(authorizeCode))
            {
                string result = "<button ";
                if (!string.IsNullOrEmpty(type)) result += string.Format("type=\"{0}\" ", type);
                if (!string.IsNullOrEmpty(name)) result += string.Format("id=\"{0}\" name=\"{0}\" ", name);
                if (!string.IsNullOrEmpty(className)) result += string.Format("class=\"{0}\" ", className);
                if (!string.IsNullOrEmpty(onclickScript)) result += string.Format("onclick=\"{0}\" ", onclickScript);
                if (!string.IsNullOrEmpty(parameters)) result += parameters;
                if (!string.IsNullOrEmpty(title)) result += string.Format("title=\"{0}\" ", title);
                result += "><i class=\"fa fa-trash\"></i>&nbsp;" + text + "";
                result += "</button>";
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// 生成bodyButton里添加按钮的标签
        /// </summary>
        /// <param name="url">添加页面的路径</param>
        /// <param name="width">新开的的页面宽度</param>
        /// <param name="height">新开的的页面高度</param>
        /// <param name="authorizeCode">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="text">显示在按钮上的文字:默认添加</param>
        public static MvcHtmlString BodyButton_Add(this HtmlHelper html, string url, object width, object height, string authorizeCode = null, string className = "add", string text = "")
        {
            if (className == null) className = "add";
            string script = string.Format("showPage('{0}','{1}', '{2}');", url, width, height);
            var displayName = Msg.Add;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayName = text;
            }
            return BodyButtonA(html, displayName, script, authorizeCode, className);
        }
        /// <summary>
        /// 生成bodyButton里编辑按钮的标签，用于勾选Grid单行后编辑此行
        /// </summary>
        /// <param name="url">编辑页面的路径，并附带参数名如"id="或"/"，后面会自动加上获取的ID值</param>
        /// <param name="width">新开的的页面宽度</param>
        /// <param name="height">新开的的页面高度</param>
        /// <param name="authorizeCode">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="gridID">获取id的Grid ID</param>
        /// <param name="text">显示在按钮上的文字:默认添加</param>
        public static MvcHtmlString BodyButton_Edit(this HtmlHelper html, string url, object width, object height, string authorizeCode = null, string className = "edit", string gridID = "gridList", string text = "")
        {
            if (className == null) className = "edit";
            var displayName = Msg.Edit;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayName = text;
            }
            string script = "var id = $('#" + gridID + "').getGridParam('selarrrow'); if (id.length != 1) { showAlertMsg('" + Msg.Message_OnlyAllowOneItemSelect + "'); return } showPage('" + url + "' + id, '" + (string)width + "', '" + (string)height + "');";
            return BodyButtonA(html, displayName, script, authorizeCode, className);
        }

        /// <summary>
        /// 生成bodyButton里删除按钮的标签，用于勾选Grid多行后删除这些行数据
        /// </summary>
        /// <param name="url">删除页面的路径，并附带参数名如"ids="或"/"，后面会自动加上获取的ID值(多ID用英文逗号分隔)</param>
        /// <param name="authorizeCode">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="gridID">获取ids的Grid ID</param>
        /// <returns></returns>
        public static MvcHtmlString BodyButton_Del(this HtmlHelper html, string url, string authorizeCode = null, string className = "delete", string gridID = "gridList", string customName = "")
        {
            if (className == null) className = "delete";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + Msg.Delete_Confirm + "', function () { var dialogObj = showWaitingMsg();  $.post('" + url + "' + ids, function (data) {  dialogObj.dialog('destroy'); if (data.success){ reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); setTimeout(function(){$('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid');},0); } } else { showErrorMsg(data.message); } }); });";
            if (string.IsNullOrEmpty(customName))
            {
                customName = Msg.Delete;
            }
            return BodyButtonA(html, customName, script, authorizeCode, className);
        }

        //审核
        public static MvcHtmlString BodyButton_Approval(this HtmlHelper html, string url, string authorizeCode = null, string className = "audit", string gridID = "gridList")
        {
            if (className == null) className = "audit";
            string script = " var displayset = this; var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) {  showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + Msg.Approval_Confirm + "', function () { displayset.style.display ='none'; var dialogObj = showWaitingMsg(); $.post('" + url + "' + ids, function (data) { dialogObj.dialog('destroy'); if (data.success){  reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); $('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid'); } } else {showErrorMsg(data.message); }displayset.style.display = 'inline'; }); });";
            return BodyButtonA(html, Msg.Approval, script, authorizeCode, className);
        }
        //撤销审核
        public static MvcHtmlString BodyButton_Revoke(this HtmlHelper html, string url, string authorizeCode = null, string className = "revoke", string gridID = "gridList", string btnText = null, string alertMsg = null)
        {
            if (className == null) className = "revoke";
            if (string.IsNullOrEmpty(gridID)) gridID = "gridList";
            if (string.IsNullOrEmpty(btnText)) btnText = Msg.Revoke;
            if (string.IsNullOrEmpty(alertMsg)) alertMsg = Msg.ApprovalCancel_Confirm;
            string script = " var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + alertMsg + "', function () {var dialogObj = showWaitingMsg();  $.post('" + url + "' + ids, function (data) {dialogObj.dialog('destroy');  if (data.success){ reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); $('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid'); } } else { showErrorMsg(data.message); }  }); });";
            return BodyButtonA(html, btnText, script, authorizeCode, className);
        }

        public static MvcHtmlString BodyButton_Print(this HtmlHelper html, string url, string authorizeCode = null, string className = "print", string gridID = "gridList")
        {
            if (className == null) className = "print";
            if (string.IsNullOrWhiteSpace(gridID)) gridID = "gridList";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } window.open('" + url + "'+ ids, '', 'width=649,height=800, top=0, left=0, toolbar=no, menubar=no, resizable=yes,location=no, status=no');";
            return BodyButtonA(html, Msg.Print, script, authorizeCode, className);
        }
        //导出
        public static MvcHtmlString BodyButton_Export(this HtmlHelper html, string url, string authorizeCode = null, string className = "exportExcel", string gridID = "gridList", string hidFrameID = "submitZone")
        {
            if (className == null) className = "exportExcel";
            if (hidFrameID == null) className = "submitZone";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showConfirmMsg('" + Msg.Export_Confirm + "', function () {  $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }); }else { $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }";//合并(适用于加密导出)
            return BodyButtonA(html, Msg.Export, script, authorizeCode, className);
        }
        /// <summary>
        /// 自定义名称导出
        /// </summary>
        /// <param name="html"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="authorizeCode"></param>
        /// <param name="className"></param>
        /// <param name="gridID"></param>
        /// <param name="hidFrameID"></param>
        /// 注意导出名称不易过长（因为是get请求）
        /// <returns></returns>
        public static MvcHtmlString BodyButton_Exports(this HtmlHelper html, string url, string name = "Export", string authorizeCode = null, string className = "exportExcel", string gridID = "gridList", string hidFrameID = "submitZone")
        {
            if (className == null) className = "exportExcel";
            if (hidFrameID == null) className = "submitZone";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showConfirmMsg('" + Msg.Export_Confirm + "', function () {  $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }); }else { $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }";//合并(适用于加密导出)
            return BodyButtonA(html, name, script, authorizeCode, className);
        }
        #endregion
        /*bodyButton*/

        public static MvcHtmlString PopupButton(this HtmlHelper html, string value, string url, string target)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"window.open('{1}','pop')\" />", value, url));
        }
        public static MvcHtmlString RedirectButton(this HtmlHelper html, string value, string url)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"location.href='{1}';\" />", value, url));
        }

        public static string StripQuote(this HtmlHelper html, string value)
        {
            string result = value;
            result = result.Replace("'", "");
            result = result.Replace("\"", "");
            return result;
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            IEnumerable<string> radioButtons = selectList.Select<SelectListItem, string>(
                item => "<label class='inline'>" + html.RadioButton(name, item.Value, item.Selected, htmlAttributes) + item.Text + "</label>"
                );
            return MvcHtmlString.Create(string.Join("", radioButtons.ToArray()));
        }
        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name, object htmlAttributes)
        {
            return RadioButtonList(html, name, (IEnumerable<SelectListItem>)html.ViewData[name], htmlAttributes);
        }
        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name)
        {
            return RadioButtonList(html, name, null);
        }


        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            int i = 1;
            IEnumerable<string> radioButtons = selectList.Select<SelectListItem, string>(
                item => html.CheckBox(item.Text, name, name + (i++).ToString(), item.Value, item.Selected, htmlAttributes).ToHtmlString()
                );
            return MvcHtmlString.Create(string.Join("", radioButtons.ToArray()));
        }
        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name, object htmlAttributes)
        {
            return CheckboxList(html, name, (IEnumerable<SelectListItem>)html.ViewData[name], htmlAttributes);
        }
        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name)
        {
            return CheckboxList(html, name, null);
        }
        public static MvcHtmlString CheckBox(this HtmlHelper html, string label, string name, string id = null, string value = "True", bool isChecked = false, object htmlAttributes = null)
        {
            if (id == null) id = name;
            StringBuilder sb = new StringBuilder();
            sb.Append("<label for=\"").Append(id).Append("\">");
            sb.Append("<input type=\"checkbox\" id=\"").Append(id).Append("\" alt=\"").Append(label).Append("\" name=\"").Append(name).Append("\" value=\"").Append(value).Append("\" ").Append(isChecked ? "checked=\"checked\" " : " ").Append(HtmlAttributesToString(htmlAttributes)).Append(" />");
            sb.Append(label);
            sb.Append("</label>");
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string HtmlAttributesToString(object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return "";
            }
            else
            {
                string temp = htmlAttributes.ToString();
                if (string.Compare(temp, "System.Object", false) == 0)
                {
                    return "";
                }
                else
                {
                    return temp.Substring(1, temp.Length - 2);
                }
            }
        }

        public static MvcHtmlString HiddenFrame(this HtmlHelper html, string name = "submitZone", string parameters = "")
        {
            return MvcHtmlString.Create(string.Format("<iframe src='' style='display:none;' name='{0}' id='{0}' {1}></iframe>", name, parameters));
        }
    }
}