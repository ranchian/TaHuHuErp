

namespace THH.Model.Dto
{
    public class UserDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LogingName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        public string RealName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
