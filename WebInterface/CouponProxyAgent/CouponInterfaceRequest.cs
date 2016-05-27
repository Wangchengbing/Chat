using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography;

namespace WebInterface
{
    /// <summary>
    /// 微信接口机请求类
    /// </summary>
    public class CouponInterfaceRequest
    {
        public static string ApiUrl = "https://api.weixin.qq.com/cgi-bin/token";

        #region Post 

        /// <summary>
        /// 使用出场时选择的优惠券
        /// </summary>
        /// <param name="parkId">车场ID</param>
        /// <param name="vehicleNo">车牌号</param>
        /// <param name="couponFreeId">优免券ID</param>
        /// <param name="use_operator_id">使用优免操作员ID</param>
        /// <param name="use_amount">优免使用时折合金额</param>
        /// <param name="parking_record_id">停车记录ID号</param>
        /// <param name="Message_"></param>
        /// <returns></returns>
        public UseCouponResult usebyadd(string parkId, string vehicleNo, int couponFreeId, int use_operator_id, decimal use_amount, string parking_record_id, ref string Message_, string qrCode = "")
        {
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("parkId", EncryptionUtility.DesEncrypt(parkId, "x#a-y6nl"));
            Para.Add("vehicleNo", vehicleNo);
            Para.Add("couponFreeId", couponFreeId.ToString());
            Para.Add("use_operator_id", use_operator_id.ToString());
            Para.Add("use_amount", string.Format("{0:N2}", use_amount));
            Para.Add("parking_record_id", parking_record_id.ToUpper());
            if (!string.IsNullOrEmpty(qrCode))
                Para.Add("qr_code", qrCode);
            
            bool connect = false;
            string path = CouponRequestPath.usebyadd(ApiUrl, "use");
            UseCouponResult result = HTMLHelper.Post<UseCouponResult>(path, Para, ref connect);

            if (!connect)
            {
                Message_ = "网络连接异常";
                return null;
            }
            else if (result.code != 0)
            {
                Message_ = result.message;
                return null;
            }
            else
            {
                Message_ = "请求成功";
                return result;
            }
        }

        /// <summary>
        /// 商家发放的优惠券使用
        /// </summary>
        /// <param name="id">优免券ID</param>
        /// <param name="use_operator_id">使用优免操作员ID</param>
        /// <param name="use_amount">优免使用时折合金额</param>
        /// <param name="parking_record_id">停车记录ID号</param>
        /// <param name="Message_"></param>
        /// <returns></returns>
        public UseCouponResult usebyupdate(string id, string use_operator_id, decimal use_amount, string parking_record_id, ref string Message_)
        {
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("id", id);
            Para.Add("use_operator_id", use_operator_id);
            Para.Add("use_amount", string.Format("{0:N2}", use_amount));
            Para.Add("parking_record_id", parking_record_id);

            bool connect = false;
            string path = CouponRequestPath.usebyupdate(ApiUrl, "usebyupdate");
            UseCouponResult result = HTMLHelper.Post<UseCouponResult>(path, Para, ref connect);

            if (!connect)
            {
                Message_ = "网络连接异常";
                return null;
            }
            else if (result.code != 0)
            {
                Message_ = result.message;
                return null;
            }
            else
            {
                Message_ = "请求成功";
                return result;
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取用户可用优免
        /// </summary>
        /// <param name="parkId">停车场ID</param>
        /// <param name="vehicleNo">车牌号</param>
        /// <param name="Message_"></param>
        /// <returns></returns>
        public GetAvailableResult Getavailable(string parkId, string vehicleNo, ref string Message_)
        {
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("parkId", EncryptionUtility.DesEncrypt(parkId, "x#a-y6nl"));
            Para.Add("vehicleNo", vehicleNo);

            bool connect = false;
            string path = CouponRequestPath.getavailable(ApiUrl, "available");
            GetAvailableResult result = HTMLHelper.Get<GetAvailableResult>(path, Para, ref connect);

            if (connect)
            {
                Message_ = "网络连接异常";
                return null;
            }
            else if (result.code!=0)
            {
                Message_ = result.message;
                return null;
            }
            else
            {
                Message_ = "请求成功";
                return result;
            }
        }

        /// <summary>
        /// 查询车场所有优免信息列表
        /// </summary>
        /// <param name="parkId">停车场ID</param>
        /// <param name="Message_"></param>
        /// <returns></returns>
        public GetParkCouponsResult Getparkcoupons(string parkId, ref string Message_)
        {
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("parkId", EncryptionUtility.DesEncrypt(parkId, "x#a-y6nl"));

            bool connect = false;
            string path = CouponRequestPath.getparkcoupons(ApiUrl, "parkcoupons");
            GetParkCouponsResult result = HTMLHelper.Get<GetParkCouponsResult>(path, Para, ref connect);

            if (connect)
            {
                Message_ = "网络连接异常";
                return null;
            }
            else if (result.code != 0)
            {
                Message_ = result.message;
                return null;
            }
            else
            {
                Message_ = "请求成功";
                return result;
            }
        }

        #endregion

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        private string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

    }
}
