//---------------------------------------
// Desc: Just A Download Sample Script
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public delegate void OnDownloadEnd(uint resId, ref WWW www);
public class Downloader : MonoBehaviour
{
    private uint m_resId = 0;
    private static List<Downloader> m_waiteList = new List<Downloader>();
    public static void WWWDownloadResource(uint resId, string strUrl, OnDownloadEnd onLoadEnd)
    {
        Downloader downloader = null;
        if(m_waiteList.Count > 0)
        {
            downloader = m_waiteList[0];
            m_waiteList.RemoveAt(0);
        }
        else
        {
            GameObject go = new GameObject("_downloader");
            downloader = go.AddComponent<Downloader>();
        }
        downloader.gameObject.SetActive(true);
        downloader.StartDownload(resId, strUrl, onLoadEnd);
    }

    void StartDownload(uint resId, string strUrl, OnDownloadEnd onLoadEnd)
    {
        m_resId = resId;
        StartCoroutine(Download(strUrl, onLoadEnd));
    }

    IEnumerator Download(string strUrl, OnDownloadEnd onLoadEnd)
    {
        WWW www = new WWW(strUrl);
        yield return www;
        if(null != onLoadEnd)
        {
            onLoadEnd(m_resId, ref www);
        }
        www.assetBundle.Unload(false);
        www.Dispose();
        RemoveToWaite();
    }

    void RemoveToWaite()
    {
        m_resId = 0;
        gameObject.SetActive(false);
        m_waiteList.Add(this);
    }
}
