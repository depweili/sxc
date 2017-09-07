using SXC.Code;
using SXC.Code.Utility;
using SXC.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SXC.Services
{
    public static class Function
    {
        public static string GetHostAndApp()
        {

            var scheme = HttpContext.Current.Request.Url.Scheme;
            var authority = HttpContext.Current.Request.Url.Authority;
            var applicationPath = HttpContext.Current.Request.ApplicationPath;

            var url = scheme + "://" + authority + (applicationPath == "/" ? "" : applicationPath);

            return url;
        }

        public static string GetPicUrl(string pic)
        {
            var host = GetHostAndApp();
            //Server.MapPath();
            //var url = host + @"/SxcWebApi/api/Image/" + Cryptography.Base64ForUrlEncode(pic);
            var url = host + @"/api/Image/" + Cryptography.Base64ForUrlEncode(pic);
            //return @"http://192.168.31.199/SxcWebApi/api/Image/" + Cryptography.Base64ForUrlEncode(pic);
            return url;
        }

        public static string GetImageDirectory()
        {
            var imgDir = ConfigHelper.GetSetting("ImagesPhysicalPath");

            if (string.IsNullOrEmpty(imgDir)) {
                imgDir = HttpContext.Current.Server.MapPath("~/Images") + @"\";
            }

            return imgDir;
        }

        public static string GetImagePath(string img)
        {
            var imgDir = GetImageDirectory();

            var imgPath = imgDir + img;

            return imgPath;
        }

        public static bool IsInTreeBySql(DbContext db,int cid, int tid, string table, string key = "ID", string pkey = "PID")
        {
            var supsql = string.Format(@"WITH temp AS
                                        (
                                        SELECT {0} FROM {1}  WHERE {0} = {3}
                                        UNION ALL
                                        SELECT d.{0} FROM {1} AS d
                                        INNER JOIN temp ON d.{2} = temp.{0}
                                        )
                                        SELECT * FROM temp", key, table, pkey, cid);

            var list = db.Database.SqlQuery<int>(supsql);

            return list.ToList().Contains(tid);


 
        }
    }
}
