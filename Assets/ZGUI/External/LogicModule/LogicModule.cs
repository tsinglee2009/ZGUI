//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: 逻辑模块类
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public abstract class LogicModule
    {
        public uint m_moduleIndex;
        public string m_strUIName;
        protected abstract ZUI UI { get; set; }
        protected virtual bool NeedShowWithAnim { get; set; }

        private bool m_bUIChecked = false;
        private bool m_bUILoaded = false;
        private bool m_bLoading = false;
        private bool m_bQuestShow = false;
        private bool m_bFirstOpen = true;

        public void Init()
        {
            LogicInit();
            CheckUI();
        }

        protected virtual void LogicInit() { }
        protected virtual void UIInit() { }

        public virtual bool SetVisible(bool bShow)
        {
            if (bShow == IsShow())
            {
                return m_bUILoaded;
            }
            else
            {
                return _SetVisible(bShow);
            }
        }

        private bool _SetVisible(bool bShow)
        {
            if(m_bLoading)
            {
                // 正在界面下载中
                m_bQuestShow = bShow;
                return false;
            }

            if(bShow && !m_bUILoaded)
            {
                if(!m_bUIChecked)
                {
                    CheckUI();
                }
                if(!m_bUILoaded)
                {
                    m_bLoading = true;
                    if (ZGUIManager.Inst.LoadUIResource((uint)E_ZUIResType.Panel, m_strUIName, OnPanelResourceLoaded))
                    {
                        CheckUI();
                        m_bLoading = false;
                    }
                    else
                    {
                        m_bQuestShow = bShow;
                    }
                }
            }

            if (m_bUILoaded)
            {
                if(bShow == IsShow())
                {
                    return true;
                }

                ZGUIManager.Inst.ChangeActiveUI(bShow, UI.LayoutStyle(), m_moduleIndex);
                UI.SetVisible(bShow);
                if(m_bFirstOpen)
                {
                    OnFirstOpen();
                    m_bFirstOpen = false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CheckUI()
        {
            GameObject go = GameObject.Find(m_strUIName);
            if (null == go)
            {
                if (!m_bUIChecked)
                {
                    m_bUIChecked = true;
                }
                else
                {
                    Debug.LogError("cant find ui " + m_strUIName);
                }
                return;
            }
            UI = go.GetComponent<ZUI>();
            if (null == UI)
            {
                Debug.LogError("cant find ZUIPanel " + m_strUIName);
                return;
            }
            UIInit();
            m_bUILoaded = true;
        }
        private void OnPanelResourceLoaded()
        {
            CheckUI();
            m_bLoading = false;
            SetVisible(m_bQuestShow);
        }

        public bool IsShow()
        {
            if (!m_bUILoaded)
            {
                return false;
            }
            else if (null == UI)
            {
                return false;
            }
            else
            {
                return UI.IsShow();
            }
        }

        protected virtual void OnFirstOpen()
        {
            
        }
        public virtual void Update(float fTime, float fDTime)
        {

        }
        public virtual void UnInit()
        {

        }

        public void SetOnLoadedCallBack(int nType, ZUIOnPanelLoadedCallBack callBack)
        {
            ZUIOnPanelLoadedCallBack _callBack = null;
            if (m_uiOnLoadedCallBack.TryGetValue(nType, out _callBack))
            {
                m_uiOnLoadedCallBack[nType] = callBack;
            }
            else
            {
                m_uiOnLoadedCallBack.Add(nType, callBack);
            }
        }
        public void DelOnLoadedCallBack(int nType)
        {
            ZUIOnPanelLoadedCallBack _callBack = null;
            if (m_uiOnLoadedCallBack.TryGetValue(nType, out _callBack))
            {
                m_uiOnLoadedCallBack.Remove(nType);
            }
        }
        public bool HasOnLoadedCallBack(int nType)
        {
            ZUIOnPanelLoadedCallBack _callBack = null;
            if (m_uiOnLoadedCallBack.TryGetValue(nType, out _callBack))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Dictionary<int, ZUIOnPanelLoadedCallBack> m_uiOnLoadedCallBack = new Dictionary<int, ZUIOnPanelLoadedCallBack>();
    }
}
