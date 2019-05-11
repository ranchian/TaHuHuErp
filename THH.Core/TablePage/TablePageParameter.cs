using System.Collections.Generic;
using System.Linq;

namespace THH.Core.TablePage
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class TablePageParameter
    {
        /// <summary>
        /// 每页显示的行数
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 偏移量
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 页总数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 行总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 每页显示的行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序列名
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序方式(asc/desc)
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// 不排序
        /// </summary>
        /// <remarks>
        /// </remarks>
        public bool NotSort { get; set; }

        public TablePageParameter() { }
        public TablePageParameter(int pageSize, string sortName, string sortOrder)
        {
            this.PageSize = pageSize;
            this.SortName = sortName;
            this.SortOrder = sortOrder;
        }
        /// <summary>
        ///初始化
        /// </summary>
        public void PageParameterInit<T>(IQueryable<T> entitys)
        {
            TotalCount = entitys.Count();
            TotalPages = (TotalCount - 1) / Limit + 1;
            if (Limit <= 0)
            {
                PageSize = Defaults.PageSize;
            }
            else
            {
                PageSize = Limit;
            }
        }
    }
}
