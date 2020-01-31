using UnityEngine;
using System.Collections;
using System.Linq;

namespace Cirrus.Resources
{
    public abstract class Resources : ScriptableObject { }

    public abstract class BaseResourceManager<T> : Resources where T : Resources
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = UnityEngine.Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                }

                return _instance;
            }
        }
    }

    public class ResourceManager : BaseBehaviour
    {
        [SerializeField]
        private Resources[] _resources;

        public override void OnValidate()
        {
            if (_resources == null || _resources.Length == 0)
                _resources = Editor.AssetDatabase.FindObjectsOfType<Resources>();
        }
    }
}