using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeChat.Implement;
using WeChat.Interface;

namespace WeChat.UI.Auth
{
    public class MvcActionAuth : AuthorizeAttribute
    {
        IHome service = new HomeService();
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string Role = string.Empty;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            //数据库验证当前Controller及Action允许访问的权限
            //简单模拟 读取数据库
            string MenuPath = "/" + controllerName.ToLower() + "/" + actionName.ToLower();
            string CurrentRole = string.Empty;
            //
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                CurrentRole = authTicket.UserData;
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Redirect("/Home/Logins");
            }
            if (authTicket == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("/Home/Logins");
            }
            else
            {
                //数据库验证菜单
                if (MenuPath.ToLower() != "/manger/index")
                {
                    if (!service.CheckRole(MenuPath, CurrentRole))
                    {
                        System.Web.HttpContext.Current.Response.Redirect("/Home/Logins");
                    }
                }
            }
            base.OnAuthorization(filterContext);
        
        }
    }
}