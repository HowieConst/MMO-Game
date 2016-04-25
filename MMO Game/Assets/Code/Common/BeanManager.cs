using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;


public class BeanMan : SingletonMono<BeanMan>
    {
        // Fields
        private readonly Dictionary<Type, IBeanType> singletons = new Dictionary<Type, IBeanType>();

        // Methods
        public IBeanType GetBean(Type type)
        {
            IBeanType type2;
            this.singletons.TryGetValue(type, out type2);
            return type2;
        }

        public void InitAllBeans()
        {
            foreach (IBeanType type in this.singletons.Values)
            {
                type.Init();
            }
        }

        public void RegisterBean(IBeanType obj)
        {
            if (obj != null)
            {
                Type key = obj.GetType();
                this.singletons[key] = obj;
            }
        }
    }

    public class GameBeanInit
    {
        // Methods
        public static void InitAllBeans()
        {
            //using (new OProfiler("InitAllBeans register"))
            //{
            //    RegisterBeans();
            //}
            //using (new OProfiler("InitAllBeans call on init"))
            //{
            //    InitBeans();
            //}
            RegisterBeans();
            InitBeans();
        }

        private static void InitBeans()
        {
            BeanMan.Instance.InitAllBeans();
        }

        private static void RegisterBeans()
        {




            BeanMan.Instance.RegisterBean(Bean<UIManager>.CreateInstance());
            BeanMan.Instance.RegisterBean(Bean<UIDlgManager>.CreateInstance());







            //using (new OProfiler("AppMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<AppMan>.CreateInstance());
            //}
            //using (new OProfiler("BundleLoader"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<BundleLoader>.CreateInstance());
            //}
            //using (new OProfiler("GameMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<GameMan>.CreateInstance());
            //}
            //using (new OProfiler("OConsole"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<OConsole>.CreateInstance());
            //}
            //using (new OProfiler("ResourceMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<ResourceMan>.CreateInstance());
            //}
            //using (new OProfiler("SceneLoader"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<SceneLoader>.CreateInstance());
            //}
            //using (new OProfiler("SceneManager"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<SceneManager>.CreateInstance());
            //}
            //using (new OProfiler("SoundLoader"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<SoundLoader>.CreateInstance());
            //}
            //using (new OProfiler("SoundPlayer"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<SoundPlayer>.CreateInstance());
            //}
            ////using (new OProfiler("TimeMan"))
            ////{
            ////    //BeanMan.Instance.RegisterBean(Bean<TimeMan>.CreateInstance());
            ////}
            //using (new OProfiler("TimeScaleMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<TimeScaleMan>.CreateInstance());
            //}
            //using (new OProfiler("LogSender"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<LogSender>.CreateInstance());
            //}
            //using (new OProfiler("SDKMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<SDKMan>.CreateInstance());
            //}
            //using (new OProfiler("ResourceReleaseMan"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<ResourceReleaseMan>.CreateInstance());
            //}
            //using (new OProfiler("ClientConfig"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<ClientConfig>.CreateInstance());
            //}
            //using (new OProfiler("Locale"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<Locale>.CreateInstance());
            //}
            //using (new OProfiler("CameraShakeHandler"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<CameraShakeHandler>.CreateInstance());
            //}
            //using (new OProfiler("Config"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<Config>.CreateInstance());
            //}
            //using (new OProfiler("UIManager"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<UIManager>.CreateInstance());
            //}
            //using (new OProfiler("ProcessingInputInterface"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<ProcessingInputInterface>.CreateInstance());
            //}
            //using (new OProfiler("GameSession"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<GameSession>.CreateInstance());
            //}
            //using (new OProfiler("AtlasManager"))
            //{
            //    BeanMan.Instance.RegisterBean(Bean<AtlasManager>.CreateInstance());
            //}
        }
    }
