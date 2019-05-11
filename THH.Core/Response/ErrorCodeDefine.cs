using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Core.Response
{
    /// <summary>
    /// 错误码信息定义
    /// </summary>
    public class ErrorCodeDefine
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        public const string Success = "200";
        /// <summary>
        /// 服务器出错
        /// </summary>
        public const string ServerError = "500";
        /// <summary>
        /// 缺少必填参数信息
        /// </summary>
        public const string ParameterRequired = "600";
        /// <summary>
        /// 第一账户 账户信息不完整
        /// </summary>
        public const string FirstAccountMessageIntegrity = "601";
        /// <summary>
        /// 第二账户 账户信息不完整
        /// </summary>
        public const string SecondAccountMessageIntegrity = "602";
        /// <summary>
        /// 第三账户 账户信息不完整
        /// </summary>
        public const string ThirdAccountMessageIntegrity = "603";
        /// <summary>
        /// 账户不能出现重复
        /// </summary>
        public const string AccountRepeat = "605";
        /// <summary>
        /// 业务逻辑错误
        /// </summary>
        public const string LogicalError = "700";
    }
}
