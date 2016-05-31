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
    public class RequestText : replyBase
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }
    }
}
