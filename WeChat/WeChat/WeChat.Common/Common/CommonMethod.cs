using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using ZXing;


namespace WeChat.Common
{
    public class CommonMethod
    {
        #region 截取字符长度 static string CutString(string str, int len)
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="str">被截取的字符串</param>
        /// <param name="len">所截取的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int len)
        {
            if (str == null || str.Length == 0 || len <= 0)
            {
                return string.Empty;
            }

            int l = str.Length;


            #region 计算长度
            int clen = 0;
            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。
                if ((int)str[clen] > 128) { len--; }
                clen++;
            }
            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// //截取字符串中文 字母
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <param name="length">截取长度！</param>
        /// <returns></returns>
        public static string SubTrueString(object content, int length)
        {
            string strContent = NoHTML(content.ToString());

            bool isConvert = false;
            int splitLength = 0;
            int currLength = 0;
            int code = 0;
            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);
            for (int i = 0; i < strContent.Length; i++)
            {
                code = Char.ConvertToUtf32(strContent, i);
                if (code >= chfrom && code <= chend)
                {
                    currLength += 2; //中文
                }
                else
                {
                    currLength += 1;//非中文
                }
                splitLength = i + 1;
                if (currLength >= length)
                {
                    isConvert = true;
                    break;
                }
            }
            if (isConvert)
            {
                return strContent.Substring(0, splitLength);
            }
            else
            {
                return strContent;
            }
        }
        #endregion

        #region /*产生验证码*/ GetCode(int codeLength)

