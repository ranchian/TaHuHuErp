using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Model
{
    [Table("User")]
    public class User : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string LogingName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
