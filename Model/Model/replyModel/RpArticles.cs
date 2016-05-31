using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using Newtonsoft.Json;

namespace LoserModel.replyModel
{
    /// <summary>
    /// 图片信息实体
    /// </summary>
    public class RpArticles  : replyBase
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }
        public string msg { get; set; }
        public string text { get; set; }

        public List<newslist> newslist = new List<newslist>();

    }

    public class newslist
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 文章发布时间
        /// </summary>
        public string ctime { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string url { get; set; }
    }
}
