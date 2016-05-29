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
    /// <summary>
    /// 回复文本
    /// </summary>
    public class replyText : ServiceBasereply
    {

        public override string ReplyExecute(replyBase json)
        {

            string xml = @"<xml>
  <ToUserName><![CDATA[{0}]]></ToUserName>
  <FromUserName><![CDATA[{1}]]></FromUserName>
  <CreateTime>{2}</CreateTime>
  <MsgType><![CDATA[text]]></MsgType>
  <Content><![CDATA[{3}]]></Content>
</xml>";
            RequestText json1 = json as RequestText;
            xml = string.Format(xml, json.xmlmsg.ToUserName, json.xmlmsg.FromUserName, SerializeHelper.longtime().ToString(),json1.Content);
            return xml;
        }
    }
}
