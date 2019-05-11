using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("Merchant")]
    public class Merchant : BaseModel
    {
        public override int Id { get; set; }
        /// <summary>
        /// 商场名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 商场编号
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
        /// 公司
        /// </summary>
        [Required]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
