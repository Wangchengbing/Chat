using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using LoserModel;
using LoserServer;
using LoserModel.Request;
using LoserModel.Response;

namespace LoserService
{
    public class GetIp_list : ServiceBase
    {
        public override string Execute(string json)
        {
            bool connect = false;
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("access_token", ShareData.access_token);

            string path = RequestPath.CreatePath(RequestType.Ip_list);
            RPGetIp_list IpListInfo = HTMLHelper.Get<RPGetIp_list>(path, Para, ref connect);
            if (IpListInfo == null && IpListInfo.errcode != null)
            {
                //重新请求access_token
                new GetAccess_token().Execute("");
            }
            return "";
        }
    }
}
