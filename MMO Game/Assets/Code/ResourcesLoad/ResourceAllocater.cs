using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceAllocater : SingletonMono<ResourceAllocater>
{
    public class RefReason
    {
       public  string filename;
       public byte count;
    }
    BundleLoader mLoader = null;
    List<RefReason> mAllocaterList = new List<RefReason>();

    public RefReason GetRefReason(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return null;
        if (mAllocaterList != null && mAllocaterList.Count > 0)
        {
            for (int i = 0; i < mAllocaterList.Count; i++)
            {
                if (mAllocaterList[i].filename == filename)
                {
                    return mAllocaterList[i];
                }
            }
        }
        return null;
      
    }
    public void  MarkAlloc(string filename)
    {
        RefReason refreason = GetRefReason(filename);
        if (refreason != null)
        {
            refreason.count += 1;
        }
        else
        {
            RefReason reason = new RefReason();
            reason.filename = filename;
            reason.count = 1;
            mAllocaterList.Add(reason);
        }
    }
    public void MarkDeAllocAll()
    {
        for (int i = 0; i < mAllocaterList.Count; i++)
        {
            string filename = mAllocaterList[i].filename;
            this.mLoader.UnLoadAssetBundle(filename);
        }
        mAllocaterList.Clear();
    }
    public void MarkDeAlloc(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return;
        RefReason refseason = GetRefReason(filename);
        if (refseason != null)
        {
            refseason.count--;
            if (refseason.count <= 0)
            {
                this.mLoader.UnLoadAssetBundle(refseason.filename);
                mAllocaterList.Remove(refseason);
            }
        }
     
    }
    public void RemoveRefseason(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return;
        for (int i = 0; i < mAllocaterList.Count; i++)
        {
            if (mAllocaterList[i].filename == filename)
            {
                RefReason reason = mAllocaterList[i];
                mAllocaterList.Remove(reason);
            }
        }
    }

}
