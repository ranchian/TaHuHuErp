using AutoMapper;
using System;
using System.Collections.Generic;

namespace THH.Core.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        private static Dictionary<Type, Type> mapDics = new Dictionary<Type, Type>();
        public AutoMapperProfile()
        {
            foreach (var key in mapDics.Keys)
            {
                CreateMap(key, mapDics[key]);   //创建映射关系
            }
        }
        /// <summary>
        /// 添加映射
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="destinationType">目标类型</param>
        public static void AddMapping(Type sourceType, Type destinationType)
        {
            mapDics.Add(sourceType, destinationType);
        }
    }
}
