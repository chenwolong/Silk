using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;
using WeChat.Model;
/*----------------------------------------------------------------
    Copyright (C) 2017 @陈卧龙
    
    文件名：WeChatCommon.cs
    文件功能描述：微信接口公共类库
----------------------------------------------------------------*/
namespace WeChat.MP
{
    public class WeChatCommon
    {
        public static string appid = ResouceLibrary.GetResourceString("appid");
        public static string appsecret = ResouceLibrary.GetResourceString("appsecret");

        #region 重置Response
        public static BaseResponse<T> SetResponse<T>(T Data)
        {
            BaseResponse<T> response = new BaseResponse<T>();
            response.Data = Data;
            response.IsSuccess = true;
            response.ResultCode = 0;
            response.ResultMessage = "请求成功...";
            return response;
        }
        #endregion

        #region 初次获取Token接口 为做保存及时效的处理
        public static BaseResponse<TokenModel> GetAccessToken()
        {
            BaseResponse<TokenModel> response = new BaseResponse<TokenModel>();
            TokenModel M = new TokenModel();
            string Uri = string.Format(WeChatUri.GetAccessTokenUri, appid, appsecret);
            M = RestServiceProxy.GetOne<TokenModel>(Uri);
            if (!string.IsNullOrEmpty(M.access_token))
            {
               response = SetResponse<TokenModel>(M);
            }
            return response;
        }
        #endregion
    }
}
