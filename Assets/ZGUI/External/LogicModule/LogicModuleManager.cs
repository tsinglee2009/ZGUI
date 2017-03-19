//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: 逻辑模块管理器
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public struct S_GlobalConfig
    {
        public static E_GlobalGameState m_eGameState = E_GlobalGameState.Login;
    }
    public enum E_GlobalGameState//临时写在这里
    {
        Login,      //登陆
        Select,     //创角
        Game        //游戏
    }

    public delegate LogicModule OnCreateLogicModule(E_LogicModuleIndex logicIndex);

    public class LogicModuleManager : Singleton
    {
        public LogicModuleManager() : base(true)
        {
            Inst = this;
            CreateLogicModules();
        }

        private void CreateLogicModules()
        {
            // 登陆创角
            // 游戏内
            m_dicLoginCreateModules.Add((uint)E_LogicModuleIndex.Main,
                new MainModule() { m_moduleIndex = (uint)E_LogicModuleIndex.Main, m_strUIName = S_ZUIName.Main });
            m_dicLoginCreateModules.Add((uint)E_LogicModuleIndex.UI_Role,
                new RoleModule() { m_moduleIndex = (uint)E_LogicModuleIndex.UI_Role, m_strUIName = S_ZUIName.Role });
            m_dicLoginCreateModules.Add((uint)E_LogicModuleIndex.UI_Bag,
                new BagModule() { m_moduleIndex = (uint)E_LogicModuleIndex.UI_Bag, m_strUIName = S_ZUIName.Bag });
        }

        public void InitLoginModules()
        {
            var item = m_dicLoginCreateModules.GetEnumerator();
            try
            {
                while (item.MoveNext())
                {
                    LogicModule module = item.Current.Value as LogicModule;
                    if (null != module)
                    {
                        module.Init();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }
        public void InitGameModules()
        {
            var item = m_dicLoginCreateModules.GetEnumerator();
            try
            {
                while (item.MoveNext())
                {
                    LogicModule module = item.Current.Value as LogicModule;
                    if (null != module)
                    {
                        module.Init();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        public LogicModule GetLogicModule(uint eLogicModuleIndex)
        {
            LogicModule module = null;
            if (m_dicLoginCreateModules.TryGetValue(eLogicModuleIndex, out module))
            {
                return module;
            }
            if (m_dicGameCreateModules.TryGetValue(eLogicModuleIndex, out module))
            {
                return module;
            }
            return null;
        }

        public T GetLogicModule<T>(uint eLogicModuleIndex) where T : LogicModule
        {
            LogicModule module = null;
            if (m_dicLoginCreateModules.TryGetValue(eLogicModuleIndex, out module))
            {
                return (T)module;
            }
            if (m_dicGameCreateModules.TryGetValue(eLogicModuleIndex, out module))
            {
                return (T)module;
            }
            return null;
        }

        public override void SingletonUpdate(float fTime, float fDTime)
        {
            if(S_GlobalConfig.m_eGameState == E_GlobalGameState.Login ||
                S_GlobalConfig.m_eGameState == E_GlobalGameState.Select)
            {
                var item = m_dicLoginCreateModules.GetEnumerator();
                try
                {
                    while(item.MoveNext())
                    {
                        LogicModule module = item.Current.Value as LogicModule;
                        if(null != module)
                        {
                            module.Update(fTime, fDTime);
                        }
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
            else if(S_GlobalConfig.m_eGameState == E_GlobalGameState.Game)
            {
                var item = m_dicGameCreateModules.GetEnumerator();
                try
                {
                    while (item.MoveNext())
                    {
                        LogicModule module = item.Current.Value as LogicModule;
                        if (null != module)
                        {
                            module.Update(fTime, fDTime);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void DestroyLogicModules()
        {
            var loginItem = m_dicLoginCreateModules.GetEnumerator();
            try
            {
                while (loginItem.MoveNext())
                {
                    LogicModule module = loginItem.Current.Value as LogicModule;
                    if (null != module)
                    {
                        module.UnInit();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
            m_dicLoginCreateModules.Clear();
            m_dicLoginCreateModules = null;

            var gameItem = m_dicGameCreateModules.GetEnumerator();
            try
            {
                while (gameItem.MoveNext())
                {
                    LogicModule module = gameItem.Current.Value as LogicModule;
                    if (null != module)
                    {
                        module.UnInit();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
            m_dicGameCreateModules.Clear();
            m_dicGameCreateModules = null;
        }

        public static LogicModuleManager Inst;
        private Dictionary<uint, LogicModule> m_dicLoginCreateModules = new Dictionary<uint, LogicModule>();
        private Dictionary<uint, LogicModule> m_dicGameCreateModules = new Dictionary<uint, LogicModule>();
    }
}
