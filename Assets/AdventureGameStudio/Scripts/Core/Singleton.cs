using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ¥ÀýÄ£Ê½
/// </summary>
namespace AdventureGame
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType(typeof(T)) as T;
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
        }

        public static bool IsInitialized
        {
            get { return instance != null; }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}