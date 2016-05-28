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
        /// 组装路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreatePath(string name)
        {
            string res = ShareData.ApiUrl + name;
            return res;
        }
    }
}
