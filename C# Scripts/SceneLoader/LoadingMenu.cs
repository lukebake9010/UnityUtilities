using System.Collections;
using UnityEngine;
using Utilities.SceneManagement;

/// <summary>
/// Controls a loading menu/page. 
/// <para>
/// Shows the menu, starts a scene load, and if a loading bar is supplied, shows it and attaches the sceneloader to it
/// </para>
/// </summary>
public class LoadingMenu : MonoBehaviour
{
    /// <summary>
    /// The loading bar to attach the <see cref="SceneLoader"/> to.
    /// </summary>
    [SerializeField]
    private SceneLoaderLoadingBar sceneLoaderLoadingBar;

    /// <summary>
    /// The current <see cref="SceneLoader.LoadScene(int)"/> coroutine run by this menu
    /// </summary>
    private Coroutine sceneLoadCoroutine = null;

    /// <summary>
    /// Shows the menu (and loadingbar if supplied), and starts loading a given scene
    /// </summary>
    /// <param name="sceneId">The id of the scene to load</param>
    public void ShowAndLoadScene(int sceneId)
    {
        SceneLoader sceneLoader = gameObject.AddComponent<SceneLoader>();//Make the scene loader (has to be attached to run a coroutine)

        if(sceneLoaderLoadingBar != null)
        {
            sceneLoaderLoadingBar.gameObject.SetActive(true);//Show loading bar
            sceneLoaderLoadingBar.sceneLoader = sceneLoader;//Attach the scene loader
        }

        gameObject.SetActive(true);//Show the loading menu
        sceneLoadCoroutine = sceneLoader.StartLoadScene(sceneId);//Start loading the scene
    }
}
