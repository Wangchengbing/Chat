using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LoserModel
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class ShareData
    {
        public static string ApiUrl = "https://api.weixin.qq.com/cgi-bin/";
        public static string access_token = ""; //access_token的有效期目前为2个小时
        public static string token = ConfigurationManager.AppSettings["WeixinToken"];//从配置文件获取Token
        public static string appid = ConfigurationManager.AppSettings["WeixinAppid"];//从配置文件获取Appid
        public static string secret = ConfigurationManager.AppSettings["WeixinSecret"];//从配置文件获取Secret
    }
}
