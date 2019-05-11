using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using THH.Core.Ftp;
using THH.Core.Log;
using THH.Model;

namespace THH.Service
{
    public class FtpService
    {
        public static FtpModel ftpModel = null;
        public FtpService()
        {
            ftpModel = new FtpModel
            {
                FtpUrl = WebConfigurationManager.AppSettings["ftpUrl"] ?? "",
                FtpPort = WebConfigurationManager.AppSettings["ftpPort"] ?? "",
                FtpUser = WebConfigurationManager.AppSettings["ftpUser"] ?? "",
                FtpPwd = WebConfigurationManager.AppSettings["ftpPwd"] ?? "",
                FtpPath = WebConfigurationManager.AppSettings["ftpPath"] ?? "",
                FtpFileName = WebConfigurationManager.AppSettings["ftpFileName"] ?? "",
                LocalPath = WebConfigurationManager.AppSettings["localPath"] ?? "",
            };
        }

        [Obsolete("废弃使用SFPT")]
        public void UploadFileOld()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string ftphost = string.Format("{0}:{1}", ftpModel.FtpUrl, ftpModel.FtpPort);
                string path = PathHelper.MergeUrl(ftphost, ftpModel.FtpPath);
                string upftphost = string.Format("{0}:{1}", ftpModel.UploadFtpUrl, ftpModel.UploadFtpPort);
                string uppath = PathHelper.MergeUrl(upftphost, ftpModel.UploadFtpPath);
                //判断文件是否已存在
                //获取本地FTP和远程FTP文件
                var downFile = FtpHelper.GetFileList(path, ftpModel.FtpUser, ftpModel.FtpPwd);
                var upLoadFile = FtpHelper.GetFileList(uppath, ftpModel.UploadFtpUser, ftpModel.UploadFtpPwd);
                //查看文件差集
                var list = downFile.Except(upLoadFile).ToList();
                foreach (var item in list)
                {
                    //下载文件
                    Task.Run(() =>
                   {
                       var downloadByte = FtpHelper.DownLoadFile(path, item, ftpModel.FtpUser, ftpModel.FtpPwd);
                       FtpHelper.UpLoadFile(downloadByte, item, uppath, ftpModel.UploadFtpUser, ftpModel.FtpPwd);
                   });
                    //判断并存储
                }
                Logger.LogInfo($"UploadFile-------{sw.ElapsedMilliseconds}");
                sw.Stop();
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}=>{System.Reflection.MethodBase.GetCurrentMethod().Name} ：{ex.Message}");
            }

        }

        /// <summary>
        /// Sftp操作
        /// </summary>
        public void SftpUploadFile()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string ftphost = string.Format("{0}:{1}", ftpModel.FtpUrl, ftpModel.FtpPort);
                string path = PathHelper.MergeUrl(ftphost, ftpModel.FtpPath);
                //判断文件是否已存在
                //获取本地文件和远程SFTP文件
                var sftp = new SFtpHelper(ftpModel.FtpUrl, ftpModel.FtpPort, ftpModel.FtpUser, ftpModel.FtpPwd);
                var serverFiles = sftp.GetFileList(ftpModel.FtpPath, ftpModel.FtpFileName);
                var localurl = PathHelper.MapPath;
                var localFiles = new ArrayList();
                var localpath = PathHelper.MergeUrl(localurl, ftpModel.LocalPath);
                if (!Directory.Exists(localpath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(localpath);
                    directoryInfo.Create();
                }

                DirectoryInfo files = new DirectoryInfo(PathHelper.MergeUrl(localurl, ftpModel.LocalPath));
                FileInfo[] fileInfos = files.GetFiles();
                foreach (var file in fileInfos)
                    localFiles.Add(file.Name);

                //查看文件差集
                var list = serverFiles.ToArray().Except(localFiles.ToArray()).ToList();

                //下载文件
                sftp.Get(ftpModel.FtpPath, list, localpath);

                Logger.LogInfo($"UploadFile-------{sw.ElapsedMilliseconds}");
                sw.Stop();
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}=>{System.Reflection.MethodBase.GetCurrentMethod().Name} ：{ex.Message}");
            }

        }
    }
}
