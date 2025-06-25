using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Utilities.Singleton
{
    /// <summary>
    /// Singleton generic
    /// </summary>
    /// <typeparam name="T">Singletons Type</typeparam>
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {

        // Refers to the instance

        /// <summary>
        /// Public get for the current instance
        /// </summary>
        public static T Singleton { get { return _singleton; } }

        /// <summary>
        /// Current instance
        /// </summary>
        private static T _singleton;

        /// <summary>
        /// Establishes this instance as the current singleton instance (and destroys the old one)
        /// </summary>
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

        /// <summary>
        /// Calls <see cref="EstablishThisAsSingleton"/> and <see cref="OnSingletonAwake"/>
        /// <para>
        /// Use <see cref="OnSingletonAwake"/> if you want to use awake for other purposes
        /// </para>
        /// </summary>
        private void Awake()
        {
            EstablishThisAsSingleton();

            OnSingletonAwake();
        }

        /// <summary>
        /// To be used instad of <see cref="Awake"/>, to not disrupt establishing singletons
        /// </summary>
        protected virtual void OnSingletonAwake() { }

        /// <summary>
        /// Removes this instance from being the singleton instance
        /// </summary>
        protected virtual void OnDestroy()
        {
            //If we're the singleton, remove us
            if (_singleton == (T)this)
            {
                _singleton = null;
            }
        }

        /// <summary>
        /// Has a warning been logged yet due to a failure getting the current singleton instance?
        /// </summary>
        private static bool warningLogged = false;

        /// <summary>
        /// Attempts to get the current singleton reference, provides extensive failure logs within the Unity Editor
        /// </summary>
        /// <param name="singleton">Provides the singleton instance through an out</param>
        /// <returns>Was a singleton returned through the out? <c>bool</c> <para>See out param <paramref name="singleton"/> to receive the singleton instance</para></returns>
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
        /// <summary>
        /// Logs important information about which script called <see cref="TryGetSingleton(out T)"/> and where
        /// </summary>
        /// <param name="isWarning"></param>
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
