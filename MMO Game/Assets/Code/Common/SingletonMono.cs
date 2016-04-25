using UnityEngine;
using System.Collections;

public class SingletonMono<T> : MonoBehaviour where T : Component
{
    public static T mInstance;
    private static object mLockobj = new object();
    private static bool mIsAppQiut = false;
    public static T Instance
    {
        get
        {
            if (mIsAppQiut)
            {
                Debug.Log("程序已经退出");
            }
            lock(mLockobj)
            {
                if(ReferenceEquals(mInstance,null))
                {
                    mInstance = (T)FindObjectOfType(typeof(T));
                    if(mInstance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString()+"_singlton");
                        mInstance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }
            
            }
            return mInstance;
        }
    }
    void OnDestroy()
    {
        mIsAppQiut = true;
    }
}
