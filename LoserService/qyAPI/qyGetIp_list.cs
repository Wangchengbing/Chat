using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using LoserModel;
using LoserServer;
using LoserModel.Request;
using LoserModel.Response;
using LoserModel.replyModel;
using LoserUtil;

namespace LoserService
{
    public class qyGetIp_list
    {
        public string Execute(string type)
        {
            bool connect = false;
            string path = string.Empty;
            try
            {
                //组建参数字典
                Dictionary<string, string> Para = new Dictionary<string, string>();
                Para.Add("access_token", ShareData.qyaccess_token);
                path = RequestPath.CreatePathqy(RequestType.Ip_list);
                RPGetIp_list IpListInfo = HTMLHelper.Get<RPGetIp_list>(path, Para, ref connect);
                if (IpListInfo == null || IpListInfo.errcode != null)
                {
                    //重新请求access_token
                    new qyGetAccess_token().Execute("");
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(GetIp_list), path + ex.ToString());
            }
            return "";
        }

    }
}
