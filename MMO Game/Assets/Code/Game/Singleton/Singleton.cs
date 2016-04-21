using UnityEngine;
using System.Collections;
interface ISinglton
{
    void Init();    
}
public abstract class Singleton<T> :ISinglton where T:new()
{
    private static T mInstance;
    public static T Instace
    {
        get
        {
            return Singleton<T>.mInstance;
        }
    }
    public static T CreateInstance()
    {
        if (Singleton<T>.mInstance == null)
        {
            mInstance = new T();
        }
        return Singleton<T>.mInstance;
    }
    public virtual void Init()
    {

    }

}

