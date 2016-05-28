using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using LoserModel.Enum;
using LoserModel.replyModel;
using LoserModel.Request;

namespace LoserService
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class LoserFactory
    { 

        public static ServiceBase CreateServiceType(string info)
        {
            switch (info)
            {
                case InfoType.text://回复文字
                    return new replyText();

                case InfoType.image://回复图片
                    return new replyImage();
                case InfoType.video://回复视频
                    return new replyVideo();
                case InfoType.voice://回复声音
                    return new replyVoice();
                default:
                    return new replyText();
            }
        }
        public static ServiceBase CreateServiceType(RQBase info)
        {
            switch (info.MsgType)
            {
                case InfoType.text://回复文字
                    return new replyText();
                case InfoType.image://回复图片
                    return new replyImage();
                case InfoType.video://回复视频
                    return new replyVideo();
                case InfoType.voice://回复声音
                    return new replyVoice();
                default:
                    return new replyText();
            }
        }
    }
}
