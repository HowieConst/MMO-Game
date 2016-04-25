using UnityEngine;
using System.Collections;


   public enum UIManagerType
    { 
        Login,
        InGame,
        Bootup,
    }
    public class UIManager : Bean<UIManager>
    {  

        public void ChangerManager(UIManagerType type)
        {
            switch (type)
            {
                case UIManagerType.Login:
                    break;
                case UIManagerType.InGame:
                    UIInGameMan ingame = new  UIInGameMan();
                    ingame.Init();
                   UIDlgManager.Instance.Currentmanger = ingame;
                    break;
                case UIManagerType.Bootup:
                    break;
                default:
                    break;
            }
        
        }

    }
