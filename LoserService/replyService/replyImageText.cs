using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoserUtil.Enum;
using LoserModel;
using LoserServer;
using LoserModel.Request;
using LoserModel.Response;
using LoserModel.replyModel;

namespace LoserService
{
    /// <summary>
    /// 回复图文
    /// </summary>
    public class replyImageText : ServiceBasereply
    {

        public override string ReplyExecute(replyBase json)
        {
            RpArticles art = json as RpArticles;

            string resxml = string.Empty;
            int count = 0;
            resxml += string.Format(@"<xml>
                                            <ToUserName><![CDATA[{0}]]></ToUserName>  
                                            <FromUserName><![CDATA[{1}]]></FromUserName>  
                                            <CreateTime>{2}</CreateTime>  
                                            <MsgType><![CDATA[news]]></MsgType>  
                                            <ArticleCount>{3}</ArticleCount>  
                                            <Articles>",
                        art.xmlmsg.ToUserName, art.xmlmsg.FromUserName, DateTime.Now, art.newslist.Count);
            foreach (newslist itemArticles in art.newslist)
            {
                count++;
                if (count <= 10)
                {
                    resxml += string.Format(@"<item>  
                                            <Title><![CDATA[{0}]]></Title>   
                                            <Description><![CDATA[{1}]]></Description>  
                                            <PicUrl><![CDATA[{2}]]></PicUrl>  
                                            <Url><![CDATA[{3}]]></Url>  
                                            </item>",
                    itemArticles.title, itemArticles.description, itemArticles.picUrl, itemArticles.url);
                }
            }
            resxml += "</Articles></xml>  ";
            return resxml;
        }
    }
}
