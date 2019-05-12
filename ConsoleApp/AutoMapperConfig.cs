using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THH.Model.ImportModel;
using THH.Model.ReportModel.DBModel;

namespace ConsoleApp
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ReportMapping>();
            });
        }
    }
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            //
            CreateMap<PointTxnDetailImport, PointTxnDetail>()
                .ForMember(opt => opt.CouponName, ptd => ptd.MapFrom(src => src.COUPON_NAME))
                .ForMember(opt => opt.Currency, ptd => ptd.MapFrom(src => src.CURRENCY))
                .ForMember(opt => opt.Discount, ptd => ptd.MapFrom(src => src.DISCOUNT))
                .ForMember(opt => opt.MerchantName, ptd => ptd.MapFrom(src => src.MERCHANT_NAME))
                .ForMember(opt => opt.HostRef, ptd => ptd.MapFrom(src => src.HOST_REF))
                .ForMember(opt => opt.Mid, ptd => ptd.MapFrom(src => src.MID))
                .ForMember(opt => opt.PaymentType, ptd => ptd.MapFrom(src => src.PAYMENT_TYPE))
                .ForMember(opt => opt.ProfileId, ptd => ptd.MapFrom(src => src.PROFILE_ID))
                .ForMember(opt => opt.SerialNo, ptd => ptd.MapFrom(src => src.SERIAL_NO))
                .ForMember(opt => opt.SettleDate, ptd => ptd.MapFrom(src => src.SETTLE_DATE))
                .ForMember(opt => opt.Status, ptd => ptd.MapFrom(src => src.STATUS))
                .ForMember(opt => opt.Tid, ptd => ptd.MapFrom(src => src.TID))
                .ForMember(opt => opt.TxnAmt, ptd => ptd.MapFrom(src => src.TXN_AMT))
                .ForMember(opt => opt.TxnDate, ptd => ptd.MapFrom(src => src.TXN_DATE))
                .ForMember(opt => opt.TxnId, ptd => ptd.MapFrom(src => src.TXN_ID))
                .ForMember(opt => opt.TxnType, ptd => ptd.MapFrom(src => src.TXN_TYPE));
            //
            CreateMap<PosTxnDetailImport, PosTxnDetail>()
                .ForMember(opt => opt.ProfileId, ptd => ptd.MapFrom(src => src.PROFILE_ID))
                .ForMember(opt => opt.SerialNo, ptd => ptd.MapFrom(src => src.SERIAL_NO))
                .ForMember(opt => opt.SettleDate, ptd => ptd.MapFrom(src => src.SETTLE_DATE))
                .ForMember(opt => opt.Status, ptd => ptd.MapFrom(src => src.STATUS))
                .ForMember(opt => opt.MerchantName, ptd => ptd.MapFrom(src => src.MERCHANT_NAME))
                .ForMember(opt => opt.TxnDate, ptd => ptd.MapFrom(src => src.TXN_DATE))
                .ForMember(opt => opt.TxnDate, ptd => ptd.MapFrom(src => src.TXN_COUNT));
            //
            CreateMap<PosTxnSettleImport, PosTxnSettle>()
                .ForMember(opt => opt.ProfileId, ptd => ptd.MapFrom(src => src.PROFILE_ID))
                .ForMember(opt => opt.SerialNo, ptd => ptd.MapFrom(src => src.SERIAL_NO))
                .ForMember(opt => opt.MerchantName, ptd => ptd.MapFrom(src => src.MERCHANT_NAME))
                .ForMember(opt => opt.PointId, ptd => ptd.MapFrom(src => src.POINT_ID))
                .ForMember(opt => opt.TxnDate, ptd => ptd.MapFrom(src => src.TXN_DATE))
                .ForMember(opt => opt.Currency, ptd => ptd.MapFrom(src => src.CURRENCY))
                .ForMember(opt => opt.TxnAmt, ptd => ptd.MapFrom(src => src.TXN_AMT))
                .ForMember(opt => opt.CardNo, ptd => ptd.MapFrom(src => src.CARD_NO))
                .ForMember(opt => opt.Point, ptd => ptd.MapFrom(src => src.POINT))
                .ForMember(opt => opt.Status, ptd => ptd.MapFrom(src => src.STATUS));
        }
    }
}
