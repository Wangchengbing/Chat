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
    /// <summary>
    /// 回复文本
    /// </summary>
    public class replyText : ServiceBase
    {

        public override string ReplyExecute(replyBase json)
        {

            string xml = @"<xml>
  <ToUserName><![CDATA[{0}]]></ToUserName>
  <FromUserName><![CDATA[{1}]]></FromUserName>
  <CreateTime>{2}</CreateTime>
  <MsgType><![CDATA[{3}]]></MsgType>
  <Content><![CDATA[{4}]]></Content>
</xml>";
            RequestText json1 = json as RequestText;
            xml = string.Format(xml, json.ToUserName, json.FromUserName, json.CreateTime, json.MsgType, "你发的文字是：" + json1.Content);
            return xml;
        }
    }
}
