using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SXC.Code.Cache;
using SXC.Code.Utility;
using SXC.Services.Impl;
using SXC.WebApi.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SXC.WebApi.Controllers
{
    public class UtilityController : ApiControllerBase
    {
        [Route("api/WxUserInfo")]
        [HttpGet]
        public IHttpActionResult GetWxUser(string code, string iv, string encryptedData)
        {
            WxHelper wh = new WxHelper();

            dynamic res = wh.GetWxUser(code, iv, encryptedData);

            return Ok(res);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        [Route("api/Image/{img}")]
        [HttpGet]
        public HttpResponseMessage GetImage(string img)
        {
            try
            {
                string key = "Image_" + img;
                byte[] imgByte;

                if (CacheHelper.Exist(key))
                {
                    imgByte = CacheHelper.Get<byte[]>(key);
                }
                else
                {
                    var service = new UtilityService();

                    string mime;

                    imgByte = service.GetImageByEncrypt(img, out mime);

                    CacheHelper.Set(key, imgByte, DateTime.Now.AddMinutes(_cacheabsoluteminutes));
                }

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgByte)
                };


                //var service = new UtilityService();

                //string mime;

                //var imgByte = service.GetImageByEncrypt(img, out mime);

                //var resp = new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    Content = new ByteArrayContent(imgByte)
                //};
                //resp.Content.Headers.ContentType = new MediaTypeHeaderValue(mime);


                return resp;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                return resp;
            }
        }

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [Route("api/QrCode/{content}")]
        [HttpGet]
        public HttpResponseMessage GetQrCode(string content)
        {
            try
            {
                var service = new UtilityService();

                var imgByte = service.GetGetQrCode(content);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgByte)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return resp;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

                return resp;
            }
        }



        


        //[Route("api/XXQrCode/{content}")]
        //[HttpGet]
        public HttpResponseMessage GetQrCodeX(string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);

            QrCode qr;

            int ModuleSize = 12;//大小

            QuietZoneModules QuietZones = QuietZoneModules.Two;  //空白区域

            MemoryStream imgStream = new MemoryStream();

            if (qrEncoder.TryEncode(content, out qr))//对内容进行编码，并保存生成的矩阵  
            {
                //Brush b = new SolidBrush(Color.FromArgb(20, Color.Gray));
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones), Brushes.Black, Brushes.Transparent);
                //render.WriteToStream(qr.Matrix, ImageFormat.Png, imgStream);

                //logo
                DrawingSize dSize = render.SizeCalculator.GetSize(qr.Matrix.Width);
                Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
                Graphics g = Graphics.FromImage(map);
                render.Draw(g, qr.Matrix);

                //logo
                //Image img = resizeImage(Image.FromFile(@"D:\code\learn\asp.net\0-part\webapi\WebApiTest\webapi1\Images\logo1.png"), new Size(100, 100));
                //img.Save(@"D:\code\learn\asp.net\0-part\webapi\WebApiTest\webapi1\Images\qrlogo.png", ImageFormat.Png);
                
                //Image img = Image.FromFile(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\qrlogo.png");

                Image img = Image.FromFile(@"D:\code\Projects\速星创\server\SXC\SXC.WebApi\Images\qrlogo.png");

                Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
                g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);

                map.Save(imgStream, ImageFormat.Png);

                img.Dispose();
                g.Dispose();

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgStream.GetBuffer())
                    //或者  
                    //Content = new StreamContent(imgStream)  
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return resp;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

        }
    }
}
