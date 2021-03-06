﻿using SXC.Services;
using SXC.Services.Dto;
using SXC.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SXC.WebApi.Controllers
{
    public class IntegralController : ApiControllerBase
    {

        /// <summary>
        /// 用户积分信息
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/Integral/User/{authid}")]
        [HttpGet]
        public IHttpActionResult UserIntegral(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new IntegralService();
                var data = service.GetUserIntegral(authid);

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
        /// 积分历史
        /// </summary>
        /// <param name="integralid"></param>
        /// <returns></returns>
        [Route("api/Integral/Records/{integralid}")]
        [HttpGet]
        public IHttpActionResult GetIntegralRecords(Guid integralid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new IntegralService();
                var data = service.GetIntegralRecords(integralid);

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

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/Integral/Commodity")]
        [HttpGet]
        public IHttpActionResult GetCommodity(Guid uid,Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.GetCommodity(uid, authid);

                res.resData = data;

                if (res.resData == null)
                {
                    throw new Exception("未找到商品");
                }
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;

            }
            return Ok(res);
        }

        /// <summary>
        /// 积分兑换订单
        /// </summary>
        /// <param name="orderdto"></param>
        /// <returns></returns>
        [Route("api/Integral/Exchange")]
        [HttpPost]
        public IHttpActionResult ExchangeOrder(ExchangeOrderDto orderdto)
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.ExchangeOrder(orderdto);

                if (!string.IsNullOrEmpty(data.message))
                {
                    res.code = "100";
                    res.msg = data.message;
                }

                res.resData = data.detail;

                //res.resData = null;
            }
            catch (Exception ex)
            {
                res.code = "100";
                res.msg = ex.Message;

            }
            return Ok(res);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="authid"></param>
        /// <returns></returns>
        [Route("api/Integral/UserOrders/{authid}")]
        [HttpGet]
        public IHttpActionResult GetUserOrders(Guid authid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.GetUserOrders(authid);

                //if (!string.IsNullOrEmpty(data.message))
                //{
                //    res.code = "100";
                //    res.msg = data.message;
                //}

                //res.resData = data.detail;

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
        /// 订单获取视频
        /// </summary>
        /// <param name="orderuid"></param>
        /// <returns></returns>
        [Route("api/Integral/UserOrders/Video/{orderuid}")]
        [HttpGet]
        public IHttpActionResult GetVideoByOrder(Guid orderuid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new StoreService();
                var data = service.GetVideoByOrder(orderuid);

                //if (!string.IsNullOrEmpty(data.message))
                //{
                //    res.code = "100";
                //    res.msg = data.message;
                //}

                //res.resData = data.detail;

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
        /// 获取抽奖信息
        /// </summary>
        /// <param name="authid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("api/Lottery")]
        [HttpGet]
        public IHttpActionResult GetLottery(Guid authid, int type = 0)
        {
            var res = new ResponseBase();
            try
            {
                var service = new IntegralService();
                var data = service.GetLottery(authid, type);

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
        /// 摇奖
        /// </summary>
        /// <param name="authid"></param>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        [Route("api/Lottery/Roll")]
        [HttpGet]
        public IHttpActionResult GetWinPrize(Guid authid, int lotteryid)
        {
            var res = new ResponseBase();
            try
            {
                var service = new IntegralService();
                var data = service.GetWinPrize(authid, lotteryid);

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