        /// <summary>
        /// 生成一个1到10000000之间的正整数
        /// </summary>
        /// <returns></returns>
        public static int GetNums()
        {
            int a = new Random().Next(1, 100000000);
            return a;
        }
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="codeLength">获取的验证码长度</param>
        /// <returns>验证码</returns>
        public static string GetCode(int codeLength)
        {

            string so = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] strArr = so.Split(',');
            string code = "";
            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }
            return code;
        }

        /// <summary>
        /// 获取一个随机字符串
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandomChar(int count)
        {
            string[] s = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                           "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                int temp = ran.Next(s.Length);
                sb.Append(s[temp]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取特定位数的随机数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandomNums(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('9', count - 1);
            int min = int.Parse(sb.ToString()) + 1;//最小值
            sb.Append(9);
            int max = int.Parse(sb.ToString());//最大值



            Random ran = new Random();
            return ran.Next(min, max).ToString();
        }

        /// <summary>
        /// //获取18位订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "order" + dateStr + CovertIntToString(random, 4);
        }

        /// <summary>
        /// 获取支付编号
        /// </summary>
        /// <returns></returns>
        public static string GetPayNum()
        {
            Random ran = new Random();
            int random1 = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            int random2 = ran.Next(9999);//四位随机数
            return CovertIntToString(random1, 4) + dateStr + CovertIntToString(random2, 4);
        }
        public static string GetShopNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "shop-" + dateStr + CovertIntToString(random, 4);
        }
        /// <summary>
        /// //获取17位活动编号
        /// </summary>
        /// <returns></returns>
        public static string GetHuoDongNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "ACT" + dateStr + CovertIntToString(random, 4);
        }
        /// <summary>
        ///  //获取购物车产品流水号
        /// </summary>
        /// <returns></returns>
        public static string GetCartNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "ddpcart" + dateStr + CovertIntToString(random, 4);
        }


        /// <summary>
        ///  //获取会员唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GetVIPNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "VIP" + dateStr + CovertIntToString(random, 6);
        }

        /// <summary>
        /// 长度不够补0
        /// </summary>
        /// <param name="src"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string CovertIntToString(object src, int num)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('0', num);
            string sTarget = sb.ToString() + src.ToString();

            return sTarget.Substring(sTarget.Length - num, num);
        }

        private static double EARTH_RADIUS = 6378137.0;
        /// <summary>
        /// 获取地球两点之间的距离
        /// </summary>
        /// <param name="lat_a"></param>
        /// <param name="lng_a"></param>
        /// <param name="lat_b"></param>
        /// <param name="lng_b"></param>
        /// <returns></returns>
        public static double Gps2m(double lat_a, double lng_a, double lat_b, double lng_b)
        {

            double radLat1 = (lat_a * Math.PI / 180.0);
            double radLat2 = (lat_b * Math.PI / 180.0);
            double a = radLat1 - radLat2;
            double b = (lng_a - lng_b) * Math.PI / 180.0;
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2)
                   + Math.Cos(radLat1) * Math.Cos(radLat2)
                   * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }
        #endregion

        #region sql注入攻击
        public static string[] words = { "select", "insert", "delete", "count(", "drop table", "update", "truncate", "asc(", "mid(", "char(", "xp_cmdshell", "exec", "master", "net", "and", "or", "where" };

        public static string CheckParam(string Value)
        {
            Value = Value.Replace("'", "");
            Value = Value.Replace(";", "");
            Value = Value.Replace("--", "");
            Value = Value.Replace("/**/", "");
            return Value;
        }
        public static string CheckParamThrow(string Value)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (Value.IndexOf(words[i], StringComparison.OrdinalIgnoreCase) > 0)
                {
                    string pattern = string.Format(@"[\W]{0}[\W]", words[i]);
                    Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                    if (rx.IsMatch(Value))
                        throw new Exception("发现sql注入痕迹!");
                }
            }
            return CheckParam(Value);
        }
        /// <summary>
        /// 查找是否含有非法参数
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool CheckParamBool(string Value)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (Value.IndexOf(words[i], StringComparison.OrdinalIgnoreCase) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region IP地址处理
        /// <summary> 
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址 
        /// by flower.b 
        /// </summary> 
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;

                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP 
                    }

                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        #endregion

        #region HTML处理
        /// <summary>
        /// 将html标签转化为特殊字符type=0或特殊字符转化为HTML type=1
        /// </summary>
        /// <param name="vv">源字符串</param>
        /// <param name="type">转化方式</param>
        /// <returns></returns>
        public static string HTML_Trans(string vv,int type)
        {
            if (type == 0)
            {
                vv = vv.Replace(" ", "&nbsp;");
                vv = vv.Replace("　", "&nbsp;");
                vv = vv.Replace(">", "&gt;");
                vv = vv.Replace("<", "&lt;");
                vv = vv.Replace("&", "&amp;");
                vv = vv.Replace("\"", "&quot;");
                vv = vv.Replace("'", "&apos");
            }
            if (type == 1)
            {
                vv = vv.Replace("&nbsp;"," ");
                vv = vv.Replace("&nbsp;","　");
                vv = vv.Replace("&gt;",">");
                vv = vv.Replace("&lt;", "<");
                vv = vv.Replace("&amp;","&");
                vv = vv.Replace("&quot;","\"");
                vv = vv.Replace("&apos", "'");
            }
           
            return vv;
        }

        /// <summary>
        /// 去掉非法html标签
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(object html)
        {
            if (html == null)
                return "";
            string Htmlstring = html.ToString();
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            return Htmlstring.Trim();
        }
       
        #endregion
 
        #region cookie操作
        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static bool setCookieForMIn(string strName, string strValue, int Mintius)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddMinutes(Mintius);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static bool setCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool delCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取当天的最大时间  和  最小时间
        /// <summary>
        /// 获取当天的最大时间  和  最小时间
        /// </summary>
        /// <param name="ty">0：默认为最小时间  1：最大时间</param>
        /// <returns></returns>
        public static string getMaxMinTim(int ty = 0)
        {
            if (ty == 0)
            {
                return DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00";
            }
            else
            {
                return DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59";
            }
        }
        #endregion

        #region 根据身份证 获取年龄 性别
        public static Person GetPersonByIdCard(string identityCard)
        {
            string birthday = string.Empty;
            string sex = string.Empty;
            if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                sex = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                sex = identityCard.Substring(12, 3);
            }
            int UserAge =DateTime.Now.Year- Convert.ToDateTime(birthday).Year;
            if (int.Parse(sex) % 2 == 0)
            {
                sex = "女";
            }
            else
            {
                sex = "男";
            }
            Person P = new Person
            {
                Age = UserAge,
                Sex=sex
            };
            return P;
        }
        #endregion

        public static int GetAgeByDate(DateTime? birthDay)
        {
            int age = 0;
            if (birthDay.HasValue)
            {
                age = DateTime.Now.Year - birthDay.Value.Year;
                if (DateTime.Now.Month < birthDay.Value.Month || (DateTime.Now.Month == birthDay.Value.Month && DateTime.Now.Day < birthDay.Value.Day)) age--;
                if (age <= 0)
                {
                    return 0;
                }
                return age;
            }
            return age;
        }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}








