using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.UI.Auth;

namespace WeChat.UI.Controllers
{
    [MvcActionAuth]
    public class WeChatController : BaseController
    {
        public ActionResult WeChat_Menus()
        {
            return View();
        }

    }
}
