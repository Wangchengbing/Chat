using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserUtil.Enum
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public struct RequestType
    {

        //获取access_token
        public const string access_token = "token";
        //api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
        //获取微信服务器IP地址
        public const string Ip_list = "getcallbackip";
        //api.weixin.qq.com/cgi-bin/getcallbackip?access_token=ACCESS_TOKEN
        //建立自定义菜单
        public const string createMenu = "menu/create";
        //api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN    POST（请使用https协议）

    }
}
