using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChat.Model;
using System.Text;
using WeChat.MP;
using WeChat.Common;
using System.IO;
using System.Net;

namespace WeChat.UI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string json = GetMenusJson();
            string AccessToken = WeChatCommon.GetAccessToken().Data.access_token;
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + AccessToken;
            string s = RestServiceProxy.Post(json, url);
            errcode_msg M = new errcode_msg();
            M = JsonConvert.DeserializeObject<errcode_msg>(s);
            //string ss = GetPage(url, json);
        }

        public string GetMenusJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"button\": [");

            //首先创建一个没有子菜单的Button
            WeChatButtons C1 = new WeChatButtons()
            {
                 key="home", name="单击我", type="click"
            };
            sb.Append(JsonConvert.SerializeObject(C1)+",").Replace(",\"url\":null", "").Replace("\"key\":null,", "");
            WeChatButtons m1 = new WeChatButtons()
            {
                 name="百度一下", url="http://www.baidu.com/", type="view"
            };
            WeChatButtons m2 = new WeChatButtons()
            {
                name = "子菜单单击",
                key = "fff",
                type = "click"
            };
            WeChatButtons m3 = new WeChatButtons()
            {
                name = "子单击",
                key = "ff2f",
                type = "click"
            };
            List<WeChatButtons> sub_buttonList = new List<WeChatButtons>();
            sub_buttonList.Add(m1); sub_buttonList.Add(m2); sub_buttonList.Add(m3);
            WeChatMenus B = new WeChatMenus()
            {
                 name="有子菜单", sub_button=sub_buttonList
            };
            sb.Append(JsonConvert.SerializeObject(B).Replace(",\"url\":null", "").Replace("\"key\":null,", ""));
            sb.Append("]");
            sb.Append("}");

            string s = sb.ToString();
            return s.ToString();


        }

       
    }
}