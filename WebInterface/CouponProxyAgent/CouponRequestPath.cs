using System;
using System.Collections.Generic;
using System.Text;

namespace WebInterface
{
    /// <summary>
    /// 组合请求路径
    /// </summary>
    public class CouponRequestPath
    {
        /// <summary>
        /// 根据车场ID和车牌号，获取该车在该停车场的优免信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getavailable(string url, string name)
        {
            string res = url;
            //string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            res = res + "/service/1/couponrecords/" + name;
            //res = string.Format(res, version, name);

            return res;
        }

        /// <summary>
        /// 商家发放的优惠券使用
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string usebyupdate(string url, string name)
        {
            string res = url;
            //string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            res = res + "/service/1/couponrecords/" + name;
            //res = string.Format(res, version, name);

            return res;
        }

        /// <summary>
        /// 根据车场ID查询该车场所有优免信息列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getparkcoupons(string url, string name)
        {
            string res = url;
            //string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            res = res + "/service/1/" + name;
            //res = string.Format(res, version, name);

            return res;
        }

        /// <summary>
        /// 使用出场时选择的优惠券
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string usebyadd(string url, string name)
        {
            string res = url;
            //string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            res = res + "/service/1/couponrecords/" + name;
            //res = string.Format(res, version, name);

            return res;
        }
    }
}
