using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.SceneManagement
{
    public class SceneUtils
    {
        /// <summary>
        /// Returns the build id (<c>int</c>) of the scene you provide by name
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to get the id of</param>
        /// <returns>The build id of the found scene (-1 if not found)</returns>
        public static int GetSceneBuildIndexByName(string sceneName)
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < sceneCount; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string name = System.IO.Path.GetFileNameWithoutExtension(path);
                if (name == sceneName)
                {
                    return i;
                }
            }

            // Scene not found
            return -1;
        }
    }
}
