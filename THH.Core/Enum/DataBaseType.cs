using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THH.Core
{
    public enum DataBaseType
    {
        [Description("Sql")]
        Sql = 0,
        [Description("MySql")]
        MySql = 1,
        [Description("Oracle")]
        Oracle = 2,
        [Description("Redis")]
        Redis = 3,
        [Description("PostgreSQL")]
        PostgreSQL = 4,
        [Description("Mongodb")]
        Mongodb = 5,
        [Description("Redis")]
        Sqlite = 6,
    }
}
