//---------------------------------------
// Desc: Just A Download Sample Script
//---------------------------------------
using UnityEngine;
using ZGUI;

class GameEntry : MonoBehaviour
{
    void Start()
    {
        SingletonManager.Init();
        LogicModuleManager.Inst.InitGameModules();
        S_GlobalConfig.m_eGameState = E_GlobalGameState.Game;
    }

    void Update()
    {
        SingletonManager.SingletonBeforeUpdate();
        // ....
        SingletonManager.SingletonUpdate();
        //
        DoTest();
    }

    void LateUpdate()
    {
        SingletonManager.SingletonAfterUpdate();
    }

    void DoTest()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            LogicModule module = LogicModuleManager.Inst.GetLogicModule((uint)E_LogicModuleIndex.Main);
            if(null != module)
            {
                module.SetVisible(!module.IsShow());
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LogicModule module = LogicModuleManager.Inst.GetLogicModule((uint)E_LogicModuleIndex.UI_Role);
            if (null != module)
            {
                module.SetVisible(!module.IsShow());
            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            LogicModule module = LogicModuleManager.Inst.GetLogicModule((uint)E_LogicModuleIndex.UI_Bag);
            if (null != module)
            {
                module.SetVisible(!module.IsShow());
            }
        }
    }
}
