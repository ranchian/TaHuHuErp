using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace THH.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入账号")]
        [Display(Name = "账号")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        //  [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}