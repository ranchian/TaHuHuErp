
namespace THH.Model.Dto
{
    public class GoodsDto : BaseDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 商品原价
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 商品会员价
        /// </summary>
        public string VipPrice { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public int IfShelves { get; set; }

        /// <summary>
        /// 是否促销
        /// </summary>
        public int IfPromotion { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
    }
}
