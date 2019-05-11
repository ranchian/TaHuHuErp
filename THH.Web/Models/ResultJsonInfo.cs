using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THH.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultJsonInfo
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 返回异常信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}