using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIDlgManager : Bean<UIDlgManager>
{
    public class DlgData
    {  

        public DlgBase dlg;
        public GameObject dlggo;
        public float mCloseTime;
        public bool isvisible;
    }

    private string mCurrentManagerName = "";
    public Dictionary<string, DlgData> mVisibelContainer = new Dictionary<string, DlgData>();
    public Dictionary<string, DlgData> mLoaderContainer = new Dictionary<string, DlgData>();

    private GameObject mCanvas;
    public GameObject Canvas
    {
        get { return mCanvas; }
        set { mCanvas = value; }
    }

    private UIDlgManager mCurrentManager;
    public UIDlgManager Currentmanger
    {
        get { return mCurrentManager; }
        set { mCurrentManager = value; }
    }
    public string CurrentManagerName
    {
        get
        {
            return mCurrentManagerName;
        }
    }
    public virtual void init()
    {

    }
    protected virtual DlgBase RigisterInstance(GameObject go, string dlgname)
    {
        return null;
    }
    public  DlgBase GetDialogByName(string dlgname)
    {
        if (mLoaderContainer != null && mLoaderContainer.Count >= 1)
        {
            if (mLoaderContainer.ContainsKey(dlgname))
            {
                return mLoaderContainer[dlgname].dlg;
            }
        }
        GameObject go = new GameObject(dlgname);
        go.transform.SetAsLastSibling();
        go.SetActive(false);
        go.transform.parent = Currentmanger.Canvas.transform; 
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        DlgBase dlg = Currentmanger.RigisterInstance(go, dlgname);
        if (dlg)
        {
            DlgData dlgdata = new DlgData();
            dlgdata.dlg = dlg;
            dlgdata.dlggo = go;
            dlgdata.mCloseTime = Time.realtimeSinceStartup;
            dlgdata.isvisible = true;
            mLoaderContainer.Add(dlgname,dlgdata);
        }
        return dlg;
    }

    

}
        
