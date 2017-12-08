using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.UI.Auth;

namespace WeChat.UI.Controllers
{
    [MvcActionAuth]
    public class MangerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult sys_UserManger()
        {
            return View();
        }

    }
}
