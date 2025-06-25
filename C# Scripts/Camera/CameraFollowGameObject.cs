using UnityEngine;

namespace Utilities.CameraScripts
{
    /// <summary>
    /// Simple use of <see cref="ICamController"/>, tells <see cref="CameraManager.mainCamera"/> to follow <c>transform</c>
    /// </summary>
    public class CameraFollowGameObject : MonoBehaviour, ICamController
    {
        private void Start()
        {
            (this as ICamController).Register();
        }

        private void OnDestroy()
        {
            (this as ICamController).Unregister();
        }

        public Vector3 CameraPosition()
        {
            return transform.position;
        }
    }
}
