//---------------------------------------
// Author: Lee
// Date: 2017/01/18
// Desc: ZUI作为单个界面的根节点
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public delegate void ZUIOnPanelLoadedCallBack();

    [ExecuteInEditMode]
    public class ZUI : ZUIBehaviour
    {
        private UIPanel m_uiPanel;
        private Transform m_cachedTrans;
        private bool m_bInited = false;

        void Awake()
        {
            Init();
        }

        public void Init()
        {
            if(m_bInited)
            {
                return;
            }
            m_bInited = true;

            m_cachedTrans = transform;
            if (m_cachedTrans.childCount != 1)
            {
                Debug.LogError("ZUIPanel must have 1 child whith UIPanel Attached !");
            }
            else
            {
                m_uiPanel = m_cachedTrans.GetChild(0).GetComponent<UIPanel>();
            }
            m_uiPanel.depth = (int)LayoutStyle();
        }

        public void AttachToGame()
        {
            ZGUIRoot.AttachZUI(this);
        }

        public virtual uint LayoutStyle()
        {
            return (uint)E_ZUILayoutStyle.UI;
        }

        public bool SetVisible(bool bShow, bool bNeedAnim = false)
        {
            m_uiPanel.enabled = bShow;
            return true;
        }

        public bool IsShow()
        {
            return m_uiPanel.isActiveAndEnabled;
        }
    }
}
