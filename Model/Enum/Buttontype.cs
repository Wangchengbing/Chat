using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel
{
    public struct Buttontype
    {
        //点击推事件
        public const string click = "click";
        //跳转URL
        public const string view = "view";
        //扫码推事件
        public const string scancode_push = "scancode_push";
        //扫码推事件且弹出“消息接收中”提示框
        public const string scancode_waitmsg = "scancode_waitmsg";
        //弹出系统拍照发图
        public const string pic_sysphoto = "pic_sysphoto";
        //弹出拍照或者相册发图
        public const string pic_photo_or_album = "pic_photo_or_album";
        //弹出微信相册发图器
        public const string pic_weixin = "pic_weixin";
        //弹出地理位置选择器
        public const string location_select = "location_select";
        //下发消息（除文本消息）
        public const string media_id = "media_id";
        //跳转图文消息URL
        public const string view_limited = "view_limited";
    }
}
