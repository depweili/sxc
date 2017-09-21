using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SXC.WebApi.Utility;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SXC.Services.Dto;
using Newtonsoft.Json;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var AesIV = "r7BXXKkLb8qrSNn05n0qiA==";
            var AesKey = "tiihtNczf5v6AKRyjwEUhQ==";
            string text ="CiyLU1Aw2KjvrjMdj8YKliAjtP4gsMZM" +
    "QmRzooG2xrDcvSnxIMXFufNstNGTyaGS" +
    "9uT5geRa0W4oTOb1WT7fJlAC+oNPdbB+" +
    "3hVbJSRgv+4lGOETKUQz6OYStslQ142d" +
    "NCuabNPGBzlooOmB231qMM85d2/fV6Ch" +
    "evvXvQP8Hkue1poOFtnEtpyxVLW1zAo6" +
    "/1Xx1COxFvrc2d7UL/lmHInNlxuacJXw" +
    "u0fjpXfz/YqYzBIBzD6WUfTIF9GRHpOn" +
    "/Hz7saL8xz+W//FRAUid1OksQaQx4CMs" +
    "8LOddcQhULW4ucetDf96JcR3g0gfRK4P" +
    "C7E/r7Z6xNrXd2UIeorGj5Ef7b1pJAYB" +
    "6Y5anaHqZ9J6nKEBvB4DnNLIVWSgARns" +
    "/8wR2SiRS7MNACwTyrGvt9ts8p12PKFd" +
    "lqYTopNHR1Vf7XjfhQlVsAJdNiKdYmYV" +
    "oKlaRv85IfVunYzO0IKXsyl7JCUjCpoG" +
    "20f0a04COwfneQAGGwd5oa+T8yO5hzuy" +
    "Db/XcxxmK01EpqOyuxINew==";
            string ss = new WxHelper().AESDecrypt(text, AesKey, AesIV);

            Console.Write(ss);
        }

        [TestMethod]
        public void TestEncode()
        {
            string pic = "wx12a7e3bf7a31d815";

            string encode = Base64ForUrlEncode(pic);

            string decode = Base64ForUrlDecode(encode);
        }

        public static string Base64ForUrlEncode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }
        public static string Base64ForUrlDecode(string str)
        {
            byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
            return Encoding.UTF8.GetString(decbuff);
        }

        [TestMethod]
        public void TestMime()
        {
            string file = "404.jpg";

            string contentType = MimeMapping.GetMimeMapping(file);

            contentType = MimeMapping.GetMimeMapping("aaa.png");
            contentType = MimeMapping.GetMimeMapping("aaa.txt");
            contentType = MimeMapping.GetMimeMapping("aaa.gif");
            contentType = MimeMapping.GetMimeMapping("aa.xlsx");
        }


        [TestMethod]
        public void TestUniqueKey()
        {
            for (int i = 0; i < 10; i++)
            {
                string key = GetUniqueKey(8);
                Console.WriteLine(key);
            }
            
        }

        public static string GetUniqueKey(int size)
        {
            char[] chars = new char[62];
            string a;
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        [TestMethod]
        public void TestresizeImage()
        {
            //Image img = resizeImage(Image.FromFile(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\qrlogo.png"), new Size(80, 80));

            //img.Save(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\qrlogonew.png", ImageFormat.Png);

            Image img = resizeImage(Image.FromFile(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\sxclogonew.png"), new Size(100, 100));

            img.Save(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\sxclogonew1.png", ImageFormat.Png);

            //img.Dispose();
            
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }


        [TestMethod]
        public void TestJson1()
        {

            AgentDto t = new AgentDto();

            t.commission = 1.234454M;

            string json = JsonConvert.SerializeObject(t);
        }

        
    }


}
