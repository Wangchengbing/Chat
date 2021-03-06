﻿using System;
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
    public class GetAccess_token
    {
        public string Execute(string json)
        {
            bool connect = false;
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("grant_type", "client_credential");
            Para.Add("appid", ShareData.wxAppid);
            Para.Add("secret", ShareData.wxSecret);

            string path = RequestPath.CreatePath(RequestType.access_token);
            RPGetaccess_token tokenInfo = HTMLHelper.Get<RPGetaccess_token>(path, Para, ref connect);
            if (tokenInfo != null)
                ShareData.wxaccess_token = tokenInfo.access_token;
            return "";
        }
        
    }
}
