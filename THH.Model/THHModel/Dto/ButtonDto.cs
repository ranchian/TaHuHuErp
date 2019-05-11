using System.ComponentModel.DataAnnotations;

namespace THH.Model.Dto
{
    public class ButtonDto : BaseDto
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ButtonName { get; set; }
        /// <summary>
        /// 按钮Code
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ButtonCode { get; set; }
        /// <summary>
        /// 按钮图标
        /// </summary>
        [MaxLength(100)]
        public string ButtonIocn { get; set; }
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
        [MaxLength(100)]
        public string ButtonStyle { get; set; }
    }
}
