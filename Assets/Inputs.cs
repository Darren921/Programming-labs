using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Inputs 
{
    private static Control _Control;
    public static void Init(Player Player1)
    {
        _Control = new Control();
        _Control.Game.Movement.performed += ctx => 
        {

            Player1.SetMoveDirection(ctx.ReadValue<Vector3>());
        };
        _Control.Game.Crouched.canceled += _ =>
        {
            Debug.Log("Crouched");
        };

    }

    public static void GameMode()
    {
        _Control.Game.Enable();
        _Control.UI.Disable();
    }
    public static void UIMode()
    {
        _Control.Game.Disable();
        _Control.UI.Enable();
    }


}
