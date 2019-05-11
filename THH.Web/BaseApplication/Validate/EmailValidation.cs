using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace THH.Core.Validate
{
    /// <summary>
    /// 邮箱自定义验证
    /// </summary>
    public sealed class EmailValidation : ValidationAttribute
    {
        public const string reg = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$";
        public EmailValidation()
        {
        }
        //重写基类方法
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            if (value is string)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            return false;
        }
    }
    /// <summary>
    /// 用来禁止属性某个值的输入
    /// </summary>
    public sealed class NoInputAttribute : ValidationAttribute, IClientValidatable
    {
        public string Input { get; set; }

        public NoInputAttribute(string input)
        {
            this.Input = input;
        }

        public override bool IsValid(object value)
        {
            //如果没有输入值，放行
            if (value == null)
            {
                return true;
            }
            if (value is string)
            {
                if (Input.Contains(value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "noinput",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["input"] = Input;
            yield return rule;
        }
    }

    /// <summary>
    /// 小数验证
    /// </summary>
    public sealed class DecimalAttribute : ValidationAttribute
    {
        public const string reg = @"^[0-9]+(.[0-9]{0,4})?$";
        public DecimalAttribute()
        {
        }
        //重写基类方法
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            if (value is decimal)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            else if (value is int)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            else if (value is double)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            else if (value is float)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            return false;
        }
    }
    /// <summary>
    /// 正整数
    /// </summary>
    public sealed class IntegerAttribute : ValidationAttribute
    {
        public const string reg = @"^[1-9]\d*$ 或 ^([1-9][0-9]*){1,3}$ 或 ^\+?[1-9][0-9]*$";
        public IntegerAttribute()
        {
        }
        //重写基类方法
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            if (value is int)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            return false;
        }
    }
}
