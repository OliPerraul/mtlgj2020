using UnityEngine;
using System.Collections;

namespace Cirrus
{
    public class Singleton : BaseBehaviour { }


    public class BaseSingleton<T> : Singleton where T : Singleton
    {
        protected static T _instance;

        public void Persist()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _instance = Instance;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }
    }

    public class PersistentSingleton<T> : BaseSingleton<T> where T : Singleton
    {
        public override void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

    }
}