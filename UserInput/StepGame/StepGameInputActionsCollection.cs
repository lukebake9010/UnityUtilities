using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities.StepGame
{
    public class StepGameInputActionsCollection
    {

        private static StepGameInputActions _input;

        static StepGameInputActionsCollection()
        {
            _input = new StepGameInputActions();
            _input.Enable();
        }

        //Action Maps
        public static StepGameInputActions.PlayerActions PlayerActions => _input.Player;
        public static StepGameInputActions.MenuActions MenuActions => _input.Menu;

        #region Player Inputs
        public static InputAction LeftClick => PlayerActions.LeftClick;
        public static InputAction RightClick => PlayerActions.RightClick;
        public static InputAction MiddleClick => PlayerActions.MiddleClick;
        public static InputAction Spacebar => PlayerActions.Spacebar;
        public static InputAction Numberkey => PlayerActions.Numberkey;
        #endregion

    }
}
