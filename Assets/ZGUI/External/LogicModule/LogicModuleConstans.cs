//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: LogicModuleConstans
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public enum E_LogicModuleIndex
    {
        Login,          //登陆界面
        Select,         //创角界面
        Loading,        //加载界面

        Main = 10,//主界面角色信息区域(左上角)

        UI_Role = 20,   //角色界面
        UI_Bag,         //背包界面

        UI_Map = 30,    //世界地图

        UI_Shop = 40,   //商城界面

        Max
    }
}
