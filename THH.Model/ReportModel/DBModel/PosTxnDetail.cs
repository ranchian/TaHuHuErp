using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("PosTxnDetail")]
    public class PosTxnDetail : BaseModel
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
        [Required]
        public DateTime TxnDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnCount { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        [Required]
        public DateTime SettleDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string Status { get; set; }
    }
}
