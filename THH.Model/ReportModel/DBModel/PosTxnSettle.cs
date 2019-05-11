using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("PosTxnSettle")]
    public class PosTxnSettle: BaseModel
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
        public int PointId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string TxnDate { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TxnAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string CardNo { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        [Required]
        public int Point { get; set; }
        /// <summary>
        /// 上传状态
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string Status { get; set; }
    }
}
