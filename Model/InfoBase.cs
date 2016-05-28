using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel
{
    /// <summary>
    /// 信息基类
    /// </summary>
    public class InfoBase
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 信息类型  text  voice  image  video  shortvideo  link   location
        /// </summary>
        public string MsgType { get; set; }
        

    }
}
