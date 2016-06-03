using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserModel.Request;
using LoserUtil;
using LoserModel.Enum;
using LoserModel.replyModel;
using LoserModel;

namespace LoserService
{
    public class qyService : IWeixinAction
    {
        
        public string Execute(string postStr)
        {
            string content = string.Empty;
            try
            {
                RQBase rqInfo = SerializeHelper.XmlDeserialize(postStr);
                //TracingHelper.Info(rqInfo.xmlmsg.ToUserName + " " + rqInfo.xmlmsg.FromUserName);
                if (rqInfo != null)
                {
                    string FromUserName = rqInfo.xmlmsg.ToUserName;
                    string ToUserName = rqInfo.xmlmsg.FromUserName;
                    rqInfo.xmlmsg.ToUserName = ToUserName;
                    rqInfo.xmlmsg.FromUserName = FromUserName;
                    switch (rqInfo.xmlmsg.MsgType)
                    {
                        //文本、位置、链接。回复默认
                        case InfoType.text:
                        case InfoType.location:
                        case InfoType.link:
                            content = HandleText(rqInfo);
                            break;
                        case InfoType.image:
                            content = HandleImage(rqInfo);
                            break;
                        case InfoType.voice:
                            content = HandleVoice(rqInfo);
                            break;

                        case InfoType.video:
                        case InfoType.shortvideo:
                            content = HandleVideo(rqInfo);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex,typeof(WeixinApiDispatch),ex.Message);
            }
            return content;
        }

        /// <summary>
        /// 返回图片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string HandleImage(RQBase info)
        {
            //发送什么，返回什么

            string xml = string.Empty;
            try
            {
                RequestImage imgeInfo = new RequestImage();
                imgeInfo.xmlmsg = info.xmlmsg;
                imgeInfo.Image = new VoiceImage() { MediaId = info.MediaId };
                xml = new replyImage().ReplyExecute(imgeInfo);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            // string xml = SerializeHelper.XmlSerialize<RequestImage>(imgeInfo);
            TracingHelper.Info(" HandleImage  " + xml);
            return xml;
        }

        /// <summary>
        /// 返回视频
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string HandleVideo(RQBase info)
        {
            //发送什么，返回什么

            string xml = string.Empty;
            try
            {
                RequestVideo video = new RequestVideo();
                video.xmlmsg = info.xmlmsg;
                video.Video = new Video() { MediaId = info.MediaId, Title = info.Title, Description = info.Description };
                xml = new replyVideo().ReplyExecute(video);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            TracingHelper.Info("   HandleVideo" + xml);
            return xml;
        }

        /// <summary>
        /// 返回声音
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string HandleVoice(RQBase info)
        {
            //发送什么，返回什么

            string xml = string.Empty;
            try
            {
                RequestVoice voice = new RequestVoice();
                voice.xmlmsg = info.xmlmsg;
                voice.Voice = new VoiceImage() { MediaId = info.MediaId };
                xml = new replyVoice().ReplyExecute(voice);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            TracingHelper.Info("  HandleVoice " + xml);
            return xml;
        }

        public string HandleText(RQBase info)
        {
            string xml = string.Empty;
            //文字处理
            try
            {
                switch (info.Content)
                {
                    case "美女"://输入美女
                        xml = new Btgirl().Execute(info.xmlmsg);
                        break;
                    case "头条"://头条
                        xml = new TouTiao().Execute(info.xmlmsg);
                        break;
                    default:
                        RequestText voice = new RequestText();
                        voice.xmlmsg = info.xmlmsg;
                        //xml = new replyText().ReplyExecute(voice);
                        //xml = new DeepQA().Execute(info.xmlmsg);
                        break;
                }
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(WeixinApiDispatch), ex.Message);
            }
            TracingHelper.Info(" HandleText  " + xml);
            return xml;

        }
    }
}
