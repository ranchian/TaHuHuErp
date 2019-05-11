using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    /// <summary>
    /// 积分
    /// </summary>
    [Table("PointTxnDetail")]
    public class PointTxnDetail : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string ProfileId { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string SerialNo { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string MerchantName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HostRef { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime TxnDate { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        public DateTime SettleDate { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string PaymentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tid { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string Discount { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }
        [MaxLength(10)]
        [Required]
        public string Status { get; set; }
    }
}
