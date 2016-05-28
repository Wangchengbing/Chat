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
    public class CreateMenu
    {
        public string Execute(string json)
        {

            //妈的，没权限尼玛~  报错40016.谁知道没有这权限啊。草
            bool connect = false;
            //建立菜单集合
            Menu MenuList = new Menu();

            //第一个主菜单  
            MenuInfo m1 = new MenuInfo();
            m1.type = Buttontype.click;
            m1.name = "今日头条";
            m1.key = "rrr";
            
            
            MenuList.button.Add(m1);

            //第二个主菜单
            MenuInfo m2 = new MenuInfo();
            m2.name = "菜单";

            //建立第二主》子菜单
            MenuInfo sub21 = new MenuInfo();
            sub21.type = Buttontype.view;
            sub21.name = "搜索";
            sub21.url = "http://www.baidu.com/";
            MenuInfo sub22 = new MenuInfo();
            sub22.type = Buttontype.scancode_waitmsg;
            sub22.name = "扫码带提示";
            sub22.key = "V1001_TODAY_MUSIC";
            MenuInfo sub23 = new MenuInfo();
            sub23.type = Buttontype.pic_photo_or_album;
            sub23.name = "拍照或者相册发图";
            sub23.key = "V1001_TODAY_MUSIC";
            MenuInfo sub24 = new MenuInfo();
            sub24.type = Buttontype.location_select;
            sub24.name = "发送位置";
            sub24.key = "V1001_TODAY_MUSIC";

            m2.sub_button.Add(sub21);
            m2.sub_button.Add(sub22);
            m2.sub_button.Add(sub23);
            m2.sub_button.Add(sub24);


            MenuList.button.Add(m2);


            string RqJson = JsonHelper.Serialize(MenuList);
            //组建参数字典
            Dictionary<string, string> Para = new Dictionary<string, string>();
            Para.Add("access_token", ShareData.access_token);
            Para.Add("body", RqJson);


            string path = RequestPath.CreatePath(RequestType.createMenu);
            RPBase IpListInfo = HTMLHelper.Post<RPBase>(path, Para, ref connect);
            if (IpListInfo != null)
            {

            }
            return "";
        }
        
    }
}
