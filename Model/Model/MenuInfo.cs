using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoserModel
{
    public class Menu
    {
        public List<MenuInfo> button = new List<MenuInfo>();

    }


    public class MenuInfo {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuInfo> sub_button = new List<MenuInfo>();
        private string _type;
        private string _name;
        private string _key;
        private string _url;
        private string _media_id;
        /// <summary>
        /// 菜单动作类型
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
        /// <summary>
        /// media_id类型和view_limited类型必须
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string media_id
        {
            get
            {
                return _media_id;
            }

            set
            {
                _media_id = value;
            }
        }
        /// <summary>
        /// view类型必须
        /// 网页链接，用户点击菜单可打开链接，不超过256字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        /// <summary>
        /// click等点击类型必须
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        /// 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }
        
    }
}
