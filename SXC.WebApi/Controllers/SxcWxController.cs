﻿using System;
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
using System.Net.Http.Headers;
using SXC.Code.Log;

namespace SXC.WebApi.Controllers
{
    public class SxcWxController : ApiControllerBase
    {
        /// <summary>
        /// 导航信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Navigations")]
        [HttpGet]
        public IHttpActionResult GetNavigations(int type = 0)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetNavigations(type);

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
        /// 专家信息
        /// </summary>
        /// <returns></returns>
        [Route("api/Experts")]
        [HttpGet]
        public IHttpActionResult GetExperts()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetTeachers(1);

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
        /// 推荐课程
        /// </summary>
        /// <returns></returns>
        [Route("api/Courses/Top")]
        [HttpGet]
        public IHttpActionResult GetCoursesTop()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetCourses(4);

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
        public IHttpActionResult GetPromotions(int type=0)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetPromotions(type);

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
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/Promotions/Top")]
        [HttpGet]
        public IHttpActionResult GetPromotionsTop(int type=0,int? top=null)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetPromotions(type,top);

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
        /// 获取通知
        /// </summary>
        /// <returns></returns>
        [Route("api/Notices")]
        [HttpGet]
        public IHttpActionResult GetNotices()
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetPromotions(2);

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
        /// <param name="sharecode"></param>
        /// <returns></returns>
        [Route("api/WxUser")]
        [HttpGet]
        public IHttpActionResult GetWxUser(string code, string iv, string encryptedData, string sharecode = null)
        {
            var res = new ResponseBase();
            try
            {
                WxHelper wh = new WxHelper();

                dynamic wxuser = wh.GetWxUser(code, iv, encryptedData);

                wxuser.sharecode = sharecode;

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
        /// 代理佣金记录
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/UserCommission/{authid}")]
        [HttpGet]
        public IHttpActionResult GetUserCommission(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetUserCommission(authid);

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

        /// <summary>
        /// 获取代理上下级信息
        /// </summary>
        /// <param name="agentcode"></param>
        /// <returns></returns>
        [Route("api/Agent/Relationship/{agentcode}")]
        [HttpGet]
        public IHttpActionResult GetAgentRelationship(string agentcode)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.GetAgentRelationship(agentcode);

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


        /// <summary>
        /// 获取代理二维码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("api/QrCode/Agent/{code}")]
        [HttpGet]
        public HttpResponseMessage GetAgentQrCode(string code)
        {
            try
            {
                var service = new UtilityService();

                var wxservice = new WxService();

                var qrcontent = code + "|" + wxservice.GetNickNameByAgentCode(code);

                var imgByte = service.GetGetQrCode(qrcontent);

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

        /// <summary>
        /// 合作加盟
        /// </summary>
        /// <param name="cooperationdto"></param>
        /// <returns></returns>
        [Route("api/Cooperation/Join")]
        [HttpPost]
        public IHttpActionResult CooperationJoin(CooperationDto cooperationdto)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.CooperationJoin(cooperationdto);

                if (!string.IsNullOrEmpty(data))
                {
                    res.code = "100";
                    res.msg = data;
                }

                res.resData = null;
                //res.resData = JsonConvert.DeserializeObject(data);
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// 课程预约
        /// </summary>
        /// <param name="reservationdto"></param>
        /// <returns></returns>
        [Route("api/Courses/Reservation")]
        [HttpPost]
        public IHttpActionResult CoursesReservation(ReservationDto reservationdto)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.CoursesReservation(reservationdto);

                if (!string.IsNullOrEmpty(data))
                {
                    res.code = "100";
                    res.msg = data;
                }

                res.resData = null;

                //res.resData = JsonConvert.DeserializeObject(data);
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/Account/{authid}")]
        [HttpGet]
        public IHttpActionResult GetUserAccount(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetUserAccount(authid);
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
        /// 获取账户明细
        /// </summary>
        /// <param name="authid"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [Route("api/Account/Records")]
        [HttpGet]
        public IHttpActionResult GetAccountRecords(Guid authid, string queryJson="")
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                var data = service.GetAccountRecords(authid, queryJson);

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
        /// 提现
        /// </summary>
        /// <param name="accountwithdto"></param>
        /// <returns></returns>
        [Route("api/Account/Withdraw")]
        [HttpPost]
        public IHttpActionResult Withdraw(AccountWithdrawDto accountwithdto)
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.Withdraw(accountwithdto);

                if (!string.IsNullOrEmpty(data))
                {
                    res.code = "100";
                    res.msg = data;
                }

                res.resData = null;
                //res.resData = JsonConvert.DeserializeObject(data);
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// 提现记录
        /// </summary>
        /// <param name="authid"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [Route("api/Account/WithdrawRecords")]
        [HttpGet]
        public IHttpActionResult GetWithdrawRecords(Guid authid, string queryJson = "")
        {
            var res = new ResponseBase();
            try
            {
                var service = new WxService();
                dynamic data = service.GetWithdrawRecords(authid, queryJson);

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
