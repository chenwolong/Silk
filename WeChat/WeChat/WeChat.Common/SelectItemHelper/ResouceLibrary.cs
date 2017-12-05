using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using WeChat.Common.SelectItemHelper;

namespace WeChat.Common
{
    public class ResouceLibrary 
    {
        /// <summary>
        /// 获取资源文件--根据资源文件键的名字，取出对应的值
        /// </summary>
        /// <param name="ResourceCode">ResourceCode</param>
        /// <returns></returns>
        public static string GetResourceString(string ResourceCode)
        {
            return WeChatConfig.ResourceManager.GetString(ResourceCode);
        }

        /// <summary>
        /// 遍历和资源文件结合的枚举
        /// </summary>
        /// <returns></returns>
        public static void ForEachParm()
        {
            foreach (string Type in Enum.GetNames(typeof(weixinParm)))
            {
                weixinParm type = (weixinParm)Enum.Parse(typeof(weixinParm), Type);
                string Key = type.ToString();
                string ResourceValue = WeChatConfig.ResourceManager.GetString(EnumCode.GetFieldCode(type));//获取资源文件中存储的值
                string EnumValue = type.GetHashCode().ToString();
            }
        }

        /// <summary>
        /// 遍历普通枚举 
        /// </summary>
        /// <returns></returns>
        public static void ForEachEnum()
        {
            foreach (string Type in Enum.GetNames(typeof(shopInfo)))
            {
                shopInfo type = (shopInfo)Enum.Parse(typeof(shopInfo), Type);
                string Key = type.ToString();
                string EnumValue = type.GetHashCode().ToString();
            }
        }

        /// <summary>
        /// 根据枚举Key 获取枚举的序号 
        /// </summary>
        /// <returns></returns>
        public static void ForEachEnum(shopInfo parm)
        {
            shopInfo type = parm;
            string Key = type.ToString();
            string EnumValue = type.GetHashCode().ToString();
        }
    }

    /// <summary>
    /// 将枚举值存入资源文件
    /// </summary>
    public enum weixinParm
    {
        [EnumCode("apisecret")]//对应资源文件中的Key值  方便遍历时寻找
        apisecret,
        [EnumCode("appid")]
        appid,
        [EnumCode("appsecret")]
        appsecret,
        [EnumCode("duokefu")]
        duokefu,
        [EnumCode("mchid")]
        mchid,
        [EnumCode("orderSuccessid")]
        orderSuccessid ,
        [EnumCode("paySuccessid")]
        paySuccessid
    }

    /// <summary>
    /// test
    /// </summary>
    public enum shopInfo
    {
        shopName,
        shopAddress,
        shopTel,
    }

    public enum NoticeType
    {
        Notice = 'A',
        LabRule = 'H',
        HotInformation = 'N',
        Column = 'C',
        All = '1',
        Null = '0'
    }
}
