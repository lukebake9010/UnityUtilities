using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Singleton;

namespace Utilities.CameraScripts
{
    /// <summary>
    /// Controls the main camera of the scene, receives positional data from <see cref="ICamController">ICamControllers</see> and moves the camera accordingly
    /// </summary>
    public class CameraManager : SingletonBase<CameraManager>
    {
        /// <summary>
        /// The scenes main camera
        /// </summary>
        public Camera mainCamera;

        /// <summary>
        /// Do not alter X position of <see cref="mainCamera"/>
        /// </summary>
        [Header("Lock Position")]
        public bool lockXPosition = false;
        /// <summary>
        /// Do not alter Y position of <see cref="mainCamera"/>
        /// </summary>
        public bool lockYPosition = false;
        /// <summary>
        /// Do not alter Z position of <see cref="mainCamera"/>
        /// </summary>
        public bool lockZPosition = false;

        /// <summary>
        /// Do not alter X rotation of <see cref="mainCamera"/>
        /// </summary>
        [Header("Lock Rotation")]
        public bool lockXRotation = false;
        /// <summary>
        /// Do not alter Y rotation of <see cref="mainCamera"/>
        /// </summary>
        public bool lockYRotation = false;
        /// <summary>
        /// Do not alter Z rotation of <see cref="mainCamera"/>
        /// </summary>
        public bool lockZRotation = false;


        /// <summary>
        /// Subscribed <see cref="ICamController">ICamControllers</see>
        /// </summary>
        private List<ICamController> camControllers = new List<ICamController>();

        /// <summary>
        /// Registers the passed <see cref="ICamController"/> (added to the end of <see cref="camControllers"/>)
        /// </summary>
        public void RegisterCamController(ICamController camController)
        {
            camControllers.Add(camController);
        }

        /// <summary>
        /// UnRegisters the passed <see cref="ICamController"/> (removes from <see cref="camControllers"/>)
        /// </summary>
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
                RepositionMainCamera();
            }
            else
            {
                Debug.LogError("No Camera Supplied to CameraManager!");
            }
        }

        /// <summary>
        /// Repositions <see cref="mainCamera"/> based on the camera position received from the last <see cref="ICamController"/> (as though it was a stack)
        /// </summary>
        private void RepositionMainCamera()
        {
            if (camControllers.Count > 0)
            {
                if (camControllers.Last() != null)
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

        #endregion

    }    
}
