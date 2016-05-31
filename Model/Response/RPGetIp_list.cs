using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel.Response
{
    public class RPGetIp_list : RPBase
    {
        private List<string> _ip_list = new List<string>();
        /// <summary>
        /// 微信服务器IP地址列表
        /// </summary>
        public List<string> ip_list
        {
            get { return _ip_list; }
            set { _ip_list = value; }
        }
    }
}
