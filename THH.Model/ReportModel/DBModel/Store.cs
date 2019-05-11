using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model.ReportModel.DBModel
{
    [Table("Store")]
    public class Store : BaseModel
    {
        public override int Id { get; set; }
        /// <summary>
        /// 商店名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 商店编号
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
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
