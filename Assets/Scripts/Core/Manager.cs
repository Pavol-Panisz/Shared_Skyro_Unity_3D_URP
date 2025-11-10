using UnityEngine;
using Game.Core;

namespace Game.Core
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance { get; private set; }
        public ItemsManager Items { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Items = GetComponentInChildren<ItemsManager>();
        }
    }
}

