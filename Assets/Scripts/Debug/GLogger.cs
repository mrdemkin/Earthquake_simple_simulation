using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCGames.GDebug
{
    public class GLogger : MonoBehaviour
    {
        public static GLogger Instance;

        void Start()
        {
            if (Instance != null) Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }

        public void Log(string message)
        {
#if DEBUG_LOG
            UnityEngine.Debug.Log($"Gifter Log: {message};");
#endif
        }

        public void LogError(string message)
        {
#if DEBUG_LOG
            UnityEngine.Debug.LogError($"Gifter Error Log: {message};");
#endif
        }
    }
}