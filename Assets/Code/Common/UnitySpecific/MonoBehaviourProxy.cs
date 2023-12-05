using System;
using UnityEngine;

namespace ChessMath.Shared.UnityCommon.Utils
{
    public class MonoBehaviourProxy : MonoBehaviour
    {
        private static MonoBehaviourProxy instance;
        
        public event Action<float> UpdateTicked;
        public event Action<float> LateUpdateTicked;
        public event Action<float> FixedUpdateTicked;
        
        void Update() =>
            UpdateTicked?.Invoke(Time.deltaTime);

        void LateUpdate() =>
            LateUpdateTicked?.Invoke(Time.deltaTime);

        private void FixedUpdate() =>
            FixedUpdateTicked?.Invoke(Time.fixedDeltaTime);


        public static MonoBehaviourProxy I
        {
            get
            {
                if (instance != null)
                    return instance;
                
                // Else create
                var go = new GameObject(nameof(MonoBehaviourProxy));
                DontDestroyOnLoad(go);
                instance = go.AddComponent<MonoBehaviourProxy>();

                return instance;
            }
        }
    }
}