using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeChat.Common
{
    public class SelectHelper
    {
        private static SelectItemConfig configManager;
        public static Dictionary<string, string> GetConfigByKey(string key)
        {
            string mapPath = HttpContext.Current.Server.MapPath(VirtualPathUtility.GetDirectory("~"));
            configManager = new SelectItemConfig(mapPath);
            Dictionary<string, string> dic = configManager.getItemListByKey(key);
            return dic;
        }
    }
}
