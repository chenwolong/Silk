using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeChat.Implement;
using WeChat.Interface;
using WeChat.Model;

namespace WeChat.UI.Controllers
{
    public class BaseController : Controller
    {
        IHome Implement = new HomeService();
        public UserInfoModel mol = new UserInfoModel();
        //
        //权限控制

        public int? Uright = -999;//权限值
        //
        #region commonCS
        #region 读取错误信息
        /// <summary>
        /// 读取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetError()
        {
            var errors = ModelState.Values;
            foreach (var item in errors)
            {
                foreach (var item2 in item.Errors)
                {
                    if (!string.IsNullOrEmpty(item2.ErrorMessage))
                    {
                        return item2.ErrorMessage;
                    }
                }
            }
            return "";
        }
        #endregion

        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="Pic"></param>
        /// <returns></returns>
        public Dictionary<string, string> uploadPic(IEnumerable<HttpPostedFileBase> Pic, string keyBody)
        {
            if (Pic != null)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string commonPath = Server.MapPath("~/Content/UpLoad/");//保存路径
                if (!Directory.Exists(commonPath))
                {
                    Directory.CreateDirectory(commonPath);
                }
                foreach (var file in Pic)
                {
                    if (file != null)
                    {
                        string fileType = file.ContentType;//上传文件的类型
                        fileType = fileType.Substring(6, fileType.Length - 6);
                        string fileName = Guid.NewGuid() + "." + fileType;//创建文件名-唯一性
                        int filesize = file.ContentLength;//上传文件的字节数 

                        string fileTypes = "gif|jpg|jpeg|png|bmp|pic";
                        if (fileTypes.Contains(fileType))//如果上传的文件属于要求的类型
                        {
                            if (filesize <= 2 * 1024 * 1024)//要求上传的图片小于2M
                            {
                                string NewcommonPath = commonPath + fileName;//物理路径
                                string SavePath = "/Content/UpLoad/" + fileName;
                                file.SaveAs(NewcommonPath);

                                string key = Guid.NewGuid() + keyBody;
                                dic.Add(key, SavePath);
                            }
                        }
                    }
                }
                return dic;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 弹出跳转
        /// <summary>
        /// 弹出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ActionResult Alert(string str)
        {
            return Content("<script >alert('" + str + "');</script >", "text/html");
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public ActionResult RedictUri(string Uri)
        {
            return Content("<script >window.location.href='" + Uri + "';</script >", "text/html");
        }

        /// <summary>
        /// 弹出并跳转
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public ActionResult Alert(string str, string Uri)
        {
            return Content("<script >alert('" + str + "');window.location.href='" + Uri + "';</script >", "text/html");
        }
        #endregion
        #endregion

        #region 自定义过滤器
        /// <summary>
        /// 自定义过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                return;
            }
            if (authTicket != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string UserId = authTicket.Name;
                if (!string.IsNullOrEmpty(UserId))
                {
                    var response = Implement.GetUserInfo(Convert.ToInt32(UserId));
                    mol = response.Data;
                    if (mol != null)
                    {
                        Uright = mol.Role.RightVle;
                        ViewBag.MenusList = Implement.GetMenus(Uright);
                        ViewBag.UserName = mol.Uname;
                        //把toke用户数据放到 HttpContext.Current.User 里
                        ClientUserData clientUserData = new ClientUserData()
                        {
                            UserId = mol.Id,
                            Uname = mol.Uname,
                            RoleId = mol.RoleId,
                            pinyin = mol.pinyin,
                            idCard = mol.idCard,
                            PhotoNum = mol.PhotoNum,
                            EmployeeSex = mol.EmployeeSex,
                            EmployeePhone = mol.EmployeePhone,
                            Age = mol.Age,
                            Worker = mol.Worker,
                            HomeAddress = mol.HomeAddress,
                            RightVle = mol.Role.RightVle,
                            RightName = mol.Role.RightName,
                            isAdd = mol.Role.isAdd,
                            isUpdate = mol.Role.isUpdate,
                            isDelete = mol.Role.isDelete,
                        };
                        if (System.Web.HttpContext.Current != null)
                        {
                            System.Web.HttpContext.Current.User = new UserPrincipal(clientUserData);
                        }
                    }
                    base.OnActionExecuting(filterContext);
                }
            }
        }
        #endregion
    }
}