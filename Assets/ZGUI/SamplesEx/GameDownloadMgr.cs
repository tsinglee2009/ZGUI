//---------------------------------------
// Desc: Just A Download Sample Script
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

public class GameDownloadMgr
{
    public static Resource LoadResource(uint resId, OnLoadResource onLoad)
    {
        Resource res;
        if(m_resMap.TryGetValue(resId, out res))
        {
            return res;
        }
        else
        {
            StartDownloadResource(resId, onLoad);
            return null;
        }
    }

    private static void StartDownloadResource(uint resId, OnLoadResource onLoad)
    {
        OnLoadResource callBack;
        if(m_resOnLoadMap.TryGetValue(resId, out callBack))
        {
            if(null != onLoad)
            {
                callBack -= onLoad;
                callBack += onLoad;
            }
        }
        else
        {
            string strUrl = GetResourceUrl(resId);
            if(string.IsNullOrEmpty(strUrl))
            {
                Debug.LogError("cant find this resId's config");
                return;
            }
            m_resOnLoadMap.Add(resId, onLoad);
            Downloader.WWWDownloadResource(resId, strUrl, OnDownloadEnd);
        }
    }

    private static string GetResourceUrl(uint resId)
    {
        string strWWWUrl = ResourceConfig.GetResPathByID(resId);
        return strWWWUrl;
    }

    private static void OnDownloadEnd(uint resId, ref WWW www)
    {
        AssetBundle bundle = www.assetBundle;
        if(null == bundle)
        {
            Debug.LogError(resId + " assetBundle is null");
            return;
        }
        Resource resource = new Resource(resId, ref bundle);
        m_resMap.Add(resId, resource);

        OnLoadResource callBack;
        if (m_resOnLoadMap.TryGetValue(resId, out callBack))
        {
            m_resOnLoadMap.Remove(resId);
            if (null != callBack) callBack(resource);
        }
    }

    private static Dictionary<uint, Resource> m_resMap = new Dictionary<uint, Resource>();
    private static Dictionary<uint, OnLoadResource> m_resOnLoadMap = new Dictionary<uint, OnLoadResource>();
}

public delegate void OnLoadResource(Resource res);
public class Resource
{
    private Object[] m_data;
    public uint nResId;

    public Resource(uint resId, ref AssetBundle bundle)
    {
        nResId = resId;
        m_data = bundle.LoadAllAssets();
    }

    public Object[] LoadAssets()
    {
        return m_data;
    }
}
