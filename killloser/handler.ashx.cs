using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Security;
namespace killloser
{
    /// <summary>
    /// handler 的摘要说明
    /// </summary>
    public class handler : IHttpHandler
    {

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
                    //Execute(postString);
                }
            }
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
            string token = ConfigurationManager.AppSettings["WeixinToken"];//从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
                //LogTextHelper.Error(string.Format("WeixinToken 配置项没有配置！"));
            }

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (checkSignature(token, signature, timestamp, nonce))
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


                ////4.SHA1散列
                //SHA1 sha = new SHA1CryptoServiceProvider();
                ////将mystr转换成byte[]
                //ASCIIEncoding enc = new ASCIIEncoding();
                //byte[] dataToHash = enc.GetBytes(tmpStr);
                ////Hash运算
                //byte[] dataHashed = sha.ComputeHash(dataToHash);
                ////将运算结果转换成string
                //string hash = BitConverter.ToString(dataHashed).Replace("-", "");
                //string hash1 = BitConverter.ToString(dataHashed);

                if (signature.Equals(hash))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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