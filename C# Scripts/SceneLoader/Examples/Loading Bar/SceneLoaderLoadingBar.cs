using UnityEngine;
using UnityEngine.UI;

namespace Utilities.SceneManagement
{
    /// <summary>
    /// Example script showing how to implement a loading bar using <see cref="SceneLoader"/>
    /// </summary>
    public class SceneLoaderLoadingBar : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="SceneLoader"/> to display the progress of.
        /// </summary>
        public SceneLoader sceneLoader;

        /// <summary>
        /// The image to fill based on <see cref="SceneLoader.LoadingProgress"/>
        /// </summary>
        [SerializeField]
        private Image loadingBarFill;

        /// <summary>
        /// Fills the loading bar based on <see cref="SceneLoader.LoadingProgress"/> if <see cref="sceneLoader"/> is supplied.
        /// </summary>
        void Update()
        {
            if(sceneLoader == null)
            {
                loadingBarFill.fillAmount = 0;
            }
            else
            {
                loadingBarFill.fillAmount = sceneLoader.LoadingProgress;
            }
        }
    }
}
