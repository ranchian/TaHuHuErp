using System;
using THH.Core.Model;

namespace THH.Service
{
    public class ImportService
    {
        #region Excel导入
        public ImportReturn ImportExcel(string fileData, string t, int uId, int depId = -1, string para = "", string userSeller = null, string saleSite = null, DateTime? dateFrom = null, DateTime? dateTo = null, string currency = null, string accountPeriod = null, string collectAccount = null, string postTypeCode = null)
        {
            string filePath = "";
            ImportReturn result = new ImportReturn();
            try
            {
                switch (t)
                {
                    case "Product":
                        //var productSvc;
                        //result = productSvc.ImportExcel(filePath);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(filePath))
                    System.IO.File.Delete(filePath);
                result.Success = false;
            }
            return result;
        }
        #endregion

        #region 模板下载

        public string DownLoadTemplate(string t, string para = "", string serverPath = "")
        {
            string templatePath = serverPath;
            if (!string.IsNullOrEmpty(t))
            {
                switch (t)
                {
                    case "Product":
                        templatePath += "产品导入模板.xls";
                        break;
                    default:
                        break;
                }
            }
            return templatePath;
        }
        #endregion
    }
}
