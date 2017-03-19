//---------------------------------------
// Author: Lee
// Date: 3/18/2017
// Desc: ZUIConstants
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public struct S_ZUIName
    {
        public const string Main = "panel_main";  //登陆
        public const string Role = "panel_role";    //角色
        public const string Bag = "panel_bag";      //背包
    }

    public enum E_ZUILayoutStyle
    {
        None = 1,
        Main,   //桌面常驻区域
        UI,     //常规界面
        Box,    //二级弹框
        Tips,   //悬浮tips
        Loading,//加载界面

        Max
    }

    public enum E_ZUIResType
    {
        Panel,  //面板
        Atlas,  //图集
        Fonts,  //字库
        Max
    }

    public struct S_OnUILoadCallBackType
    {
        public const int LOGIC_UIINIT = -2;
    }

}
