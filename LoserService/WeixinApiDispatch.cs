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
                TracingHelper.Error(ex.Message);
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
                imgeInfo.FromUserName = info.ToUserName;
                imgeInfo.ToUserName = info.FromUserName;
                imgeInfo.CreateTime = SerializeHelper.longtime().ToString();
                imgeInfo.MsgType = InfoType.image;
                imgeInfo.Image = new VoiceImage() { MediaId = info.MediaId };
                xml = new replyImage().ReplyExecute(imgeInfo);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex.Message);
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
                video.FromUserName = info.ToUserName;
                video.ToUserName = info.FromUserName;
                video.CreateTime = SerializeHelper.longtime().ToString();
                video.MsgType = InfoType.video;
                video.Video = new Video() { MediaId = info.MediaId, Title= info.Title, Description=info.Description };
                xml = new replyVideo().ReplyExecute(video);
            }
            catch (Exception ex)
            {
                TracingHelper.Error( ex.Message);
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
                voice.FromUserName = info.ToUserName;
                voice.ToUserName = info.FromUserName;
                voice.CreateTime = SerializeHelper.longtime().ToString();
                voice.MsgType = InfoType.voice;
                voice.Voice = new VoiceImage() { MediaId = info.MediaId };
                xml = new replyVoice().ReplyExecute(voice);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex.Message);
            }
            TracingHelper.Info("  HandleVoice " + xml);
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
                xml = new replyText().ReplyExecute(textInfo);
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex.Message);
            }
            TracingHelper.Info(" HandleText  " + xml);
            return xml;

        }
    }
}
