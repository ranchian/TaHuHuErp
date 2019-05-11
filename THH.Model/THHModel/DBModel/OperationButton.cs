using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Model
{
    [Table("OperationButton")]
    public class OperationButton : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ButtonName { get; set; }
        /// <summary>
        /// 按钮Code
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ButtonCode { get; set; }
        /// <summary>
        /// 按钮状态
        /// </summary>
        [MaxLength(10)]
        public string Status { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        [MaxLength(200)]
        public string InputType { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        [MaxLength(200)]
        public string Style { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
    }
}
