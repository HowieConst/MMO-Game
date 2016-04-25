using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class TestUI : MonoBehaviour {

    void Awake()
    {
        GameBeanInit.InitAllBeans();
    }
	void Start () 
    {  

        UIManager.Instance.ChangerManager(UIManagerType.InGame);
        UIDlgManager.Instance.init();
        DlgBase dlgbase = UIDlgManager.Instance.GetDialogByName(DlgLogin.NAME);
        dlgbase.ShowDlg(true);
   
	}
	void Update () 
    {
	
	}
}
