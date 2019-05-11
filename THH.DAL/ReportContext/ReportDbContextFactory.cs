using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using THH.Core;

namespace THH.DAL.ReportContext
{
    public class ReportDbContextFactory
    {
        public static ReportDbContext GetCurrentContext(DataBaseType dataBaseType = DataBaseType.Sql)
        {
            ReportDbContext baseDbContext = CallContext.GetData("ReportContext") as ReportDbContext;
            switch (dataBaseType)
            {
                case DataBaseType.Sql:
                    if (baseDbContext == null)
                    {
                        baseDbContext = new ReportDbContext();
                        CallContext.SetData("ReportContext", baseDbContext);
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
