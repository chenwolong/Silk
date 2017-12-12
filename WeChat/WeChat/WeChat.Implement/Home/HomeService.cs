using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Entity;
using WeChat.Interface;
using WeChat.Model;
using System.Data.Entity;

namespace WeChat.Implement
{
    public class HomeService : IHome
    {
        #region 重置Response
        private static BaseResponse<T> SetResponse<T>(T Data)
        {
            BaseResponse<T> response = new BaseResponse<T>();
            response.Data = Data;
            response.IsSuccess = true;
            response.ResultCode = 0;
            response.ResultMessage = "请求成功...";
            return response;
        }
        #endregion

        #region 用户基本信息操作
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse<UserInfoModel> GetUserInfo(int userId)
        {
            using (WeChatEntities context = new WeChatEntities())
            {
                BaseResponse<UserInfoModel> response = new BaseResponse<UserInfoModel>();
                UserInfoModel Model = new UserInfoModel();
                var dbset = context.SYS_UserInfo;
                var M = dbset.Find(userId);
                if (M != null)
                {
                    Model = Mapper.DynamicMap<UserInfoModel>(M);
                    SYS_RoleModel RoleModel = new SYS_RoleModel()
                    {
                        Id = M.SYS_Role.Id,
                        RightName = M.SYS_Role.RightName,
                        RightVle = M.SYS_Role.RightVle,
                        isAdd = M.SYS_Role.isAdd,
                        isUpdate = M.SYS_Role.isUpdate,
                        isDelete = M.SYS_Role.isDelete,
                        Addtime = M.SYS_Role.Addtime
                    };
                    Model.Role = RoleModel;
                    response = SetResponse<UserInfoModel>(Model);
                }
                return response;
            }
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="UserName">账户名 密码</param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        public BaseResponse<UserInfoModel> UserLogin(string UserName, string UserPwd)
        {
            using (WeChatEntities context = new WeChatEntities())
            {
                BaseResponse<UserInfoModel> response = new BaseResponse<UserInfoModel>();
                UserInfoModel Model = new UserInfoModel();
                var dbset = context.SYS_UserInfo;
                var bol = dbset.Any(A => A.Uname == UserName && A.Upwd == UserPwd);
                if (bol)
                {
                    var M = dbset.Include(A=>A.SYS_Role).Where(A => A.Uname == UserName && A.Upwd == UserPwd).FirstOrDefault();
                    if (M != null)
                    {
                        Model = Mapper.DynamicMap<UserInfoModel>(M);
                        SYS_RoleModel RoleModel = new SYS_RoleModel()
                        {
                             Id=M.SYS_Role.Id,
                             RightName = M.SYS_Role.RightName,
                             RightVle = M.SYS_Role.RightVle,
                             isAdd = M.SYS_Role.isAdd,
                             isUpdate = M.SYS_Role.isUpdate,
                             isDelete = M.SYS_Role.isDelete,
                             Addtime=M.SYS_Role.Addtime
                        };
                        Model.Role = RoleModel;
                        response = SetResponse<UserInfoModel>(Model);
                    }
                }
                return response;
            }
        }
        #endregion

        #region 获取导航栏
        /// <summary>
        /// 获取导航栏
        /// </summary>
        /// <param name="UserRight"></param>
        /// <returns></returns>
        public string GetMenus(int? UserRight)
        {
            StringBuilder sb = new StringBuilder("");

            using (WeChatEntities context = new WeChatEntities())
            {
                var dbset = context.SYS_Menus;
                var data = dbset.AsNoTracking().Where(A => A.isdeleted == false);
                List<MenusModel> MenusList = Mapper.DynamicMap<List<MenusModel>>(data);
                if (MenusList.Count > 0)
                {
                    string right = UserRight.ToString();
                    var Flist = MenusList.Where(A => A.FId == 0&&A.rightId.Contains(UserRight.ToString())).ToList();
                    foreach (var Fitem in Flist)
                    {
                        var Clist = MenusList.Where(A => A.FId == Fitem.Id && A.rightId.Contains(UserRight.ToString())).ToList();
                        if (Clist.Count > 0)
                        {
                            sb.Append(@" <li><a href='#subPages" + Flist.IndexOf(Fitem) + "' data-toggle='collapse' class='collapsed'><i class='"+Fitem.remark1+"'></i><span>" + Fitem.menuName + "</span> <i class='icon-submenu lnr lnr-chevron-left'></i></a><div id='subPages" + Flist.IndexOf(Fitem) + "' class='collapse '><ul class='nav'>");
                            foreach (var Citem in Clist)
                            {
                                sb.Append("<li><a href='"+Citem.menuPth+"' class=''>"+Citem.menuName+"</a></li>");
                            }
                            sb.Append("</ul></div></li>");
                        }
                        else
                        {
                            sb.Append("<li><a href='" + Fitem.menuPth + "' class='' ><i class='lnr lnr-code'></i><span>" + Fitem.menuName + "</span></a></li>");
                        }
                    }
                }
                return sb.ToString();
            };

        }
        #endregion

        #region 权限验证
        public bool CheckRole(string MenuPth,string CurrentUserRole)
        {
            using (WeChatEntities context = new WeChatEntities())
            {
                var bol=false;
                var M = context.SYS_Menus.Where(A=>A.menuPth==MenuPth).FirstOrDefault();
                if (M != null)
                {
                    if (M.rightId.Contains(CurrentUserRole))
                    {
                        bol = true; 
                    }
                }
                return bol;
            }
        }
        #endregion
    }
}
