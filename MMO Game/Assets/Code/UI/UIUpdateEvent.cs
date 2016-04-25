using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public enum UIUpdateEvent : uint
{
   


}
public class UIEventHandler
{
    public static Dictionary<UIUpdateEvent, List<DlgBase>> mRigisiteDic = new Dictionary<UIUpdateEvent, List<DlgBase>>();
    public static void RigisteEvent(UIUpdateEvent uievent,DlgBase dlgbase)
    {
        List<DlgBase> dlgbasesList = null;
        mRigisiteDic.TryGetValue(uievent, out dlgbasesList);
        if (dlgbasesList != null)
        {
            dlgbasesList.Add(dlgbase);
        }
        else
        {
            List<DlgBase> baseList = new List<DlgBase>();
            baseList.Add(dlgbase);
            mRigisiteDic[uievent] = baseList;
        }
    }
    public static void UnRigisteEvent(UIUpdateEvent uievent,DlgBase dlgbase)
    { 
       List<DlgBase> dlgbaselist = null;
       mRigisiteDic.TryGetValue(uievent, out dlgbaselist);
       if (dlgbaselist != null)
       {
           for (int i = 0; i < dlgbaselist.Count; i++)
           {
               if (dlgbaselist[i].DlgName == dlgbase.DlgName)
               {
                   dlgbaselist.Remove(dlgbaselist[i]);
               }
           }
       }
    }
    public static void PostUIEvent(UIUpdateEvent uievnt)
    {
        List<DlgBase> dlgbaselist = new List<DlgBase>();
        mRigisiteDic.TryGetValue(uievnt,out dlgbaselist);
        if (dlgbaselist != null)
        {
            for (int i = 0; i < dlgbaselist.Count; i++)
            {
                dlgbaselist[i].ProcessUIEvent();
            }
        }
    }
    
}
