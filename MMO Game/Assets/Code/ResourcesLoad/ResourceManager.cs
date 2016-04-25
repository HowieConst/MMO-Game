using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ResourceManager : SingletonMono<ResourceManager> 
{

    public class CachedGo
    {
       public string filename;
       public GameObject prefabgo;
       public float cachetime;
       public int count;
    }

    private ResourceAllocater Allocater = null;
    private BundleLoader mBundleLoader = null;
    public int mCachenumber = 10;
    public float mCachetime = 10;
    private List<CachedGo> mCachedGolist = new List<CachedGo>();     
    public IEnumerator LoadAsset(string assetname,GameObject parentgo,Action act)
    {
        yield return StartCoroutine(BundleLoader.Instance.LoadManifest());
        string asset = assetname;
        string assetbundlename = assetname.ToLower() + Config.BUNDLE_EXTENSION;
        yield return StartCoroutine(BundleLoader.Instance.LoadAssetbundleByName(assetbundlename, false));
        foreach (KeyValuePair<string, AssetBundle> kv in BundleLoader.Instance.mLoadedAssetBundle)
        {
            Debug.Log(kv.Key + kv.Value.name.ToString());
        }
        BundleLoader.Instance.CloneGO(assetbundlename, asset, parentgo,act);
    }
    public void MarkAlloc(string bundlename)
    {
        Allocater.MarkAlloc(bundlename);
    }
    public void MarkDeAlloc(string bundlename)
    {
        Allocater.MarkDeAlloc(bundlename);
    }
    public void MarkDeAllocDependcies(string filename)
    {
        string[] dependices = mBundleLoader.mMainfest.GetAllDependencies(filename);
        for (int i = 0; i < dependices.Length; i++)
        {
            mBundleLoader.UnLoadAssetBundle(dependices[i]);
        }
    }
    public CachedGo GetCachedGo(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return null;
        if (mCachedGolist != null && mCachedGolist.Count > 0)
        {
            for (int i = 0; i < mCachedGolist.Count; i++)
            {
                if (mCachedGolist[i].filename == filename)
                {
                    return mCachedGolist[i];
                }
            }
        }
        return null;
    }
    public void AddCacheGo(string filename,GameObject go)
    {
        CachedGo cache = GetCachedGo(filename);
        if (cache == null)
        {
            cache = new CachedGo();
            cache.filename = filename;
            cache.prefabgo = go;
            cache.cachetime = Time.realtimeSinceStartup;
            cache.count = 1;
            mCachedGolist.Add(cache);
        }
        else
        {
            cache.count += 1;
        }
    }

}
