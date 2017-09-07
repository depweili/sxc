using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Text;
using System.Web.Http;
using SXC.Code.Utility;
using SXC.Services;
using System.Dynamic;

namespace SXC.WebApi.Controllers
{
    public class TestController : ApiController
    {
        #region 废弃
        //[Route("api/xxxNavigationsold")]
        [HttpGet]
        public IHttpActionResult GetNavigations1()
        {
            var service = new NavigationService();
            //List<Navigation> res = service.GetAll();
            //if (res == null)
            //{
            //    return NotFound();
            //}

            ////var res = new { product.Name, product.Price };

            //return Ok(res);
            var res = service.GetJsonAll();

            return Ok<object>(res);
        }

        //[Route("api/xxxNavigations")]
        [HttpGet]
        public HttpResponseMessage GetNavigationsXX()
        {
            var service = new NavigationService();
            //List<Navigation> res = service.GetAll();
            //if (res == null)
            //{
            //    return NotFound();
            //}

            ////var res = new { product.Name, product.Price };

            //return Ok(res);
            var res = service.GetJsonAll();

            //var res1 = service.CreateNavigations(new SXC.Core.Models.Navigation());

            return new HttpResponseMessage { Content = new StringContent(res, Encoding.GetEncoding("UTF-8"), "application/json") };

            //return Json(res);
        }
        #endregion


        //[Route("api/WxUserXXX")]
        [HttpGet]
        public IHttpActionResult GetWxUser(string code, string iv, string encryptedData)
        {
            string Appid = "wx12a7e3bf7a31d815";
            string Secret = ConfigHelper.GetSetting("Wx_Secret");//ConfigurationManager.AppSettings["Wx_Secret"]; //"b4722d8a6a5629ed7717e58d1af431ba"; 
            string grant_type = "authorization_code";


            //向微信服务端 使用登录凭证 code 获取 session_key 和 openid  
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + Appid + "&secret=" + Secret + "&js_code=" + code + "&grant_type=" + grant_type;
            string type = "utf-8";

            WxHelper wh = new WxHelper();
            string html = wh.GetUrlToHtml(url, type);//获取微信服务器返回字符串 

            //将字符串转换为json格式 
            JObject jo = (JObject)JsonConvert.DeserializeObject(html);

            dynamic res = new System.Dynamic.ExpandoObject();

            try
            {
                //微信服务器验证成功 
                res.openid = jo["openid"].ToString();
                res.session_key = jo["session_key"].ToString();
            }
            catch (Exception)
            {
                //微信服务器验证失败 
                res.errcode = jo["errcode"].ToString();
                res.errmsg = jo["errmsg"].ToString();
            }
            if (((IDictionary<string, object>)res).ContainsKey("openid") && !string.IsNullOrEmpty(res.openid))
            {
                var decryptres = wh.AESDecrypt(encryptedData, res.session_key, iv);

                JObject userInfo = (JObject)JsonConvert.DeserializeObject(decryptres);

                res.userinfo = userInfo;
            }

            return Ok(res);
        }


        [Route("api/WxUserTest/{openid}")]
        [HttpGet]
        public IHttpActionResult GetWxUser(string openid)
        {
            var res = new ResponseBase();
            try
            {
                dynamic wxuser = new ExpandoObject();
                wxuser.openid = openid;

                var service = new WxService();
                var data = service.GetUserTest(wxuser);

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }
        
    }
}
