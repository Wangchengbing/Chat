using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel.Enum
{
    public struct InfoType
    {
        //文本消息
        public const string text = "text";
        //图片消息
        public const string image = "image";
        //语音消息
        public const string voice = "voice";
        //视频消息
        public const string video = "video";
        //小视频消息
        public const string shortvideo = "shortvideo";
        //地理位置消息
        public const string location = "location";
        //链接消息
        public const string link = "link";
        //音乐信息
        public const string music = "music";
    }
}
