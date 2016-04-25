using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    public class UIInGameMan : UIDlgManager
    {


        private const string mManagerName = "INGameMan";
        private GameObject mGameObject = null;
        public GameObject ThisGameObject
        {
            get
            {
                return mGameObject;
            }
        }
        protected override DlgBase RigisterInstance(GameObject go, string dlgname)
        {
            DlgBase dlg = null;
            switch (dlgname)
            {  
                case DlgLogin.NAME:
                    dlg = go.AddComponent<DlgLogin>();
                    break;
                default:
                    break;
            }
            return dlg;
        }
        public override void Init()
        {
        
            //Canvas = Util.CreateCanvas(mManagerName,"InGameCanvas");
            Canvas = GameObject.Find("Canvas");
        }
            
    }
