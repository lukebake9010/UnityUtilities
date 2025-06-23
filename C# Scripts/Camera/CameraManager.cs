using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Singleton;

namespace Utilities.CameraScripts
{
    public class CameraManager : SingletonBase<CameraManager>
    {
        public Camera mainCamera;

        private List<ICamController> camControllers;

        public void RegisterCamController(ICamController camController)
        {
            camControllers.Add(camController);
        }

        public void UnRegisterCamController(ICamController camController)
        {
            camControllers.Remove(camController);
        }


        #region Monobehavior calls

        private void Awake()
        {

        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        #endregion

    }    
}
