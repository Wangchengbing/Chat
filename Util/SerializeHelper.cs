using LoserModel.Enum;
using LoserModel.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LoserUtil
{

    public class SerializeHelper
    {
        public static string XmlSerialize<T>(T obj) where T : class
        {
            try
            {
                if (obj == null) return null;
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, obj);
                    stream.Position = 0;

                    StreamReader sr = new StreamReader(stream);
                    string resultStr = sr.ReadToEnd();
                    sr.Close();
                    stream.Close();
                    return resultStr;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T XmlDeserialize<T>(string xml) where T : class
        {
            T result = default(T);
            if (string.IsNullOrEmpty(xml))
                return result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml.ToCharArray())))
                {
                    result = (T)serializer.Deserialize(ms);
                    ms.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 处理消息类
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static RQBase XmlDeserialize(string xml)
        {

            try
            {
                //封装请求类
                XmlDocument requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(xml);
                XmlElement rootElement = requestDocXml.DocumentElement;
                //XmlNodeList xnl = rootElement.ChildNodes;
                //foreach (XmlNode xn in xnl)
                //{
                //    XmlElement xe = (XmlElement)xn;
                    
                    
                //}
                RQBase WxXmlModel = new RQBase();
                WxXmlModel.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                WxXmlModel.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                WxXmlModel.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                WxXmlModel.MsgType = rootElement.SelectSingleNode("MsgType").InnerText;
                WxXmlModel.MsgId = rootElement.SelectSingleNode("MsgId").InnerText;

                switch (WxXmlModel.MsgType)
                {
                    case InfoType.text:
                        WxXmlModel.Content = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                    case InfoType.image:
                        WxXmlModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        WxXmlModel.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                        break;
                    case InfoType.voice:
                        WxXmlModel.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                        WxXmlModel.Format = rootElement.SelectSingleNode("Format").InnerText;
                        WxXmlModel.Recognition = rootElement.SelectSingleNode("Recognition").InnerText;
                        break;
                    case InfoType.video:
                    case InfoType.shortvideo:
                        WxXmlModel.MediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                        WxXmlModel.ThumbMediaId = rootElement.SelectSingleNode("ThumbMediaId").InnerText;
                        break;
                    case InfoType.location:
                        WxXmlModel.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        WxXmlModel.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        WxXmlModel.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                        WxXmlModel.Label = rootElement.SelectSingleNode("Label").InnerText;
                        break;
                    case InfoType.link:
                        WxXmlModel.Title = rootElement.SelectSingleNode("Title").InnerText;
                        WxXmlModel.Description = rootElement.SelectSingleNode("Description").InnerText;
                        WxXmlModel.Url = rootElement.SelectSingleNode("Url").InnerText;
                        break;
                }
                return WxXmlModel;
            }
            catch (Exception ex)
            {
                TracingHelper.Error(ex, typeof(SerializeHelper), ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 时间整型
        /// </summary>
        /// <returns></returns>
        public static long longtime()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 1389753949);
            DateTime now = Convert.ToDateTime(DateTime.Now);
            DateTime baseTime = now - ts;
            baseTime = Convert.ToDateTime("1970-1-1 8:00:00");
            ts = DateTime.Now - baseTime;
            long intervel = (long)ts.TotalSeconds;
            return intervel;
        }
    }
}
