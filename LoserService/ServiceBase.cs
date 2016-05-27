using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserService
{
    public abstract class ServiceBase
    {
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public abstract String Execute(String json);
        

    }
}
