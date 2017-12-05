using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Model
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            this.IsSuccess = false;
            this.ResultCode = -1;
            this.ResultMessage = "请求失败...";
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// 返回编码 -1 代表失败  0代表成功
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 处理是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        //public List<T> DataList { get; set; }

        public BaseResponse()
        {
            this.IsSuccess = false;
            this.ResultCode = -1;
            this.ResultMessage = "请求失败...";
        }

        public BaseResponse(T data)
        {
            this.Data = data;
        }
    }


    public class SelectInfoForInt
    {
        public string sName { get; set; }
        public int? sValue { get; set; }
    }
    public class SelectInfoForString
    {
        public string sName { get; set; }
        public string sValue { get; set; }
    }
}
