using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeChat.Common;
using WeChat.UI.Models;

namespace WeChat.UI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Logins()
        {
            FormsAuthentication.SignOut();
            //ClearLogin();
            string HX_userName = CommonMethod.getCookie("HX_userName");
            string HX_userPwd = CommonMethod.getCookie("HX_userPwd");
            ViewBag.HX_userName = HX_userName;
            ViewBag.HX_userPwd = HX_userPwd;
            return View();
        }

        [HttpPost]
        public object Login(LoginsModel LoginMol)
        {
            if (ModelState.IsValid)//是否通过Model验证
            {
                return LoginMol.LoginAction();
            }
            else
            {
                return GetError();
            }
        }
    }
}
