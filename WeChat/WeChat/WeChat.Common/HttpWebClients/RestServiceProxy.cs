using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Web;

namespace WeChat.Common
{
    public class RestServiceProxy
    {
        #region static List<T> Get<T>(string endpoint)类型请求
        /// <summary>
        /// HttpClientGet请求
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="endpoint">URL</param>
        /// <returns></returns>
        public static List<T> Get<T>(string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);//带上请求标题头 
                var response = httpClient.GetAsync(endpoint).Result;
                return JsonConvert.DeserializeObject<List<T>>(response.Content.ReadAsStringAsync().Result);
            }
        }
        #endregion

        #region static T Get<T>(int id, string endpoint)类型请求
        public static T Get<T>(int id, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
                //httpClient.DefaultRequestHeaders.Add("marketcode", GlobalClientConfig.MarketCode);//新宇多带的标题头
                //httpClient.DefaultRequestHeaders.Add("languagecode", GlobalClientConfig.LanguageResource);
                var response = httpClient.GetAsync(endpoint + id).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public static T Get<T>(string id, string endpoint)
        {
            T obj;
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var response = httpClient.GetAsync(endpoint + id).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<T>(result);
                return obj;
            }
        }

        #endregion

        public static T GetOne<T>(string endpoint)
        {
            T obj;
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var response = httpClient.GetAsync(endpoint).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<T>(result);
                return obj;
            }
        }

        public static T CheckWhetherInternet<T>(string endpoint)
        {
            T obj;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
              
                var response = httpClient.GetAsync(endpoint).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<T>(result);
                return obj;
            }
        }
        /// <summary>
        /// general get restful service data 
        /// </summary>
        /// <typeparam name="T1">return data type</typeparam>
        /// <typeparam name="T2">input data type</typeparam>
        /// <param name="data">search condition</param>
        /// <param name="endpoint">service url</param>
        /// <returns></returns>
        public static T1 Get<T1, T2>(T2 data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
              
                var _endpoint = endpoint;

                var requestMessage = GetObjectPropertyValue(data);
                if (!string.IsNullOrEmpty(requestMessage))
                {
                    _endpoint += "?" + requestMessage.Remove(0, 1);
                }
                var response = httpClient.GetAsync(_endpoint).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonHelper.JsonDeserialize<T1>(result);
            }
        }

        public static List<T1> GetList<T1, T2>(T2 data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
              
                var _endpoint = endpoint;
                var requestMessage = GetObjectPropertyValue(data);
                if (!string.IsNullOrEmpty(requestMessage))
                {
                    _endpoint += "?" + requestMessage.Remove(0, 1);
                }
                var response = httpClient.GetAsync(_endpoint).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }
                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<T1>>(result);
            }
        }

        public static List<T1> GetList<T1>(string id, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
              
                var response = httpClient.GetAsync(endpoint + id).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<T1>>(result);
            }
        }

        public static List<T1> GetList<T1>(string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var response = httpClient.GetAsync(endpoint).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<T1>>(result);
            }
        }

        public static string Post<T>(T data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
             
                var requestMessage = JsonHelper.JsonSerializer<T>(data);
                HttpContent contentPost = new StringContent(requestMessage, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(endpoint, contentPost).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<string>(result);

            }
        }

        /// <summary>
        /// 陈卧龙 2017-12-11
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static string Post(string data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);

                var requestMessage = data;
                HttpContent contentPost = new StringContent(requestMessage, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(endpoint, contentPost).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return result;

            }
        }

        public static TRetrun Post<TRetrun, TPost>(TPost data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var requestMessage = JsonConvert.SerializeObject(data, new IsoDateTimeConverter());
                HttpContent contentPost = new StringContent(requestMessage, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(endpoint, contentPost).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TRetrun>(result);
            }
        }

        public static Dictionary<string, object> PostByDictionay<T>(T data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var requestMessage = JsonHelper.JsonSerializer<T>(data);
                HttpContent contentPost = new StringContent(requestMessage, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(endpoint, contentPost).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        throw new Exception("Invoke Server Service Error");
                    }
                }

                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            }
        }
        /// <summary>
        /// 注册设备MAC同步服务器，无需验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static string PostDevice<T>(T data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var requestMessage = JsonHelper.JsonSerializer<T>(data);
                HttpContent contentPost = new StringContent(requestMessage, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(endpoint, contentPost).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<string>(result);
            }
        }


        public static string GetObjectPropertyValue<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property != null && t != null)
                {
                    object o = property.GetValue(t, null);
                    if (o != null)
                    {
                        sb.Append("&" + property.Name + "=" + o); ;
                    }
                }
            }
            return sb.ToString();
        }

        public static string Delete<T>(T data, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("mac", GlobalClientConfig.MacAddress);
               
                var _endpoint = endpoint;
                var requestMessage = GetObjectPropertyValue(data);
                if (!string.IsNullOrEmpty(requestMessage))
                {
                    _endpoint += "?" + requestMessage.Remove(0, 1);
                }
                var result = httpClient.DeleteAsync(_endpoint).Result;
                //return result.Content.ReadAsStringAsync().Result;


                string res = result.Content.ReadAsStringAsync().Result;
                return res;
            }
        }

        public static string Delete(string id, string endpoint)
        {
            using (var httpClient = NewHttpClient())
            {
                var result = httpClient.DeleteAsync(endpoint + id).Result;

                return result.Content.ReadAsStringAsync().Result;
            }
        }

        protected static HttpClient NewHttpClient()
        {
          
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            return new HttpClient(handler);

        }

        /// <summary>
        /// 请求格式JSON数据格式
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string GetUri(string posturl, string postData)
        {

            using (var http = new HttpClient())
            {
                var content = new StringContent(postData, Encoding.UTF8, "application/json");
                //await异步等待回应
                var response = http.PostAsync(posturl, content).Result;

            }
            return null;
        }
    }
}