using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using THH.Model.BaseModule;

namespace THH.Model
{
    /// <summary>
    /// 用户权限
    /// </summary>
    [Table("UserRights")]
    public class UserRights : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required]
        public int SysMenuId { get; set; }
        public virtual SysMenu SysMenu { get; set; }
        /// <summary>
        /// 按钮Id
        /// </summary>
        [Required]
        public int SysButtonId { get; set; }
        public virtual SysButton SysButton { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
