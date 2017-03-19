//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: ZGUIResourceManager
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public delegate void OnUIResourceLoaded();

    public abstract class ZGUIResourceManager
    {
        private static Dictionary<uint, ZGUIResourceManager> m_uiResManagerMap = new Dictionary<uint, ZGUIResourceManager>();
        public static void Init()
        {
            m_uiResManagerMap.Add((uint)E_ZUIResType.Panel, new ZGUIPanelResManager());
            m_uiResManagerMap.Add((uint)E_ZUIResType.Atlas, new ZGUIAtlasResManager());
            m_uiResManagerMap.Add((uint)E_ZUIResType.Fonts, new ZGUIFontsResManager());
        }
        public static void UnInit()
        {
            var item = m_uiResManagerMap.GetEnumerator();
            try
            {
                while(item.MoveNext())
                {
                    item.Current.Value.OnUnInit();
                }
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static ZGUIResourceManager GetResMgr(uint eType)
        {
            ZGUIResourceManager resMgr;
            if(m_uiResManagerMap.TryGetValue(eType, out resMgr))
            {
                return resMgr;
            }
            else
            {
                return null;
            }
        }

        //-------------------------------------------------------------------------
        // 资源查询
        public bool Contains(string strResName)
        {
            uint nResId = GetUIResIdByName(ref strResName);
            return Contains(nResId);
        }
        public bool Contains(uint nResId)
        {
            return m_uiResourceList.Contains(nResId);
        }
        // 加载资源
        public bool LoadResource(string strResName, OnUIResourceLoaded onLoad)
        {
            uint nResId = GetUIResIdByName(ref strResName);
            return LoadResource(nResId, onLoad);
        }
        public bool LoadResource(uint resId, OnUIResourceLoaded onLoad)
        {
            if(Contains(resId))
            {
                return true;
            }
            else
            {
                if (null != onLoad) AddOnLoadCallBack(onLoad);

                Resource res = GameDownloadMgr.LoadResource(resId, OnLoadResource);
                if(null != res)
                {
                    OnLoadResource(res);
                    return true;
                }

                return false;
            }
        }
        // 卸载资源
        public bool UnloadResource(string strResName)
        {
            uint nResId = GetUIResIdByName(ref strResName);
            return UnloadResource(nResId);
        }
        public bool UnloadResource(uint resId)
        {
            return OnUnload(resId);
        }
        // 资源加载完成
        private void OnLoadResource(Resource res)
        {
            uint resId = res.nResId;
            if(m_uiResourceList.Contains(resId))
            {
                Debug.LogError("重复加载ui资源 " + resId);
                m_onLoadCallBack = null;
                return;
            }
            m_uiResourceList.Add(resId);

            Object[] objs = res.LoadAssets();
            if (OnLoad(resId, objs))
            {
                ProcessOnLoadCallBack();
            }
            else
            {
                m_onLoadCallBack = null;
            }
        }
        // 卸载
        private void OnUnInit()
        {
            m_onLoadCallBack = null;
            OnDestroy();
            m_uiResourceList.Clear();
            m_uiResourceList = null;
        }

        protected abstract bool OnLoad(uint resId, Object[] objs);
        protected abstract bool OnUnload(uint resId);
        protected abstract void OnDestroy();

        // 通过ui资源名获取资源id
        private uint GetUIResIdByName(ref string strResName)
        {
            // 访问底层接口
            uint resId = ResourceConfig.GetResIdByName(strResName);
            return resId;
        }

        public void RemoveOnLoadCallBack(OnUIResourceLoaded onLoad)
        {
            if (null != m_onLoadCallBack)
                m_onLoadCallBack -= onLoad;
        }
        protected void AddOnLoadCallBack(OnUIResourceLoaded onLoad)
        {
            if (null != m_onLoadCallBack)
                m_onLoadCallBack -= onLoad;
            m_onLoadCallBack += onLoad;
        }
        protected void ProcessOnLoadCallBack()
        {
            if (null != m_onLoadCallBack) m_onLoadCallBack();
            m_onLoadCallBack = null;
        }

        private BetterList<uint> m_uiResourceList = new BetterList<uint>();
        protected OnUIResourceLoaded m_onLoadCallBack = null;
    }
}
