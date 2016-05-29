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
        
        public static ServiceBaseAPI CreateServiceType(string content)
        {
            switch (content)
            {
                case "美女"://输入美女
                    return new Btgirl();
                case "头条"://头条
                    return new TouTiao();
                case "笑话":
                    return new Getjoke();
                default:
                    return new DeepQA();
            }
        }
    }
}
