using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Security.Policy;

public class BundleLoader : SingletonMono<BundleLoader> 
{

    [HideInInspector]
    public AssetBundleManifest mMainfest;
    public Dictionary<string, AssetBundle> mLoadedAssetBundle = new Dictionary<string, AssetBundle>();

    public IEnumerator LoadAssetbundleByName(string bundlename,bool IsLoadMainfest)
    {
        string loadurl = GetLoadAssetBundlePath() + bundlename;
        Debug.Log(loadurl);
        if (IsLoadMainfest)
        {
            WWW www = new WWW(loadurl);
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield break;
            }
            mMainfest = (AssetBundleManifest)www.assetBundle.LoadAsset("AssetBundleManifest");
            if (mMainfest != null)
            {
                yield break;
            }
        }
        else
        {
            
            yield return StartCoroutine(LoadAllDependcies(bundlename));
        }
    }
    public IEnumerator LoadManifest()
    {
      string mainfestname = Getplatform();

        // 由于鞋哥申请的那个服务器的要求 manifest必须是网站能识别的格式 所以都加了.jpg后缀
      yield return StartCoroutine(LoadAssetbundleByName(mainfestname, true));
    }
    public  string Getplatform()
    {
        string assetbunldepath = "";
        RuntimePlatform plat = Application.platform;
        switch (plat)
        {
            case RuntimePlatform.Android:
                assetbunldepath ="Android" ;
                break;
            case RuntimePlatform.OSXDashboardPlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXWebPlayer:
                assetbunldepath = "IOS";
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
                assetbunldepath = "Windows";
                break;
            default:
                break;
        }
        return assetbunldepath;
    }
    public string GetLoadAssetBundlePath()
    {
        string loadpath = "";
        RuntimePlatform plat = Application.platform;
        switch (plat)
        {
            case RuntimePlatform.Android:
                loadpath = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
                //loadpath = "file://" + Application.streamingAssetsPath+"/";
                //loadpath =  "http://totoroz.3vzhuji.net/Windows/";
                loadpath = "file://" + "F:\\Data\\Windows\\";
                break;
            case RuntimePlatform.OSXDashboardPlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXWebPlayer:
                loadpath = "file://" + "F:\\Data\\IOS\\";
                break;
            default:
                break;
        }
        return loadpath;
    
    }
    IEnumerator LoadAssetBundle(string bundlename)
    {
        if (!mLoadedAssetBundle.ContainsKey(bundlename))
        {
            string loadurl = GetLoadAssetBundlePath() + bundlename;
            Debug.Log(loadurl);
            WWW www = new WWW(loadurl);
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield break;
            }
            mLoadedAssetBundle.Add(bundlename,www.assetBundle);
        }
    }
    public string[] GetDependices(string bundlename)
    {
        if (mMainfest != null)
        {  
           return  mMainfest.GetAllDependencies(bundlename);
        }
        return null;
    }
    IEnumerator LoadAllDependcies(string bundlename)
    {
        string[] dependices = GetDependices(bundlename);

        if (dependices == null || dependices.Length ==0)
        {
  
            yield return StartCoroutine(LoadAssetBundle(bundlename));
        }
        else
        {
            for (int i = 0; i < dependices.Length; i++)
            {
                Debug.Log(dependices[i]);
                yield return StartCoroutine(LoadAssetBundle(dependices[i]));
            }
            bool IsAllLoad = true;
            for (int i = 0; i < dependices.Length; i++)
            {
                while (!mLoadedAssetBundle.ContainsKey(dependices[i]))
                {
                    IsAllLoad = false;
                    break;
                }
            }
            if (IsAllLoad)
            {
                yield return StartCoroutine(LoadAssetBundle(bundlename));
            }
        }
     
    }
    public bool IsLoaded(string bundlename)
    {
        if (mLoadedAssetBundle.ContainsKey(bundlename))
        {
            return true;
        }
        return false;
    }
    public AssetBundle GetBundle(string bundlename)
    {
        if (mLoadedAssetBundle.ContainsKey(bundlename))
        { 
           return mLoadedAssetBundle[bundlename];
        }
        return null;
    }
    public void UnLoadAll()
    {
        if (mLoadedAssetBundle != null && mLoadedAssetBundle.Count > 0)
        {
            foreach (string key in mLoadedAssetBundle.Keys)
            {
                mLoadedAssetBundle[key].Unload(false);
            }
        }
    }
    public void UnLoadAssetBundle(string bundlename)
    {
        AssetBundle assetbundle = GetBundle(bundlename);
        assetbundle.Unload(false);
    }
    public void CloneGO(string bundlename,string assetname ,GameObject parentgo,Action act)
    {
  
        if (mLoadedAssetBundle != null && mLoadedAssetBundle.Count > 0)
        { 
           AssetBundle assetbundle = mLoadedAssetBundle[bundlename];
           if (assetbundle != null)
           {
             
                GameObject go= assetbundle.LoadAsset<GameObject>(assetname);
               if (go != null)
               {
                   GameObject Instancego = Instantiate(go) as GameObject;
                   Instancego.transform.SetParent(parentgo.transform);
                   Instancego.transform.localPosition = Vector3.zero;
                   Instancego.transform.localRotation = Quaternion.identity;
                   Instancego.transform.localScale = Vector3.one;
                   if (act != null)
                   {
                       act();
                   }

               }
           }
        }
    }
}
