//---------------------------------------
// Author: Lee
// Date: 2017/01/18
// Desc: ZGUIManager
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public class ZGUIManager : Singleton
    {
        public ZGUIManager() : base(true)
        {
            ZGUIResourceManager.Init();
            ZGUILayoutManager.Init();
            Inst = this;
        }

        public void UnInit()
        {
            ZGUIResourceManager.UnInit();
            ZGUILayoutManager.UnInit();
        }

        /// <summary>
        /// 加载UI资源
        /// </summary>
        public bool LoadUIResource(uint eResType, string strResourceName, OnUIResourceLoaded onLoad)
        {
            ZGUIResourceManager resMgr = ZGUIResourceManager.GetResMgr(eResType);
            if(null != resMgr)
            {
                return resMgr.LoadResource(strResourceName, onLoad);
            }
            return false;
        }
        public bool LoadUIResource(uint eResType, uint nResId, OnUIResourceLoaded onLoad)
        {
            ZGUIResourceManager resMgr = ZGUIResourceManager.GetResMgr(eResType);
            if (null != resMgr)
            {
                return resMgr.LoadResource(nResId, onLoad);
            }
            return false;
        }
        public void RemoveLoadUICallBack(uint eResType, OnUIResourceLoaded onLoad)
        {
            ZGUIResourceManager resMgr = ZGUIResourceManager.GetResMgr(eResType);
            if (null != resMgr)
            {
                resMgr.RemoveOnLoadCallBack(onLoad);
            }
        }

        /// <summary>
        /// 打开/关闭 界面
        /// </summary>
        public void ChangeActiveUI(bool bShow, uint eLayoutType, uint eLogicModuleIndex)
        {
            if (bShow)
            {
                ZGUILayoutManager.Inst.ShowUI(eLayoutType, eLogicModuleIndex);
            }
            else
            {
                ZGUILayoutManager.Inst.HideUI(eLayoutType, eLogicModuleIndex);
            }
        }

        public static ZGUIManager Inst = null;
    }
}
