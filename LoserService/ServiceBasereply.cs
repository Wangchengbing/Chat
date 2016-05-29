using LoserModel.replyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserService
{
    public abstract class ServiceBasereply
    {
        

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public abstract String ReplyExecute(replyBase json);

    }
}
