using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;

namespace LoserModel.replyModel
{
    /// <summary>
    /// 文本信息实体
    /// </summary>
    public class RequestVideo : replyBase
    {
        public Video Video = new Video();
    }
    public class Video
    {
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }
    }
}
