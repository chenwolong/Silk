using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Interface;
using WeChat.Model;

namespace WeChat.Implement
{
    public class HomeService:IHome
    {
        public UserInfoModel GetUserInfo(int userId)
        {
            return new UserInfoModel();
        }
    }
}
