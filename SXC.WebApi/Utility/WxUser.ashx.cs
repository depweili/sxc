using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SXC.WebApi.Utility
{
    /// <summary>
    /// WxUser 的摘要说明
    /// </summary>
    public class WxUser : IHttpHandler
    {
        private HttpContext _context;
        public void ProcessRequest(HttpContext context)
        {
            _context = context;

            context.Response.ContentType = "text/plain";
            context.Response.Write(GetResult());
        }

        private string GetResult()
        {
            string resstr="";

            string code = "";
            string iv = "";
            string encryptedData = "";
            try
            {
                code = HttpContext.Current.Request.QueryString["code"].ToString();
                iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
            }
            catch (Exception ex)
            {
                _context.Response.Write(ex.ToString());
            }

            string Appid = "wx12a7e3bf7a31d815";
            string Secret = "b4722d8a6a5629ed7717e58d1af431ba";
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
            if (!string.IsNullOrEmpty(res.openid))
            {
                var decryptres = WxHelper.AESDecrypt(encryptedData, res.session_key, iv);

                JObject userInfo = (JObject)JsonConvert.DeserializeObject(decryptres);

                res.userinfo = userInfo;
            }

            return JsonConvert.SerializeObject(res);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}