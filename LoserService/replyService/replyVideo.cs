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
    public class replyVideo : ServiceBasereply
    {

        public override string ReplyExecute(replyBase json)
        {

            string xml = @"<xml>
                              <ToUserName><![CDATA[{0}]]></ToUserName>
                              <FromUserName><![CDATA[{1}]]></FromUserName>
                              <CreateTime>{2}</CreateTime>
                              <MsgType><![CDATA[video]]></MsgType>
                              <Video>
                                <MediaId><![CDATA[{3}]]></MediaId>
                                <Title><![CDATA[{4}]]></Title>
                                <Description><![CDATA[{5}]]></Description>
                              </Video>
                            </xml>";
            RequestVideo json1 = json as RequestVideo;
            xml = string.Format(xml, json.xmlmsg.ToUserName, json.xmlmsg.FromUserName,SerializeHelper.longtime().ToString(), json1.Video.MediaId, json1.Video.Title, json1.Video.Description);
            return xml;
        }
    }
}
