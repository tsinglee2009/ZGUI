//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: ZGUILayoutManager
//---------------------------------------
using System.Collections.Generic;

namespace ZGUI
{
    public class ZGUILayoutManager
    {
        public static ZGUILayoutManager Inst = null;
        public static void Init()
        {
            Inst = new ZGUILayoutManager();
        }
        public static void UnInit()
        {

        }

        // 同类界面只显示一个
        private Dictionary<uint, uint> m_uiActiveDict = new Dictionary<uint, uint>();
        private BetterList<uint> m_uiDisactiveList = new BetterList<uint>();
        
        public void ShowUI(uint eLayoutType, uint eLogicModuleIndex)
        {
            if(m_uiDisactiveList.Contains(eLogicModuleIndex))
            {
                m_uiDisactiveList.Remove(eLogicModuleIndex);
            }

            uint curActiveMoudleIndex;
            if(m_uiActiveDict.TryGetValue(eLayoutType, out curActiveMoudleIndex))
            {
                if(curActiveMoudleIndex == eLogicModuleIndex)
                {
                    return;
                }
                // 隐藏逻辑界面
                LogicModule module = LogicModuleManager.Inst.GetLogicModule(curActiveMoudleIndex);
                if(null != module)
                {
                    module.SetVisible(false);
                }
                m_uiActiveDict[eLayoutType] = eLogicModuleIndex;
            }
            else
            {
                m_uiActiveDict.Add(eLayoutType, eLogicModuleIndex);
            }
        }

        public void HideUI(uint eLayoutType, uint eLogicModuleIndex)
        {
            if (m_uiDisactiveList.Contains(eLogicModuleIndex))
            {
                return;
            }
            m_uiDisactiveList.Add(eLogicModuleIndex);
        }
    }
}
