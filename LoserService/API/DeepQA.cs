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
using LoserUtil;

namespace LoserService
{
    /// <summary>
    /// 图灵机器人
    /// </summary>
    public class DeepQA : ServiceBaseAPI
    {
       

        public override string Execute(InfoBase json)
        {
            bool connect = false;
            string xml = string.Empty;
            try
            {
                //组建参数字典
                Dictionary<string, string> Para = new Dictionary<string, string>();
                //Para.Add("key", ShareData.QAkye);
                //Para.Add("info", json.Contents);
                //Para.Add("userid", ShareData.QAuserid);

                string path = RequestPath.CreatePathApi("/turing/turing/turing");
                RpArticles newList = HTMLHelper.Get<RpArticles>(path, Para, ref connect, ShareData.baiduapikey);
                if (newList.code == "200" && newList != null)
                {
                    newList.xmlmsg = json;
                    xml = new replyImageText().ReplyExecute(newList);
                }
            }
            catch (Exception ex)
            {

                TracingHelper.Error(ex, typeof(DeepQA), ex.Message);
            }
            return xml;
        }

    }
}
