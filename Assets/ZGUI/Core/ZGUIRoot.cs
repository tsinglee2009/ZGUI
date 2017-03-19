//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: ZGUIRoot作为所有界面的挂载点
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public class ZGUIRoot : ZUIBehaviour
    {
        public UIRoot m_uiRoot;
        public Camera m_uiCamera;

        public static ZGUIRoot Instance { get; private set; }
        public Transform cachedTrans { get; private set; }

        void Awake()
        {
            Instance = this;
            cachedTrans = transform;
        }

        public static void AttachZUI(ZUI ui)
        {
            Transform uiTrans = ui.transform;
            uiTrans.SetParent(Instance.cachedTrans);
            uiTrans.localScale = Vector3.one;
            uiTrans.localPosition = Vector3.zero;
            uiTrans.localRotation = Quaternion.identity;
        }
    }
}
