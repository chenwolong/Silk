using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*----------------------------------------------------------------
    Copyright (C) 2017 @陈卧龙
    
    文件名：WeChatUri.cs
    文件功能描述：微信接口Uri 调用地址
----------------------------------------------------------------*/
namespace WeChat.MP
{
    public class WeChatUri
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetAccessTokenUri = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// 创建微信菜单
        /// </summary>
        public static string CreateWeChatMenus = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
    }
}
