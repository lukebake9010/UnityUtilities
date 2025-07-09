using UnityEngine;

/// <summary>
/// A generic menu manager for any menu with multiple windows.
/// Windows should be set up as child objects of the gameObject this script is attatched to.
/// </summary>
public class WindowedMenuManager : MonoBehaviour
{
    /// <summary>
    /// A function for disabling all child objects and enabling a specific one, used for switching windows in the menu by index.
    /// Can be assigned to buttons with a variable declaration.
    /// </summary>
    /// <param name="targetWrapperIndex">The index of the window to switch to, in the order of child objects to this manager. Defaults to 0, which should be used for the "main" menu.</param>
    public void SwitchWindow(int targetWrapperIndex = 0)
    {
        //Disables all child object
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        //Enable one specific child at index.
        transform.GetChild(targetWrapperIndex).gameObject.SetActive(true);
    }

    /// <summary>
    /// Function for Logic for a simple "Quit" button.
    /// <para>
    /// Simply exits the application.
    /// </para>
    /// </summary>
    public void Quit()
    {
        //DEBUG
        Debug.Log("Quit button pressed.");
        //Quits the application
        Application.Quit();
    }

}
