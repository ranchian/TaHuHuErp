using System;
using System.Web;

namespace THH.Core.Ftp
{
    public class PathHelper
    {
        public static bool isWeb
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Request != null;
            }
        }

        public static string AppPath
        {
            get
            {
                return PathHelper.isWeb ? HttpContext.Current.Request.ApplicationPath : (string)null;
            }
        }

        public static string MapPath
        {
            get
            {
                return PathHelper.isWeb ? HttpContext.Current.Server.MapPath(PathHelper.AppPath) : AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string MergePathName(string path, string sub)
        {
            path = path.Trim();
            sub = sub.Trim();
            if (!path.EndsWith("\\"))
                path += "\\";
            return path + sub;
        }

        public static string GetConfigPath()
        {
            return PathHelper.MergePathName(PathHelper.MapPath, "Config");
        }

        public static string MergeUrl(string path, string sub)
        {
            path = path.Trim();
            sub = sub.Trim();
            if (!path.EndsWith("/"))
                path += "/";
            return path + sub;
        }
    }
}
