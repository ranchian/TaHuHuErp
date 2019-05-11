using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("Machine")]
    public class Machine : BaseModel
    {
        public override int Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(500)]
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// 市场
        /// </summary>
        [Required]
        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
