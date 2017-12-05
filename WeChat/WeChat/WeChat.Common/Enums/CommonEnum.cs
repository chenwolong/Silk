using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Common
{
    /// <summary>
    /// 枚举类
    /// </summary>
    public class CommonEnum
    {
        public enum userRole
        {
            /// <summary>
            /// 集团负责人
            /// </summary>
            [Description("集团负责人")]
            JT = 2,
            /// <summary>
            /// 机构负责人
            /// </summary>
            [Description("机构负责人")]
            JG = 1,
            /// <summary>
            /// 普通职员
            /// </summary>
            [Description("普通职员")]
            ZY = 0
        }

    }
}
