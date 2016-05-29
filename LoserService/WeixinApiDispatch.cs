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
    public class WeixinApiDispatch : IWeixinAction
    {

        /// <summary>
        /// 获取主菜单
        /// </summary>
        /// <returns></returns>
        private static string GetMenu()
        {
            System.Text.StringBuilder retsb = new StringBuilder(200);
            retsb.Append("欢迎各位小主的关注\n\n");
            retsb.Append("宝宝会争做大家喜欢的公众号\n\n");
            retsb.Append("小主们可以直接提建议\n\n");
            retsb.Append("小主回复美女、头条获取最新报道\n\n");
            retsb.Append("还可以跟偶随便聊天哟\n");
            return retsb.ToString() + msgFinal();
        }
        /// <summary>
        /// 字符串后加入返回菜单项
        /// </summary>
        /// <returns></returns>
        public static string msgFinal()
        {
            return "\nKillLose\n\r无梦想不青春";
        }
        /// <summary>
        /// 返回帮助信息
        /// </summary>
        /// <returns></returns>
        public static string MsgHelpInfo()
        {
            System.Text.StringBuilder retsb = new StringBuilder(200);
            retsb.Append("【1/2】请选择您需要办理的业务\n");
            retsb.Append("查询还款金额，，或请编辑发送：HKJE#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("查询账单，，或请编辑发送：CXZD#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("定制电子账单（Email），请编辑发送：DZZD#卡号#email#密码器动态密码或短信银行密码；\n");
            retsb.Append("查询额度，，或请编辑发送：CXED#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("查询自动还款，请编辑发送：ZDHK#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("查询办卡进度，请编辑发送：CXBK#身份证号；\n");
            retsb.Append("查询还款日，请编辑发送：HKR#卡号；\n");
            retsb.Append("查询电子账单地址，请编辑发送：CXDZ#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("取消纸质账单，请编辑发送：QXZZ#卡号#密码器动态密码或短信银行密码；\n");
            retsb.Append("补寄账单，请编辑发送：BJZD#卡号#YYYYMM#email#密码器动态密码或短信银行密码；\n");
            return retsb.ToString() + msgFinal();
        }
        public string Execute(string postStr)
        {
            
            string content = msgFinal();
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
                        voice.Content = GetMenu();
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
