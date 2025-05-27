using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsCollection
{

    private static InputActions _input;

    static InputActionsCollection()
    {
        _input = new InputActions();
        _input.Enable();
    }

    //Action Maps
    public static InputActions.PlayerActions PlayerActions => _input.Player;
    public static InputActions.MenuActions MenuActions => _input.Menu;

    #region Player Inputs
    public static InputAction LeftClick => PlayerActions.LeftClick;
    public static InputAction RightClick => PlayerActions.RightClick;
    public static InputAction MiddleClick => PlayerActions.MiddleClick;
    public static InputAction Spacebar => PlayerActions.Spacebar;
    public static InputAction Numberkey => PlayerActions.Numberkey;
    #endregion

}
