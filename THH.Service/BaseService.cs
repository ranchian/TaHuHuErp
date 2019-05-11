using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using THH.Core.TablePage;

namespace THH.Service
{
    public class BaseService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<T> GetTablePagedList<T>(IQueryable<T> entitys, TablePageParameter gp)
        {
            gp.PageParameterInit(entitys);
            if (!gp.NotSort)
            {
                if (string.IsNullOrEmpty(gp.SortName))
                {
                    entitys = entitys.OrderBy("Id");
                }
                else
                {
                    try
                    {
                        entitys = entitys.OrderBy(gp.SortName + " " + gp.SortOrder); //排序
                    }
                    catch (Exception)
                    {
                        entitys = entitys.OrderBy("Id");
                    }
                }
            }
            entitys = entitys.Skip(gp.Offset).Take(gp.PageSize); //分页
            return entitys.ToList();
        }
    }
}
