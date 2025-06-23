using UnityEngine;
using Utilities.CameraScripts;

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
