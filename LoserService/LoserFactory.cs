using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;

namespace LoserService
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class LoserFactory
    { 

        public static ServiceBase CreateServiceType(string strType)
        {
            switch (strType)
            {
                case "0"://获取Access_token
                    return new GetAccess_token();
                default:
                    return new GetAccess_token();
            }
        }
    }
}
