using SXC.Services;
using SXC.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SXC.WebApi.Controllers
{
    public class IntegralController : ApiController
    {
        /// <summary>
        /// 每日签到
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Route("api/Integral/DailySignIn/{uid}")]
        [HttpGet]
        public IHttpActionResult DailySignIn(Guid uid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new IntegralService();
                var data = service.DailySignIn(uid);

                if (!string.IsNullOrEmpty(data.message))
                {
                    res.code = "100";
                    res.msg = data.message;
                }

                res.resData = data.detail;

                
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;
            }

            return Ok(res);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        [Route("api/Integral/Commoditys")]
        [HttpGet]
        public IHttpActionResult GetCommoditys()
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.GetCommoditys();

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
        /// 获取推荐商品
        /// </summary>
        /// <returns></returns>
        [Route("api/Integral/Commoditys/Top")]
        [HttpGet]
        public IHttpActionResult GetCommoditysTop()
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.GetCommoditys(4);

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
