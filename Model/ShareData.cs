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
        #region 订阅号全局

        //订阅号 wxApiUrl
        public static string wxApiUrl = ConfigurationManager.AppSettings["wxApiUrl"];
        //订阅号 access_token//access_token的有效期目前为2个小时
        public static string wxaccess_token = ConfigurationManager.AppSettings["wxaccess_token"]; 

  
        //订阅号 token
        public static string wxToken = ConfigurationManager.AppSettings["wxToken"];//从配置文件获取Token
         //订阅号 EncodingAESKey(消息加解密密钥)
        public static string wxEncodingAESKey = ConfigurationManager.AppSettings["wxEncodingAESKey"];//从配置文件获取EncodingAESKey
        //订阅号 AppID(应用ID)
        public static string wxAppid = ConfigurationManager.AppSettings["wxAppid"];//从配置文件获取Appid
        //订阅号 AppSecret(应用密钥)
        public static string wxSecret = ConfigurationManager.AppSettings["wxSecret"];//从配置文件获取Secret

        #endregion


        #region 企业号全局

        //企业号 ApiUrl
        public static string qyApiUrl = ConfigurationManager.AppSettings["qyApiUrl"];
        //企业号 access_token//access_token的有效期目前为2个小时
        public static string qyaccess_token = ConfigurationManager.AppSettings["qyaccess_token"]; 
        //企业号 token
        public static string qyToken = ConfigurationManager.AppSettings["qyToken"];//从配置文件获取Token
        //企业号 EncodingAESKey(消息加解密密钥)
        public static string qyEncodingAESKey = ConfigurationManager.AppSettings["qyEncodingAESKey"];
        //企业号 qyCorpID(企业ID)
        public static string qyCorpID = ConfigurationManager.AppSettings["qyCorpID"];//从配置文件获取qyCorpID
        //企业号 qySecret(企业Secret)
        public static string qySecret = ConfigurationManager.AppSettings["qySecret"];
        
        #endregion

        //百度 apiUrl
        public static string ApiUrlBaidu = ConfigurationManager.AppSettings["ApiUrlBaidu"];
        //百度 apikey
        public static string baiduapikey = ConfigurationManager.AppSettings["baiduapikey"];
                                                                                          

        
        

    }
}
