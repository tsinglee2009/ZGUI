//---------------------------------------
// Desc: Just A Singleton Sample Script
//---------------------------------------
using UnityEngine;
using ZGUI;

public class Singleton
{
    public Singleton(bool bUpdate) { }

    public virtual void SingletonBeforeUpdate(float fTime, float fDTime) { }
    public virtual void SingletonUpdate(float fTime, float fDTime) { }
    public virtual void SingletonAfterUpdate(float fTime, float fDTime) { }
}

public class SingletonManager
{
    public static void Init()
    {
        m_singletonList.Add(new ZGUIManager());
        m_singletonList.Add(new LogicModuleManager());
    }

    private static BetterList<Singleton> m_singletonList = new BetterList<Singleton>();

    private static float m_fLastTime = Time.timeSinceLevelLoad;
    private static float m_fTime, m_fDTime;

    private static void CalculateTimeElapse()
    {
        m_fTime = Time.timeSinceLevelLoad;
        m_fDTime = m_fTime - m_fLastTime;
        m_fLastTime = m_fTime;
    }

    public static void SingletonBeforeUpdate()
    {
        CalculateTimeElapse();
        for(int i=0;i< m_singletonList.size; i++)
        {
            m_singletonList[i].SingletonBeforeUpdate(m_fTime, m_fDTime);
        }
    }
    public static void SingletonUpdate()
    {
        for (int i = 0; i < m_singletonList.size; i++)
        {
            m_singletonList[i].SingletonUpdate(m_fTime, m_fDTime);
        }
    }
    public static void SingletonAfterUpdate()
    {
        for (int i = 0; i < m_singletonList.size; i++)
        {
            m_singletonList[i].SingletonAfterUpdate(m_fTime, m_fDTime);
        }
    }

}
