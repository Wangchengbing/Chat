using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using System.Web.Security;
namespace LoserUtil
{
    public static class ChatTokenCheck
    {
        #region 第一步：验证微信签名部分，使用时直接调用Valid方法即可

        #region 验证微信签名 保持默认即可
        /// <summary>
        /// 验证微信签名
        /// <para>* 将token、timestamp、nonce三个参数进行字典序排序</para>
        /// <para>* 将三个参数字符串拼接成一个字符串进行sha1加密</para>
        /// <para>* 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。</para>
        /// </summary>
        /// <param name="Token">微信的Token签名，自己写的或者定义的</param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool CheckSignature(string Token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 微信验证验证方法，需要微信提供的四个参数，分别是signature，timestamp，nonce，echoStr
        /// <para>以上四种参数需要Get方式获取，C#使用Request.QueryString方式</para>
        /// </summary>
        /// <param name="signature">微信发送来的signature</param>
        /// <param name="timestamp">微信发送来的timestamp</param>
        /// <param name="nonce">微信发送来的nonce</param>
        /// <param name="echoStr">微信发送来的echoStr</param>
        public static void Valid(string Token, string signature, string timestamp, string nonce, string echoStr)
        {
            if (CheckSignature(Token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    //PublicCommon.ResponseWriteString(echoStr);
                }
            }
        }
        #endregion

        #region SHA1加密方式,对字符串进行加密
        /// <summary>
        /// SHA1加密方式,对字符串进行加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns>加密后的字符串</returns>
        public static string GetSha1(string text)
        {
            byte[] cleanBytes = System.Text.Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
        #endregion

        #region SHA1加密方式，对list数组进行加密
        /// <summary>
        /// SHA1加密方式，对list数组进行加密
        /// </summary>
        /// <param name="codelist"></param>
        /// <returns></returns>
        public static string GetSha1(System.Collections.Generic.List<string> codelist)
        {
            codelist.Sort();
            var combostr = string.Empty;
            for (int i = 0; i < codelist.Count; i++)
            {
                combostr += codelist[i];
            }
            return EncryptToSHA1(combostr);
        }
        #endregion

        #region SHA1加密处理，加密至SHA1，主要是复杂的加密方式
        /// <summary>
        /// SHA1加密处理，加密至SHA1，主要是复杂的加密方式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptToSHA1(string str)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] str1 = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

        #endregion

    }
}