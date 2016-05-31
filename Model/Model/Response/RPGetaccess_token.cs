using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel.Response
{
    public class RPGetaccess_token : RPBase
    {
        private string _access_token;
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        private int _expires_in;
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }
    }
}
