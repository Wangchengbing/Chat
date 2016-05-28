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
    public class replyImage : ServiceBase
    {

        public override string ReplyExecute(replyBase json)
        {

            string xml = @"<xml>
  <ToUserName><![CDATA[{0}]]></ToUserName>
  <FromUserName><![CDATA[{1}]]></FromUserName>
  <CreateTime>{2}</CreateTime>
  <MsgType><![CDATA[{3}]]></MsgType>
  <Image>
    <MediaId><![CDATA[{4}]]></MediaId>
  </Image>
</xml>";
            RequestImage json1 = json as RequestImage;
            xml = string.Format(xml, json.ToUserName, json.FromUserName, json.CreateTime, json.MsgType, json1.Image.MediaId);
            return xml;
        }
    }
}
