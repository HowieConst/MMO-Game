using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
    public class DlgLogin : DlgBase
    {
        public const string NAME = "UI_Login";
        private GameObject mStartButton = null;
        protected override void Init()
        {
            base.Init();
            mStartButton = Util.FindChildDeep(gameObject, "StartButton");
            Debug.Log(mStartButton.name);
            Button button = mStartButton.GetComponent<Button>();
            button.onClick.AddListener(OnClickBtn);
        }
        public void OnClickBtn()
        {
            Debug.Log("关闭了---");
            ShowDlg(false);
        }
    }


