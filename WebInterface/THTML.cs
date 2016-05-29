using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace LoserServer
{
    /// <summary>
    /// 模拟请求
    /// </summary>
    public class HTMLHelper
    {
        #region Get
        /// <summary>
        /// 获取网页的源文件 HTML
        /// </summary>
        /// <param name="strurl_">URL包含IP</param>
        /// <param name="code_">编码</param>
        /// <returns></returns>
        public static string Get(string strurl_, string code_, ref bool isError_, string apikey = "")
        {
            string str = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strurl_);
                request.Method = "GET";
                request.Timeout = 5000;
                request.ReadWriteTimeout = 3000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                if (resStream != null)
                {
                    StreamReader sr = new StreamReader(resStream, Encoding.GetEncoding(code_));
                    str = sr.ReadToEnd();
                    resStream.Close();
                    sr.Close();
                }
                isError_ = false;
            }
            catch
            {
                isError_ = true;
            }
            return str;
        }
        /// <summary>
        /// 获取网页的源文件 HTML  百度api
        /// </summary>
        /// <param name="strurl_">URL包含IP</param>
        /// <returns></returns>
        public static string GetBaidu(string strurl_, string apikey)
        {
            string str = "";
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strurl_);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", apikey);
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.UTF8);
            str = sr.ReadToEnd();
            s.Close();
            sr.Close();
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strurl_"></param>
        /// <returns></returns>
        public static string Get(string strurl_, ref bool isError_, string apikey = "")
        {
            if (string.IsNullOrEmpty(apikey))
                return Get(strurl_, "utf-8", ref isError_);
            else
                return GetBaidu(strurl_, apikey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL_"></param>
        /// <param name="Value_"></param>
        /// <returns></returns>
        public static T Get<T>(string URL_, Dictionary<string, string> Value_, ref bool isError_, string apikey = "")
        {
            if (Value_ != null && Value_.Count > 0)
            {
                URL_ += "?";
                foreach (var item in Value_)
                {
                    URL_ += item.Key + "=" + HttpUtility.UrlEncode(item.Value) + "&";
                }
                URL_ = URL_.Substring(0, URL_.Length - 1);
            }
            string json = Get(URL_, ref isError_, apikey);
            return JsonHelper.DeserializeOnly<T>(json);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL_"></param>
        /// <param name="Value_"></param>
        /// <returns></returns>
        public static T Get<T>(string url_, string param_, ref bool isError_)
        {
            string json = Get(url_ + param_, ref isError_);
            return JsonHelper.DeserializeOnly<T>(json);
        }
        #endregion

        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="MyCookie"></param>
        /// <param name="url"></param>
        /// <param name="s"></param>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string PostData(CookieContainer MyCookie, string url, string s, string code, ref bool result)
        {
            result = false;
            string str = "";
            Encoding encode = System.Text.Encoding.GetEncoding(code);
            byte[] arrB = encode.GetBytes(s);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.Method = "POST";
            myReq.Timeout = 5000;
            myReq.ReadWriteTimeout = 3000;
            myReq.CookieContainer = MyCookie;

            myReq.ContentType = "application/x-www-form-urlencoded";
            myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myReq.ContentLength = arrB.Length;

            try
            {
                Stream outStream = myReq.GetRequestStream();
                outStream.Write(arrB, 0, arrB.Length);
                outStream.Close();
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                Stream ReceiveStream = myResp.GetResponseStream();
                StreamReader readStream = new StreamReader(ReceiveStream, Encoding.GetEncoding(code));
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    str += new String(read, 0, count);
                    count = readStream.Read(read, 0, 256);
                }
                readStream.Close();
                myResp.Close();
                result = true;
            }
            catch
            {
                result = false;
            }
            return str;
        }
        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="MyCookie"></param>
        /// <param name="url_"></param>
        /// <param name="param_"></param>
        /// <param name="code_"></param>
        /// <param name="result_"></param>
        /// <returns></returns>
        public static string PostData(string url_, string param_, string code_, ref bool result_)
        {
            string str = "";
            Encoding encode = Encoding.GetEncoding(code_);
            byte[] arrB = encode.GetBytes(param_);
            HttpWebRequest myReq = WebRequest.Create(url_) as HttpWebRequest;
            myReq.Method = "POST";
            myReq.Timeout = 5000;
            myReq.ReadWriteTimeout = 5000;
            myReq.ContentType = "application/x-www-form-urlencoded";
            //myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            myReq.ContentLength = arrB.Length;
            try
            {
                Stream outStream = myReq.GetRequestStream();
                outStream.Write(arrB, 0, arrB.Length);
                outStream.Close();
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                Stream ReceiveStream = myResp.GetResponseStream();
                StreamReader readStream = new StreamReader(ReceiveStream, Encoding.GetEncoding(code_));
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    str += new String(read, 0, count);
                    count = readStream.Read(read, 0, 256);
                }
                readStream.Close();
                myResp.Close();
                outStream = null;
                readStream = null;
                myResp = null;
                myReq = null;

                result_ = true;
            }
            catch
            {
                result_ = false;
            }

            return str;
        }

        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="MyCookie"></param>
        /// <param name="url_"></param>
        /// <param name="param_"></param>
        /// <param name="code_"></param>
        /// <param name="result_"></param>
        /// <returns></returns>
        public static T Post<T>(string url_, string param_, string code_, ref bool result_)
        {
            string str = PostData(url_, param_, code_, ref result_);

            return JsonHelper.DeserializeOnly<T>(str);
        }

        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="MyCookie"></param>
        /// <param name="url_"></param>
        /// <param name="param_"></param>
        /// <param name="code_"></param>
        /// <param name="result_"></param>
        /// <returns></returns>
        public static T Post<T>(string url_, string param_, ref bool result_)
        {
            return Post<T>(url_, param_, "utf-8", ref result_);
        }

        /// <summary>
        /// Post数据
        /// </summary>
        /// <param name="MyCookie"></param>
        /// <param name="url_"></param>
        /// <param name="param_"></param>
        /// <param name="code_"></param>
        /// <param name="result_"></param>
        /// <returns></returns>
        public static T Post<T>(string url_, Dictionary<string, string> Value_, ref bool result_)
        {
            string parm = string.Empty;
            if (Value_ != null && Value_.Count > 0)
            {
                foreach (var item in Value_)
                {
                    parm += item.Key + "=" + HttpUtility.UrlEncode(item.Value) + "&";
                }
                parm = parm.Substring(0, parm.Length - 1);
            }
            return Post<T>(url_, parm, "utf-8", ref result_);
        }
    }
}
