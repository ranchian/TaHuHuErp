using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace THH.Model
{
    [Table("Goods")]
    public class Goods : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public override int Id { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [MaxLength(255)]
        public string Picture { get; set; }

        /// <summary>
        /// 商品原价
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 商品会员价
        /// </summary>
        public decimal VipPrice { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IfShelves { get; set; }

        /// <summary>
        /// 是否促销
        /// </summary>
        public bool IfPromotion { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(255)]
        public string Introduction { get; set; }
    }
}