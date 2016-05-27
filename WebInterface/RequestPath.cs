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
        /// 获取微信GetAccess_token
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAccess_token(string name)
        {
            string res = ShareData.ApiUrl + name;
            return res;
        }
        /// <summary>
        /// 微信GetIp_list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetIp_list(string name)
        {
            string res = ShareData.ApiUrl + name;
            return res;
        }
    }
}
