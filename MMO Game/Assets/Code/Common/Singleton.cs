using UnityEngine;
using System.Collections;

public class Singleton<T> where T: new()
{
    public static T mInstance;
    public static T Instance
    {
       get
       {
           return ReferenceEquals(mInstance, null) ? mInstance = new T() : mInstance;
       }
    }

}
