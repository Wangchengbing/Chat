﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;

namespace LoserModel.replyModel
{
    /// <summary>
    /// 文本信息实体
    /// </summary>
    public class RequestImage : replyBase
    {
        public VoiceImage Image = new VoiceImage();
    }
}
