using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserModel.Request;
using LoserUtil;
using LoserModel.Enum;
using LoserModel.replyModel;

namespace LoserService
{
    public class WeixinApiDispatch : IWeixinAction
    {

        public string Execute(string postStr)
        {
            string content = @"KillLose
                                     无梦想不青春";
            try
            {
                RQBase rqInfo = SerializeHelper.XmlDeserialize(postStr);
                TracingHelper.Info(rqInfo.ToUserName + " " + rqInfo.FromUserName);
                if (rqInfo != null)
                {
                    switch (rqInfo.MsgType)
                    {
                        case InfoType.text:
                            content = HandleText(rqInfo);
                            break;
                        case InfoType.image:
                        case InfoType.voice:
                        case InfoType.video:
                        case InfoType.shortvideo:
                        case InfoType.location:
                        case InfoType.link:
                            content = HandleMedia(rqInfo);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            return content;
        }

        public string HandleMedia(RQBase info)
        {
            //发送什么，返回什么

            string xml = string.Empty;
            //1.回复文字
            //2.回复图片
            try
            {
                RequestImage imgeInfo = new RequestImage();
                imgeInfo.FromUserName = info.ToUserName;
                imgeInfo.ToUserName = info.FromUserName;
                imgeInfo.CreateTime = SerializeHelper.longtime().ToString();
                imgeInfo.MsgType = InfoType.image;
                imgeInfo.Image = new VoiceImage() { MediaId = info.MediaId };
                ServiceBase service = LoserFactory.CreateServiceType(imgeInfo.MsgType);
                xml = service.ReplyExecute(imgeInfo);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex,typeof(WeixinApiDispatch), ex.Message);
            }
            // string xml = SerializeHelper.XmlSerialize<RequestImage>(imgeInfo);
            TracingHelper.Info(" HandleMedia  "+xml);
            return xml;
        }

        public string HandleText(RQBase info)
        {
            string xml = string.Empty;
            //发送什么，返回什么
            //1.回复文字
            try
            {
                RequestText textInfo = new RequestText();
                textInfo.FromUserName = info.ToUserName;
                textInfo.ToUserName = info.FromUserName;
                textInfo.CreateTime = SerializeHelper.longtime().ToString();
                textInfo.MsgType = InfoType.text;
                textInfo.Content = info.Content;
                ServiceBase service = LoserFactory.CreateServiceType(textInfo.MsgType);
                xml = service.ReplyExecute(textInfo);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            //string xml = SerializeHelper.XmlSerialize<RequestText>(textInfo);
            TracingHelper.Info(" HandleText  " + xml);
            return xml;

        }
    }
}
