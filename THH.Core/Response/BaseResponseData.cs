using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Core.Response
{
    /// <summary>
    /// Json交互数据格式
    /// </summary>
    public class BaseResponseData
    {
        public BaseResponseData()
        {
            this.ErrorCode = ErrorCodeDefine.Success;
        }
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUrl { get; set; }

    }
}
