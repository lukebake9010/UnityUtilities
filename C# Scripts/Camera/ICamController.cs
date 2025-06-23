using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.CameraScripts
{
    public interface ICamController
    {
        public abstract Vector3 CameraPosition();

        protected void Register()
        {
            if (CameraManager.TryGetSingleton(out CameraManager camManager))
            {
                camManager.RegisterCamController(this);
            }
        }

        protected void Unregister()
        {
            if (CameraManager.TryGetSingleton(out CameraManager camManager))
            {
                camManager.UnRegisterCamController(this);
            }
        }
    }
}
