using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;

namespace WeChat.Common
{
    public class NetworkHelper
    {
        //返回描述本地计算机上的网络接口的对象(网络接口也称为网络适配器)。
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        ///<summary>
        /// 通过NetworkInterface读取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static List<string> GetMacByNetworkInterface()
        {
            List<string> macs = new List<string>();
            string mac = string.Empty;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (!ni.GetPhysicalAddress().ToString().Equals(""))
                {
                    mac = ni.GetPhysicalAddress().ToString();
                    for (int i = 1; i < 6; i++)
                    {
                        mac = mac.Insert(3 * i - 1, "-");
                    }
                    macs.Add(mac);
                }
            }
            return macs;
        }

        public  static bool Ping()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "a";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1200;

            try
            {
                PingReply reply = pingSender.Send("www.microsoft.com", timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}