using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THH.DAL;

namespace THH.Service.ReportService
{
    public class ReportBaseService: BaseService
    {
        public  readonly EnumConnection enumConnection = EnumConnection.ReportContext;
    }
}
