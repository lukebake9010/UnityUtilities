using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Utilities.SceneManagement
{
    /// <summary>
    /// Handles an asynchronous scene load.
    /// <para>
    /// Has various methods to insert functionality at different stages of the loading process
    /// </para>
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Instantly load scene when ready within <see cref="LoadScene(int)"/>
        /// </summary>
        [SerializeField]
        protected bool autoLoadScene = false; 

        /// <summary>
        /// Progress of the current <see cref="LoadScene"/> Coroutine
        /// </summary>
        public float LoadingProgress { get; private set; }

        /// <summary>
        /// Kicks off <see cref="LoadScene"/> coroutine
        /// </summary>
        /// <param name="sceneIndex">
        /// Index of the scene to load
        /// </param>
        /// <returns>
        /// The started <see cref="LoadScene(int)"/> coroutine
        /// </returns>
        public Coroutine StartLoadScene(int sceneIndex)
        {
            return StartCoroutine(LoadScene(sceneIndex)); //Start Coroutine
        }

        /// <summary>
        /// Coroutine that handles asynchronous scene loading
        /// </summary>
        /// <param name="sceneIndex">
        /// Index of the scene to load
        /// </param>
        private IEnumerator LoadScene(int sceneIndex)
        {
            //Create Empty AsyncOperation
            AsyncOperation loadAsync = null;

            //Attempt to create an Async Operation to Load Scene
            try
            {
                loadAsync = SceneManager.LoadSceneAsync(sceneIndex);
                loadAsync.allowSceneActivation = autoLoadScene;
            }
            //If can't create Async, throw error message and break;
            catch (System.Exception e)
            {
                Debug.LogException(e);
                yield break;
            }

            //If loadAsync wasn't created, break
            if (loadAsync == null) yield break;

            StartCoroutine(OnStartLoadLevelCoroutine());
            while (!breakStartLoadLevel)
            {
                yield return null;
            }

            //While scene isn't loaded
            while (!loadAsync.isDone && LoadingProgress < 0.9f)
            {
                //Update DisplayProgress
                LoadingProgress = loadAsync.progress;

                //Synchronous processes to run while Actively Loading Scene
                LoadingScene();

                //Yield Process
                yield return null;
            }

            LoadingProgress = 1;

            StartCoroutine(OnFinishLoadLevelCoroutine());
            while (!breakFinishLoadLevel)
            {
                yield return null;
            }

            //Enter Logic Here for Booting Scene if necessary (E.g. Key Press, Delay)
            loadAsync.allowSceneActivation = true;

            //End Coroutine
            yield return null;
        }

        //Synchronous function to run while actively Loading New Scene
        /// <summary>
        /// Virtual method that is called each frame while loading isn't complete within <see cref="LoadScene(int)"/>
        /// <para>
        /// You can use this like an update loop for things like loading bar visuals
        /// </para>
        /// </summary>
        public virtual void LoadingScene()
        {

        }

        protected bool breakStartLoadLevel = false;
        /// <summary>
        /// Template Coroutine to run when the scene is starting to be loaded.
        /// <para>
        /// <see cref="LoadScene(int)"/> will continue when you set <see cref="breakStartLoadLevel"/> to <c>true</c> and end this scope.
        /// It is good practice to set <see cref="breakStartLoadLevel"/> within <see cref="OnStartLoadLevel"/>
        /// </para>
        /// </summary>
        public virtual IEnumerator OnStartLoadLevelCoroutine()
        {
            OnStartLoadLevel();

            breakStartLoadLevel = true;

            yield return null;
        }

        /// <summary>
        /// This method is called when the scene is starting to be loaded
        /// <see cref="LoadScene(int)"/> will continue when you set <see cref="breakStartLoadLevel"/> to true and end this scope
        /// </summary>
        public virtual void OnStartLoadLevel()
        {

        }

        /// <summary>
        /// When the scene is loaded and ready to be opened, <see cref="LoadScene(int)"/> will only continue when this <c>true</c>
        /// </summary>
        protected bool breakFinishLoadLevel = false;

        /// <summary>
        /// Template Coroutine to run when the scene is loaded and ready to be opened.
        /// <see cref="LoadScene(int)"/> will continue when you set <see cref="breakFinishLoadLevel"/> to true and end this scope.
        /// It is good practice to set <see cref="breakFinishLoadLevel"/> within <see cref="OnFinishLoadLevel"/>
        /// </summary>
        public virtual IEnumerator OnFinishLoadLevelCoroutine()
        {
            OnFinishLoadLevel();

            breakFinishLoadLevel = true;

            yield return null;
        }

        /// <summary>
        /// This method is called when the scene is loaded and ready to be opened.
        /// <see cref="LoadScene(int)"/> will continue when you set <see cref="breakFinishLoadLevel"/> to true and end this scope
        /// </summary>
        public virtual void OnFinishLoadLevel()
        {
        }
    }
}