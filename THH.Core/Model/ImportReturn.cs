using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Core.Model
{
    /// <summary>
    /// 导入返回值
    /// </summary>
    public class ImportReturn
    {
        public bool Success { get; set; }
        /// <summary>
        /// 导入成功条数
        /// </summary>
        public int SuccessRecord { get; set; }
        /// <summary>
        /// 导入失败条数
        /// </summary>
        public int FailRecord { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int? TotalQuantity { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
}
