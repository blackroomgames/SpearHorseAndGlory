using UnityEngine;

namespace SpearHorseAndGlory
{
    internal abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }

        private static T _instance;
    }
}

