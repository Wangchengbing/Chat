using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;

namespace LoserModel.replyModel
{
    /// <summary>
    /// 图片信息实体
    /// </summary>
    public class RequestMusic : replyBase
    {
        public Music Music = new Music();
    }
    public class Music {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id，通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
