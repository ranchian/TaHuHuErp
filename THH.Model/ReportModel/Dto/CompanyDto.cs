using System;
using System.ComponentModel.DataAnnotations;
using THH.Model.Dto;

namespace THH.Model.ReportModel.Dto
{
    public class CompanyDto : BaseDto
    {
        public override int Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 公司编号
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
    }
}
