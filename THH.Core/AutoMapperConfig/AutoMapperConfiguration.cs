using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;

namespace THH.Core.AutoMapperConfig
{
    /// <summary>
    /// AutoMapper的全局实体映射配置静态类
    /// </summary>
    //public static class AutoMapperExtension
    //{
    //    /// <summary>
    //    /// 对象到对象
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>

    //    public static T MapTo<T>(this object obj)
    //    {
    //        if (obj == null) return default(T);
    //        Mapper.Initialize(ctx => ctx.CreateMap(obj.GetType(), typeof(T)));
    //        return Mapper.Map<T>(obj);
    //    }

    //    /// <summary>
    //    /// 集合到集合
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>

    //    public static List<T> MapTo<T>(this IEnumerable obj)
    //    {
    //        try
    //        {
    //            if (obj == null) throw new ArgumentNullException();
    //            Mapper.Initialize(ctx => ctx.CreateMap(obj.GetType(), typeof(T)));
    //            return Mapper.Map<List<T>>(obj);
    //        }
    //        catch (Exception ex)
    //        {
    //            string msg = ex.Message;
    //            throw;
    //        }
    //    }
    //}
}
