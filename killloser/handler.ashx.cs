using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Security;
using LoserModel;
using LoserService;
using LoserUtil;

namespace killloser
{
    /// <summary>
    /// handler 的摘要说明
    /// </summary>
    public class handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //TracingHelper.Initialization(1, 1, 1, 1, "Log", "", "");
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    TracingHelper.Info("Post " + postString);
                    Execute(postString);
                }
            }
            //else if (HttpContext.Current.Request.HttpMethod.ToUpper() == "GET")
            //{
            //    TracingHelper.Info("Get " + postString);
            //    Execute(postString);
            //}
            else
            {
                Auth(); //微信接入的测试
            }
        }
        /// <summary>
        /// 成为开发者的第一步，验证并相应服务器的数据
        /// </summary>
        private void Auth()
        {
            if (string.IsNullOrEmpty(ShareData.token))
            {
                //LogTextHelper.Error(string.Format("WeixinToken 配置项没有配置！"));
            }

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (checkSignature(ShareData.token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 检查 Signature
        /// </summary>
        /// <returns></returns>
        private bool checkSignature(string token, string signature, string timestamp, string nonce)
        {
            try
            {
                //1.组成数组
                var tmpArr = new string[] { token, timestamp, nonce };
                //2.数组排序
                Array.Sort(tmpArr);
                //3.转成字符串
                string tmpStr = string.Join("", tmpArr);
                //4.sha1 散列
                string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
                hash = hash.ToLower();


                if (signature.Equals(hash))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(handler), ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 业务处理
        /// </summary>
        /// <param name="postString"></param>
        private void Execute(string postString)
        {

            //1.检验access_token
            new GetIp_list().Execute("");
            TracingHelper.Info("检验access_token完毕");
            //2.业务处理
            WeixinApiDispatch dispatch = new WeixinApiDispatch();
            string responseContent = dispatch.Execute(postString);
            TracingHelper.Info("业务处理完毕  " + responseContent);
            //3.返回微信服务器

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(responseContent);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}