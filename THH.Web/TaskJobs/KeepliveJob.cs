using Quartz;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using THH.Core.Log;

namespace THH.Web.TaskJobs
{
    public class KeepliveJob : IJob
    {
        /// <summary>
        /// 防止IIS回收
        /// </summary>
        /// <param name="context"></param>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                string url = WebConfigurationManager.AppSettings["ArborDayUrl"];
                new WebClient
                {
                    Encoding = Encoding.UTF8
                }.DownloadString(url);
                Logger.LogInfo("KeepliveJob: OK");
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"KeepliveJob:{ex}");
            }
        }

    }
}