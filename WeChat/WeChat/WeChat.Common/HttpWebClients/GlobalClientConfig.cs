using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeChat.Common
{
    public class GlobalClientConfig
    {
        static string _macAddress = string.Empty;
        public static string MacAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_macAddress))
                {
                    string mac = MacAddresses.FirstOrDefault();
                    if (mac == string.Empty)
                    {
                        _macAddress = null;
                    }
                    else
                    {
                        _macAddress = mac;
                    }
                }
                return _macAddress;
            }
        }

        static List<string> _macAddresses = null;
        public static List<string> MacAddresses
        {
            get
            {
                if (_macAddresses == null)
                {
                    List<string> macs = NetworkHelper.GetMacByNetworkInterface();
                    if (macs == null || macs.Count == 0)
                    {
                        _macAddresses = null;
                    }
                    else
                    {
                        _macAddresses = macs;
                    }
                }
                return _macAddresses;
            }
        }
    }
}