using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using THH.Core;
using THH.DAL.ReportContext;

namespace THH.DAL
{
    public class DbContextFactory
    {
        public static BaseDbContext GetCurrentContext(DataBaseType dataBaseType = DataBaseType.Sql)
        {
            BaseDbContext baseDbContext = CallContext.GetData("ERPContext") as BaseDbContext;
            switch (dataBaseType)
            {
                case DataBaseType.Sql:
                    if (baseDbContext == null)
                    {
                        baseDbContext = new BaseDbContext();
                        CallContext.SetData("ERPContext", baseDbContext);
                    }
                    break;
                case DataBaseType.MySql:
                    break;
                case DataBaseType.Oracle:
                    break;
                case DataBaseType.Redis:
                    break;
                default:
                    break;
            }
            return baseDbContext;
        }
    }
}
