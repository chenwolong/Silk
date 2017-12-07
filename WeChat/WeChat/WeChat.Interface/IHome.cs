using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Model;

namespace WeChat.Interface
{
    public interface IHome
    {
        /// <summary>
        /// 获取登录人信息
        /// </summary>
        /// <param name="userId">id</param>
        /// <returns></returns>
        BaseResponse<UserInfoModel> GetUserInfo(int userId);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        BaseResponse<UserInfoModel> UserLogin(string UserName, string UserPwd);
        /// <summary>
        /// 获取菜单栏
        /// </summary>
        /// <param name="UserRight"></param>
        /// <returns></returns>
        string GetMenus(int? UserRight);
    }
}
