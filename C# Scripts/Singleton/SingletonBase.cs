using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Utilities.Singleton
{
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {

        // Refers to the instance
        public static T Singleton { get { return _singleton; } }
        private static T _singleton;

        private void EstablishThisAsSingleton()
        {
            //If there is already an instance
            if (_singleton != null)
            {
                //And its not this instance
                if (_singleton != (T)this)
                {
                    //Destroy the existing
                    DestroyImmediate(_singleton);
                    //And replace it
                    _singleton = (T)this;

                    Debug.LogWarning($"Replaced existing Singleton of type {typeof(T).ToString()}");
                }
            }
            //If it's empty
            else
            {
                //This is the new instance
                _singleton = (T)this;
            }
        }

        // Can be used attatched to an object in the scene, aslong as there's only one.
        private void Awake()
        {
            EstablishThisAsSingleton();

            OnSingletonAwake();
        }

        protected virtual void OnSingletonAwake() { }

        protected virtual void OnDestroy()
        {
            //If we're the singleton, remove us
            if (_singleton == (T)this)
            {
                _singleton = null;
            }
        }

        private static bool warningLogged = false;

        public static bool TryGetSingleton(out T singleton)
        {
            if (_singleton == null)
            {
                singleton = null;

                // Only log the caller information in debug builds (or when needed)
#if UNITY_EDITOR
                if (!warningLogged)
                {
                    LogSingletonCallerInformation(true);
                    warningLogged = true;
                }
#endif
                return false;
            }
            else
            {
                singleton = _singleton;
                return true;
            }
        }

#if UNITY_EDITOR
        // Helper function to log the caller class automatically
        private static void LogSingletonCallerInformation(bool isWarning = false)
        {
            var stackTrace = new StackTrace(true);
            var frame = stackTrace.GetFrame(2); // Get the frame two levels up (caller)
            var method = frame.GetMethod();
            var callerType = method.DeclaringType;
            var file = frame.GetFileName();
            string filePath = "Assets\\" + Path.GetRelativePath(Application.dataPath, file);
            filePath = filePath.Replace("\\", "/");
            var lineNumber = frame.GetFileLineNumber();
            string fullPath = filePath + $":{lineNumber}";


            string information = string.Empty;

            if (callerType != null && !string.IsNullOrEmpty(file) && lineNumber > 0)
            {
                // Only log file/line number if available
                information = $"Singleton of type {typeof(T).Name} accessed by {callerType.FullName} (at <a href=\"{filePath}\" line=\"{lineNumber}\">{fullPath}</a>)";
            }
            else
            {
                // Log fallback message if file/line is not available
                information = $"Singleton of type {typeof(T).Name} accessed by {callerType?.FullName ?? "Unknown"} (file/line unavailable)";
            }

            if (isWarning)
            {
                Debug.LogWarning(information);
            }
            else
            {
                Debug.Log(information);
            }
        }
#endif

    }
}
