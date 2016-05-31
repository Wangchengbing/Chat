using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace LoserDAL
{
    public abstract class Config
    {
        private Config()
        {

        }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["connstring"];
            }
        }
        /// <summary>
        /// 数据库链接字符串  读取
        /// </summary>
        public static string ConnectionStringRead
        {
            get
            {
                return ConfigurationManager.AppSettings["connstringRead"];
            }
        }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionStringLog
        {
            get
            {
                return ConfigurationManager.AppSettings["connstringLog"];
            }
        }

        /// <summary>
        /// 配置读取的记录条数
        /// </summary>
        public static string TopSum
        {
            get
            {
                return ConfigurationManager.AppSettings["topsum"];
            }
        }
        /// <summary>
        /// 每页条数
        /// </summary>
        public static int PageSize
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
            }
        }
    }
}
