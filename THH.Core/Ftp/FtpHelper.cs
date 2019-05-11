using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using THH.Core.Log;

namespace THH.Core.Ftp
{
    /// <summary>
    /// FTP帮助类
    /// </summary>
    public class FtpHelper
    {
        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filestream">文件流</param>
        /// <param name="filename">文件名</param>
        /// <param name="ftpPath">文件路径</param>
        /// <param name="ftpUser">FTP服务器用户名</param>
        /// <param name="ftpPassword">FTP服务器用户密码</param>
        public static void UpLoadFile(byte[] filestream, string filename, string ftpPath, string ftpUser, string ftpPassword)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename is empty");

            if (ftpUser == null)
            {
                ftpUser = "";
            }
            if (ftpPassword == null)
            {
                ftpPassword = "";
            }

            //if (!File.Exists(localFile))
            //{
            //    //MyLog.ShowMessage("文件：“" + localFile + "” 不存在！");
            //    return;
            //}

            FtpWebRequest ftpWebRequest = null;
            MemoryStream localFileStream = null;
            Stream requestStream = null;
            try
            {
                ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(PathHelper.MergeUrl(ftpPath, filename));
                ftpWebRequest.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpWebRequest.ContentLength = filestream.Length;
                int buffLength = 4096;
                byte[] buff = new byte[buffLength];
                int contentLen;
                //localFileStream = new FileInfo(localFile).OpenRead();
                localFileStream = new MemoryStream(filestream);
                requestStream = ftpWebRequest.GetRequestStream();
                contentLen = localFileStream.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    requestStream.Write(buff, 0, contentLen);
                    contentLen = localFileStream.Read(buff, 0, buffLength);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"ftp upload failed :{ex}");
                throw ex;
                //MyLog.ShowMessage(ex.Message, "FileUpLoad0001");
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
                if (localFileStream != null)
                {
                    localFileStream.Close();
                }
            }
        }
        #endregion

        #region 文件夹管理

        ///// <summary>
        ///// 新建目录
        ///// </summary>
        ///// <param name="ftpIp">FTP服务器地址</param>
        ///// <param name="ftpPath">目录名</param>
        ///// <param name="ftpUser">FTP服务器用户名</param>
        ///// <param name="ftpPassword">FTP服务器用户密码</param>
        public static void MakeDir(string ftpIp, string ftpPath, string ftpUser, string ftpPassword)
        {

            string fullDir = FtpParseDirectory(PathHelper.MergeUrl(ftpPath, String.Empty));
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            foreach (var dir in dirs)
            {
                //如果是以/开始的路径,第一个为空    
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(ftpIp, curDir, ftpUser, ftpPassword);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        public static string FtpParseDirectory(string ftpPath)
        {
            if (!ftpPath.EndsWith("/"))
                return ftpPath;
            return ftpPath.Substring(0, ftpPath.LastIndexOf("/"));
        }

        /// <summary>
        /// 创建目录 
        /// </summary> 
        public static Boolean FtpMakeDir(string ftpIp, string curDir, string ftpUser, string ftpPassword)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpIp + curDir);
            req.Credentials = new NetworkCredential(ftpUser, ftpPassword);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="ftpPath">FTP服务器地址</param>
        /// <param name="dirName">目录名</param>
        /// <param name="ftpUser">FTP服务器用户名</param>
        /// <param name="ftpPassword">FTP服务器用户密码</param>
        /// <returns></returns>
        public static bool CheckDirectoryExist(string ftpPath, string dirName, string ftpUser, string ftpPassword)
        {
            bool result = false;
            try
            {
                //实例化FTP连接
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath));
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                //指定FTP操作类型为创建目录
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                //获取FTP服务器的响应
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);

                StringBuilder str = new StringBuilder();
                string line = sr.ReadLine();
                while (line != null)
                {
                    str.Append(line);
                    str.Append("|");
                    line = sr.ReadLine();
                }
                string[] datas = str.ToString().Split('|');

                for (int i = 0; i < datas.Length; i++)
                {
                    if (datas[i].Contains("<DIR>"))
                    {
                        int index = datas[i].IndexOf("<DIR>");
                        string name = datas[i].Substring(index + 5).Trim();
                        if (name == dirName)
                        {
                            result = true;
                            break;
                        }
                    }
                }

                sr.Close();
                sr.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"ftp upload failed{ex}");
                throw ex;
            }
            return result;
        }


        public static byte[] DownLoadFile(string ftpPath, string fileName, string ftpUser, string ftpPassword, string savePath = "")
        {
            try
            {
                FileStream outputStream = null;
                if (!string.IsNullOrEmpty(savePath))
                    outputStream = new FileStream(savePath, FileMode.Create);

                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(PathHelper.MergeUrl(ftpPath, fileName)));
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream?.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream?.Close();
                response.Close();
                return buffer;
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"DownLoadFile Error {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取FTP文件列表(包括文件夹)
        /// </summary>   
        private static string[] GetAllList(string url, string ftpUser, string ftpPassword)
        {
            List<string> list = new List<string>();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(new Uri(url));
            req.Credentials = new NetworkCredential(ftpUser, ftpPassword);
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            req.UseBinary = true;
            req.UsePassive = true;
            try
            {
                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"Ftp => GetAllList Error : {ex.Message}");
                throw (ex);
            }
            return list.ToArray();
        }

        /// <summary>  
        /// 获取当前目录下文件列表(不包括文件夹)  
        /// </summary>  
        public static string[] GetFileList(string url, string ftpUser, string ftpPassword)
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {

                    if (line.IndexOf("<DIR>") == -1)
                    {
                        result.Append(Regex.Match(line, @"[\S]+ [\S]+", RegexOptions.IgnoreCase).Value.Split(' ')[1]);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                if (result.Length > 0)
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"Ftp => GetFileList Error : {ex.Message}");
            }
            return result.ToString().Split('\n');
        }
        #endregion
    }
}
