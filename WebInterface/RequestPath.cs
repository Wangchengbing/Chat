using System;
using System.Collections.Generic;
using System.Text;
using LoserModel;

namespace LoserServer
{
    /// <summary>
    /// 组合请求路径
    /// </summary>
    public class RequestPath
    {
        /// <summary>
        /// 组装路径   订阅号请求微信接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreatePath(string name)
        {
            string res = ShareData.wxApiUrl + name;
            return res;
        }

        /// <summary>
        /// 组装路径   企业号请求微信接口
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreatePathqy(string name)
        {
            string res = ShareData.qyApiUrl + name;
            return res;
        }


        /// <summary>
        /// 组装路径  百度api
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreatePathApi(string name)
        {
            string res = ShareData.ApiUrlBaidu + name;
            return res;
        }
    }
}
