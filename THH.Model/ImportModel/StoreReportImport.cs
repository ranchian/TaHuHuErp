using System;

namespace THH.Model.ImportModel
{
    /// <summary>
    /// 导入模板
    /// </summary>
    public class PointTxnDetailImport
    {
        public string PROFILE_ID { get; set; }
        public string SERIAL_NO { get; set; }
        public string MERCHANT_NAME { get; set; }
        public string TXN_ID { get; set; }
        public string HOST_REF { get; set; }
        public string TXN_TYPE { get; set; }
        public string TXN_AMT { get; set; }
        public DateTime TXN_DATE { get; set; }
        public DateTime SETTLE_DATE { get; set; }
        public string CURRENCY { get; set; }
        public string PAYMENT_TYPE { get; set; }
        public string MID { get; set; }
        public string TID { get; set; }
        public string DISCOUNT { get; set; }
        public string COUPON_NAME { get; set; }
        public string STATUS { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PosTxnDetailImport
    {
        public string PROFILE_ID { get; set; }
        public string SERIAL_NO { get; set; }
        public string MERCHANT_NAME { get; set; }
        public string TXN_DATE { get; set; }
        public string TXN_COUNT { get; set; }
        public string SETTLE_DATE { get; set; }
        public string STATUS { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PosTxnSettleImport
    {
        public string PROFILE_ID { get; set; }
        public string SERIAL_NO { get; set; }
        public string MERCHANT_NAME { get; set; }
        public string POINT_ID { get; set; }
        public string TXN_DATE { get; set; }
        public string CURRENCY { get; set; }
        public string TXN_AMT { get; set; }
        public string CARD_NO { get; set; }
        public string POINT { get; set; }
        public string STATUS { get; set; }
    }
}
