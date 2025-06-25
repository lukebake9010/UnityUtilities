using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.CameraScripts
{
    /// <summary>
    /// Interface used to provide positional data to <see cref="CameraManager"/>
    /// </summary>
    public interface ICamController
    {
        /// <summary>
        /// Insert the logic for the mainCameras positional data here
        /// </summary>
        /// <returns>The positional data provided to <see cref="CameraManager"/></returns>
        public abstract Vector3 CameraPosition();

        /// <summary>
        /// Registers this to <see cref="CameraManager"/>
        /// </summary>
        public void Register()
        {
            if (CameraManager.TryGetSingleton(out CameraManager camManager))
            {
                camManager.RegisterCamController(this);
            }
        }

        /// <summary>
        /// UnRegisters this from <see cref="CameraManager"/>
        /// </summary>
        public void Unregister()
        {
            if (CameraManager.TryGetSingleton(out CameraManager camManager))
            {
                camManager.UnRegisterCamController(this);
            }
        }
    }
}
