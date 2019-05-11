using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("StoreReport")]
    public class StoreReport : BaseModel
    {
        public override int Id { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string StoreName { get; set; }
        /// <summary>
        /// 商户编号
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string StoreNo { get; set; }
        /// <summary>
        /// 支付类型(支付通道)
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string PaymentType { get; set; }
        /// <summary>
        /// 交易成功笔数
        /// </summary>
        [Required]
        public int Quantity { get; set; }
        /// <summary>
        /// 交易成功金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
        /// <summary>
        /// 退款笔数
        /// </summary>
        public int RefundQuantity { get; set; }
        /// <summary>
        /// 退款成功金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 交易净额
        /// </summary>
        public decimal NetAmount { get; set; }
        /// <summary>
        /// 商户费率
        /// </summary>
        public string StoreRate { get; set; }
        /// <summary>
        /// 商户手续费
        /// </summary>
        public decimal StoreFee { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }
}
