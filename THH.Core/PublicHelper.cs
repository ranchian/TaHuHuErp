using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace THH.Core
{
    /// <summary>
    ///公共辅助操作类
    /// </summary>
    public static class PublicHelper
    {
        #region 公共方法
        /// <summary>
        /// 人民转换为大写
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ConvertToChineseRMB(decimal amount)
        {
            string strAmount = amount.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string regex = Regex.Replace(strAmount, @"((?<=-|^)[^\-1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            string amountRMB = "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰";
            string UpperRMB = Regex.Replace(regex, ".", m => amountRMB[m.Value[0] - '-'].ToString());
            if (UpperRMB.EndsWith("元"))
                UpperRMB = UpperRMB + "整";
            return UpperRMB;
        }
        #endregion
    }
}
