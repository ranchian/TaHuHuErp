using AutoMapper;
using System;
using System.Reflection;

namespace THH.Core.AutoMapperConfig
{
    public class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMapping<AutoMapFromAttribute>(type);
            CreateMapping<AutoMapToAttribute>(type);
            CreateMapping<AutoMapAttribute>(type);
        }

        public static void Register()
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());      //最后初始化注册一次
        }

        private static void CreateMapping<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapAttr in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapAttr.TargetTypes == null || autoMapAttr.TargetTypes.Length < 0)
                {
                    continue;
                }

                foreach (var targetType in autoMapAttr.TargetTypes)
                {
                    if (autoMapAttr.Direciton.HasFlag(AutoMapDirection.To))
                    {
                        AutoMapperProfile.AddMapping(type, targetType);
                    }
                    if (autoMapAttr.Direciton.HasFlag(AutoMapDirection.From))
                    {
                        AutoMapperProfile.AddMapping(targetType, type);
                    }
                }
            }
        }
    }
}
