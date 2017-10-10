using Newtonsoft.Json;
using SXC.Code.Log;
using SXC.Code.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SXC.WebApi
{
    public class ActionFilter : ActionFilterAttribute
    {
        private const string Key = "_WebApiMonitor_";
        private bool _IsDebugLog = ConfigHelper.GetConfigBool("IsFilterLog");
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_IsDebugLog)
            {
                Stopwatch stopWatch = new Stopwatch();

                actionContext.Request.Properties[Key] = stopWatch;

                string actionName = actionContext.ActionDescriptor.ActionName;

                //Debug.Print(Newtonsoft.Json.JsonConvert.SerializeObject(actionContext.ActionArguments));

                //LogHelper.Monitor(JsonConvert.SerializeObject(actionContext, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //}));
                LogHelper.Monitor(actionContext.Request.RequestUri.ToString());

                LogHelper.Monitor(JsonConvert.SerializeObject(actionContext.ActionArguments));

                stopWatch.Start();
            }
            base.OnActionExecuting(actionContext);

        }

        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    if (_IsDebugLog)
        //    {
        //        Stopwatch stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;

        //        if (stopWatch != null)
        //        {

        //            stopWatch.Stop();

        //            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

        //            string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

        //            LogHelper.Monitor(actionExecutedContext.Response.Content.ReadAsStringAsync().Result);

        //            LogHelper.Monitor(string.Format(@"[{0}/{1} 用时 {2}ms]", controllerName, actionName, stopWatch.Elapsed.TotalMilliseconds));
        //        }
        //    }

        //    base.OnActionExecuted(actionExecutedContext);
        //}

        public override System.Threading.Tasks.Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, System.Threading.CancellationToken cancellationToken)
        {
            if (_IsDebugLog)
            {
                Stopwatch stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;

                if (stopWatch != null)
                {

                    stopWatch.Stop();

                    string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

                    string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                    LogHelper.Monitor(actionExecutedContext.Response.Content.ReadAsStringAsync().Result);

                    LogHelper.Monitor(string.Format(@"[{0}/{1} 用时 {2}ms]", controllerName, actionName, stopWatch.Elapsed.TotalMilliseconds));
                }
            }

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

    }
}