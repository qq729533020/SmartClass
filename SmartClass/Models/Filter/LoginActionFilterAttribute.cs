﻿using Common;
using Common.Cache;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SmartClass.Models
{
    public class LoginActionFilterAttribute : ActionFilterAttribute
    {
        public ISys_UserService Sys_UserService { get; set; }
        public ISys_LogService Sys_LogService { get; set; }
        Sys_Log log { get; set; }
        string action { get; set; }
        /// <summary>
        /// 行为执行后，记录登录记录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            action = filterContext.RouteData.Values["action"].ToString();
            JsonResult result = filterContext.Result as JsonResult;
            //TODO 这边需要改成从请求头和Cookie获取
            //string account = filterContext.HttpContext.Request["Account"] == null ? filterContext.HttpContext.Request.Cookies["Account"]?.Value : filterContext.HttpContext.Request["Account"];
            ModelResult mr = null;
            string userHostAddress = filterContext.HttpContext.Request.UserHostAddress;//IP地址
            if (result != null)
            {

                mr = result.Data as LoginResult;
                log = new Sys_Log();
                log.F_Account = "admin";
                #region 开启一个线程，后台处理日志信息
                ThreadPool.QueueUserWorkItem((o =>
                {
                    try
                    {
                        log.F_Id = Guid.NewGuid().ToString();
                        log.F_Date = DateTime.Now;
                        log.F_IPAddress = userHostAddress;
                        log.F_Type = action.ToString();
                        log.F_ModuleName = "系统登录";
                        log.F_IPAddressName = "广东省河源市 电信";
                        log.F_Result = mr.Status;
                        log.F_Description = mr.Message;
                        Sys_User user = Sys_UserService.GetEntityByAccount(log.F_Account);
                        log.F_NickName = user.F_NickName;
                        //更新日志信息
                        Sys_LogService.AddEntity(log);

                    }
                    catch (Exception ex)
                    {
                        ExceptionHelper.AddException(ex);
                    }
                }));
                #endregion
            }
        }

        /// <summary>
        /// 行为执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

        }
    }
}