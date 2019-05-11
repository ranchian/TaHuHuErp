using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.DAL.UnitOfWork
{
    /// <summary>
    ///业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///获取 当前单元操作是否已被提交
        /// </summary>
        bool IsCommitted { get; }

        #region 方法

        /// <summary>
        ///提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        ///把当前单元操作回滚成未提交状态
        /// </summary>
        void Rollback();

        #endregion
    }
}
