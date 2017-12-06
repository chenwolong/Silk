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
        UserInfoModel GetUserInfo(int userId);
    }
}
