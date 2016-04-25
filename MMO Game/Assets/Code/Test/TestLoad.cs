using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestLoad : MonoBehaviour
{

	IEnumerator Start ()
    {
        yield return StartCoroutine(BundleLoader.Instance.LoadAssetbundleByName("Windows", true));
        yield return StartCoroutine(BundleLoader.Instance.LoadAssetbundleByName("ui_login.unity3d", false));
        AssetBundle assetbundle = null;
        foreach (KeyValuePair<string ,AssetBundle> kv in BundleLoader.Instance.mLoadedAssetBundle)
        {
            Debug.Log(kv.Key + kv.Value.name.ToString());
        }
        if (BundleLoader.Instance.mLoadedAssetBundle.TryGetValue("ui_login.unity3d", out assetbundle))
        {
            if (assetbundle != null)
            {
                Debug.Log(assetbundle.name);
                GameObject go = assetbundle.LoadAsset<GameObject>("UI_Login");
                if (go == null)
                {
                    Debug.Log("游戏物体为空！");
                }
                Instantiate(go);
            }
            else
            {
                Debug.Log("AssetBundle 为空--");
            }
        }
	}
}
