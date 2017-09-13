using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SXC.Services.Impl;
using SXC.Services;
using SXC.WebApi.Utility;
using Newtonsoft.Json;
using SXC.Services.Dto;

namespace SXC.WebApi.Controllers
{
    public class SxcWxController : ApiController
    {
        /// <summary>
        /// 导航信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Navigations")]
        [HttpGet]
        public IHttpActionResult GetNavigations()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetNavigations();

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code ="100";
                res.msg = ex.Message;
            }
            
            return Ok(res);
        }

        /// <summary>
        /// 讲师信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Teachers")]
        [HttpGet]
        public IHttpActionResult GetTeachers()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetTeachers();

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
                
            }
            return Ok(res);
        }

        /// <summary>
        /// 课程信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Courses")]
        [HttpGet]
        public IHttpActionResult GetCourses()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetCourses();

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
                
            }
            return Ok(res);
        }

        /// <summary>
        /// 获取课程明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Courses/{id}")]
        [HttpGet]
        public IHttpActionResult GetCourses(int id)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetCourse(id);

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;

            }
            return Ok(res);
        }

        /// <summary>
        /// 活动信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Promotions")]
        [HttpGet]
        public IHttpActionResult GetPromotions()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetPromotions();

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;

            }
            return Ok(res);
        }

        /// <summary>
        /// 文章明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Article/{id}")]
        [HttpGet]
        public IHttpActionResult GetArticle(int? id)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetArticle(id);

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// 标题获取文章明细
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("api/Article/Title/{title}")]
        [HttpGet]
        public IHttpActionResult GetArticle(string title)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetArticleByTitle(title);

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// 微信用户登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="iv"></param>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        [Route("api/WxUser")]
        [HttpGet]
        public IHttpActionResult GetWxUser(string code, string iv, string encryptedData)
        {
            var res = new ResponseBase();
            try
            {
                WxHelper wh = new WxHelper();

                dynamic wxuser = wh.GetWxUser(code, iv, encryptedData);

                var service = new WxService();
                var data = service.GetUser(wxuser);

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// 获取用户明细信息
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/UserProfile/{authid}")]
        [HttpGet]
        public IHttpActionResult GetUserProfile(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetUserProfile(authid);
                if (data == null)
                {
                    throw new Exception("未找到用户明细信息");
                }

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// 编辑用户明细信息
        /// </summary>
        /// <param name="userpfdto"></param>
        /// <returns></returns>
        [Route("api/UserProfile/Edit")]
        [HttpPost]
        public IHttpActionResult EditUserProfile(UserProfileDto userpfdto)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.EditUserProfile(userpfdto);
                if (!string.IsNullOrEmpty(data))
                {
                    res.code = "100";
                    res.msg = data;
                }

                res.resData = null;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// 获取用户代理信息
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/UserAgent/{authid}")]
        [HttpGet]
        public IHttpActionResult GetUserAgent(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetUserAgent(authid);
                if (data == null)
                {
                    throw new Exception("未找到代理信息");
                }

                res.resData = data;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }
            return Ok(res);
            //return Json(res);
        }

        /// <summary>
        /// 上级代理绑定
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Route("api/Agent/BindSup")]
        [HttpPost]
        public IHttpActionResult BindSupAgent(BindSupAgentDto req)
        {
            var res = new ResponseBase();
            if (string.IsNullOrEmpty(req.agentcode) || req.authid == null)
            {
                res.code = "100";
                res.msg = "信息缺失";
            }
            else
            {
                try
                {
                    var service = new WxService();
                    var data = service.BindSupAgent(req);

                    if (!string.IsNullOrEmpty(data))
                    {
                        res.code = "100";
                        res.msg = data;
                    }

                    res.resData = null;
                }
                catch (Exception ex)
                {
                    res.code = "100";
                    res.msg = ex.Message;
                }
            }
            
            return Ok(res);
        }

        /// <summary>
        /// 获取代理下级成员
        /// </summary>
        /// <param name="agentcode"></param>
        /// <returns></returns>
        [Route("api/Agent/SubUsers/{agentcode}")]
        [HttpGet]
        public IHttpActionResult GetSubAgentUser(string agentcode)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.GetSubAgentUser(agentcode);

                res.resData = data;
                //res.resData = JsonConvert.DeserializeObject(data);
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
