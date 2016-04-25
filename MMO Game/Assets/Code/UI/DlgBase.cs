using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

   public enum LoadStatus
    { 
        LOADSTATYS_NONE,
        LOADSTATUS_LOADING,
        LOADSTATUS_LOADED,
        LOADSTATUS_FAILED,
    }
    public class DlgBase : MonoBehaviour
    {  
        private const int mManualHeigt = 720;
        private const int mManualWidth = 1280;

        private string mDlgName = "";
        public string DlgName
        {
            get
            {
                if (string.IsNullOrEmpty(mDlgName))
                {
                    return mGameObject.name;
                }
                return DlgName;
            }
        }
        private int mDlgIndex = 0;
        public int DlgIndex
        {
            get
            {
                return mDlgIndex;
            }
        }
        private LoadStatus mCurrentLoadStatus = LoadStatus.LOADSTATYS_NONE;
        public LoadStatus CurrentLoadStatus
        {
            get
            {
                return mCurrentLoadStatus;
            }
        }
        public GameObject mGameObject
        {
            get
            {
                return this.gameObject;
            }
        }
        public bool Show
        {
            get
            {
               return  mGameObject.activeSelf;
            }
        }
        private bool IsNeedAdjustIndex;
        public bool NeedAdjustIndex
        {
            set { IsNeedAdjustIndex = value; }
            get { return IsNeedAdjustIndex; }
        }
        protected virtual void Init()
        { 
          
        }
        protected virtual void Awake()
        {

          Action act = new Action(() => { Init(); });
          StartCoroutine(ResourceManager.Instance.LoadAsset(DlgName, mGameObject,act));
        }
        protected virtual void Update()
        { 
          
        }
        protected virtual void LateUpdate()
        { 
          
        }
        public static UIDlgManager GetCurrentDlgManager()
        {
            return UIDlgManager.Instance.Currentmanger;
        }
        public  virtual void ShowDlg(bool isshow)
        {
            if (isshow)
            {
                if (!Show)
                {
             
                    mGameObject.SetActive(true);
                    onOpen();
                }
            }
            else
            {
                if (Show)
                {
                    mGameObject.SetActive(false);
                    OnClose();
                }
            }
        }
        protected virtual void OnClose()
        {
          
        }
        protected virtual void onOpen()
        {
            
        }
        public virtual void ProcessUIEvent()
        { 
          
        }
        public virtual void OnDestroy()
        { 
           
        }
    }
