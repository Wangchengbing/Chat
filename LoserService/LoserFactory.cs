using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using LoserModel.Enum;
using LoserModel.replyModel;

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

                case InfoType.image://回复文字
                    return new replyImage();
                default:
                    return new replyText();
            }
        }
    }
}
