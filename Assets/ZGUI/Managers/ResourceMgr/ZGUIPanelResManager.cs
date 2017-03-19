//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: ZGUIPanelResManager
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZGUI
{
    public class ZGUIPanelResManager : ZGUIResourceManager
    {
        protected override void OnDestroy()
        {

        }

        protected override bool OnLoad(uint resId, UnityEngine.Object[] objs)
        {
            if(null == objs || objs.Length != 1)
            {
                return false;
            }

            GameObject zpanelObj = GameObject.Instantiate(objs[0]) as GameObject;
            if(null == zpanelObj)
            {
                return false;
            }

            zpanelObj.name = zpanelObj.name.Replace("(Clone)", "");
            ZUI uiComp = zpanelObj.GetComponent<ZUI>();
            if(null != uiComp)
            {
                uiComp.Init();
                uiComp.SetVisible(false);
                uiComp.AttachToGame();
            }

            return true;
        }

        protected override bool OnUnload(uint resId)
        {
            return true;
        }
    }
}
