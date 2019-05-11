using AutoMapper;
using THH.Web.ModelAutoMapper;

namespace THH.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
                cfg.AddProfile<ReportMapping>();
            });
        }

    }
}