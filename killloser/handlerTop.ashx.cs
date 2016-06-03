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
    /// handlerTop 的摘要说明
    /// </summary>
    public class handlerTop : IHttpHandler
    {
        WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(ShareData.qyToken, ShareData.qyEncodingAESKey, ShareData.qyCorpID);
        public void ProcessRequest(HttpContext context)
        {
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
                    TracingHelper.Info("qy Post " + postString);
                    Execute(postString);
                }
            }
            else
            {
                TracingHelper.Info("Url:  " + HttpContext.Current.Request.Url.ToString());
                AuthQY();
            }
        }
        /// <summary>
        /// 成为开发者的第一步，验证并相应服务器的数据  企业号
        /// </summary>
        private void AuthQY()
        {

            try
            {
                string msg_signature = HttpContext.Current.Request.QueryString["msg_signature"];
                string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
                string nonce = HttpContext.Current.Request.QueryString["nonce"];
                string echostr = HttpContext.Current.Request.QueryString["echoStr"];

                int ret = 0;
                string sEchoStr = "";

                ret = wxcpt.VerifyURL(msg_signature, timestamp, nonce, echostr, ref sEchoStr);
                if (ret != 0)
                {
                    TracingHelper.Info("qy ERR: VerifyURL fail, ret: " + ret);
                    return;
                }
                TracingHelper.Info("qy sEchoStrt: " + sEchoStr);
                HttpContext.Current.Response.Write(sEchoStr);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(handlerTop), ex.Message);
            }
        }



        /// <summary>
        /// 业务处理
        /// </summary>
        /// <param name="postString"></param>
        private void Execute(string postString)
        {
            postString = @"<xml><ToUserName><![CDATA[wx74bb1fb7abf4b203]]></ToUserName>
<Encrypt><![CDATA[mhY8AsexiiDpA7ilrwFrtDCY/opCpidX2eanBnPn/vZ0d4ck43K6+i2q7J7rdE+l78B44gwp/mr2YWGc4sSRyAPVq9hBoMT1Z9t9UgK3y94jQ1dfvi74b5zdKaAgPCg2xS5LwFJRgzUVuHNTsXkwh36Q90GhWArq6gRmX/K5RDDC24bqIqty4TVWcsf9kfCmSsjcG4BU5aXG2h/EYK5ErGkSHi5qsb650bZBPE68TnkQDbk/uvhKNJmRYj/rVMHX6wWHkjYy4KoHJGAWH8NWVvfKE8T9bJg0ku4bH6q+VKqi1tCTarR26pyzPpmvEqARILhqyNO1y/IJfx/bkjyhqtZvTZnjAdz827EvC2XcJkzGppkZdzbpNoILD93J4ty9S/SLBkj6TEhmKNXZssY3TrhJut6D6eOAcCIHJA4Vty0OjwIDuS8JPACORQUf+lh222pAnBeH3eoaq2EuIV35SA==]]></Encrypt>
<AgentID><![CDATA[0]]></AgentID>
</xml>";
            //1.检验access_token
            new qyGetIp_list().Execute("qy");
            TracingHelper.Info("qy检验access_token完毕");
            string sMsg = "";
            
            #region //2.AES解密
            try
            {
                int ret = 0;
                ret = wxcpt.DecryptMsg(postString, ref sMsg);
                if (ret != 0)
                {
                    System.Console.WriteLine("qy ERR: Decrypt Fail, ret: " + ret);
                    return;
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(handlerTop), "qy 消息解密 ： " + ex.Message);
            }
            #endregion

            //2.业务处理
            qyService qy = new qyService();
            string responseContent = qy.Execute(sMsg);
            TracingHelper.Info("qy业务处理完毕  " + responseContent);
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