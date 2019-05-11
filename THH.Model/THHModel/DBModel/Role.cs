using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THH.Model
{
    [Table("Role")]
    public class Role : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色状态
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Status { get; set; }
    }
}
