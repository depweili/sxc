using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using SXC.Code.Utility;
using SXC.Code;

namespace SXC.WebApi.Utility
{
    public class WxHelper
    {
        public dynamic GetWxUser(string code, string iv, string encryptedData)
        {
            string Appid = Cryptography.Base64ForUrlDecode(ConfigHelper.GetConfig("Wx_Appid"));//"wx12a7e3bf7a31d815";
            string Secret = ConfigHelper.GetConfig("Wx_Secret"); //ConfigurationManager.AppSettings["Wx_Secret"]; //"b4722d8a6a5629ed7717e58d1af431ba"; 
            string grant_type = "authorization_code";


            //向微信服务端 使用登录凭证 code 获取 session_key 和 openid  
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + Appid + "&secret=" + Secret + "&js_code=" + code + "&grant_type=" + grant_type;
            string type = "utf-8";

            string html = GetUrlToHtml(url, type);//获取微信服务器返回字符串 

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
                var decryptres = AESDecrypt(encryptedData, res.session_key, iv);

                JObject userInfo = (JObject)JsonConvert.DeserializeObject(decryptres);

                res.userinfo = userInfo;
            }

            return res;
        }

        public string GetUrlToHtml(string Url, string type)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance. 
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream) 
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public string AESDecrypt(string inputdata, string AesKey, string AesIV)
        {
            try
            {
                AesIV = AesIV.Replace(" ", "+");
                AesKey = AesKey.Replace(" ", "+");
                inputdata = inputdata.Replace(" ", "+");

                byte[] encryptedData = Convert.FromBase64String(inputdata);

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(AesKey); // Encoding.UTF8.GetBytes(AesKey); 
                rijndaelCipher.IV = Convert.FromBase64String(AesIV);// Encoding.UTF8.GetBytes(AesIV); 
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);

                return result;
            }
            catch (Exception ex)
            {
                
                return null;

            }
        } 
    }
}