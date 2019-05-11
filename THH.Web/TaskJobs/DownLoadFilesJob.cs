using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using THH.Core.Log;
using THH.Service;

namespace THH.Web.TaskJobs
{
    public class DownLoadFilesJob : IJob
    {
        public async Task  Execute(IJobExecutionContext context)
        {
            try
            {
                new FtpService().SftpUploadFile();
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}=>{System.Reflection.MethodBase.GetCurrentMethod().Name} ：{ex.Message}");
            }
        }
    }
}