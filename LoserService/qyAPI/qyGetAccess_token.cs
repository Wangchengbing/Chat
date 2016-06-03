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

namespace LoserService
{
    public class qyGetAccess_token
    {
        public string Execute(string json)
        {
            bool connect = false;
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
           
            Para.Add("corpid", ShareData.qyCorpID);
            Para.Add("corpsecret", ShareData.qySecret);

            string path = RequestPath.CreatePathqy(RequestType.qyaccess_token);
            RPGetaccess_token tokenInfo = HTMLHelper.Get<RPGetaccess_token>(path, Para, ref connect);
            if (tokenInfo != null)
                ShareData.qyaccess_token = tokenInfo.access_token;
            return "";
        }
        
    }
}
