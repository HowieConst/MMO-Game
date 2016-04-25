using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

    public interface IBeanType
    {
        // Methods
        void Init();
    }

    public abstract class Bean<T> : IBeanType where T : new()
    {
        // Fields
        //[ThreadStatic]
        private static T instance;

        // Methods
        protected Bean()
        {
        }

        public static T CreateInstance()
        {
            if (Bean<T>.instance == null)
            {
                Bean<T>.instance = new T();
            }
            
            return Bean<T>.instance;
        }

        public virtual void Init()
        {
        }
        // Properties
        public static T Instance
        {  
           
            get
            {  
                return Bean<T>.instance;
            }
        }
    }