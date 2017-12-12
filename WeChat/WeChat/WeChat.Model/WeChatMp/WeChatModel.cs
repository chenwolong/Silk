using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Model
{
    #region AccessToken Model
    /// <summary>
    /// 获取AccessToken Model
    /// </summary>
    public class TokenModel
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }
    #endregion 

    #region 微信菜单创建Model
    /// <summary>
    /// 拥有子菜单的Button
    /// </summary>
    public class WeChatMenus
    {
        public WeChatMenus()
        {
            this.sub_button = new List<WeChatButtons>();
        }
        public string name { get; set; }
        public List<WeChatButtons> sub_button { get; set; }
    }

    /// <summary>
    /// 没有子菜单的Button
    /// </summary>
    public class WeChatButtons
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }  
        public string url { get; set; }
    }
    #endregion

    #region 创建菜单返回Model、其他请求返回Model（errcode、errmsg）
    public class errcode_msg
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
    #endregion
}
