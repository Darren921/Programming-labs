using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager 
{
    private static Control _Control;
   public static void Init(Player Player1)
    {
        _Control = new Control();
        _Control.Permanent.Enable();
        _Control.Game.Movement.performed += ctx => 
        {
            Player1.SetMoveDirection(ctx.ReadValue<Vector3>());
        };
       

        _Control.Game.Shoot.performed += ctx =>
        {
            Player1.Shoot();
        };
        _Control.Game.Look.performed += ctx =>
        {
            Player1.SetLookRotation(ctx.ReadValue<Vector2>());
        };
        _Control.Game.Reload.performed += ctx =>
        {
            Player1.Reload();
        };

    }


public static void setGameControls()
    {
        _Control.Game.Enable();
        _Control.UI.Disable();

    }
    public static void setUIControls()
    {
         _Control.UI.Disable();
        _Control.Game.Disable();
    }
}
