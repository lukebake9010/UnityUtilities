using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Singleton;

namespace Utilities.CameraScripts
{
    public class CameraManager : SingletonBase<CameraManager>
    {
        public Camera mainCamera;

        [Header("Lock Position")]
        public bool lockXPosition = false;
        public bool lockYPosition = false;
        public bool lockZPosition = false;

        [Header("Lock Rotation")]
        public bool lockXRotation = false;
        public bool lockYRotation = false;
        public bool lockZRotation = false;

        private List<ICamController> camControllers = new List<ICamController>();

        public void RegisterCamController(ICamController camController)
        {
            camControllers.Add(camController);
        }

        public void UnRegisterCamController(ICamController camController)
        {
            camControllers.Remove(camController);
        }


        #region Monobehavior calls

        protected override void OnSingletonAwake() { }

        private void Start() { }

        private void Update()
        {
            if(mainCamera != null)
            {
                if(camControllers.Count > 0)
                {
                    if(camControllers.Last() != null)
                    {
                        ICamController camController = camControllers.Last();
                        Vector3 camControllerPosition = camController.CameraPosition();


                        Transform mainCameraTransform = mainCamera.transform;

                        float newXPosition = (lockXPosition) ? mainCameraTransform.position.x : camControllerPosition.x;
                        float newYPosition = (lockYPosition) ? mainCameraTransform.position.y : camControllerPosition.y;
                        float newZPosition = (lockZPosition) ? mainCameraTransform.position.z : camControllerPosition.z;

                        Vector3 newCameraPosition = new Vector3(newXPosition, newYPosition, newZPosition);

                        mainCameraTransform.position = newCameraPosition;
                    }
                }
            }
            else
            {
                Debug.LogError("No Camera Supplied to CameraManager!");
            }
        }

        #endregion

    }    
}
