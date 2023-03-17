using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerCombatState : PlayerBaseState
{
    public PlayerCombatState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public override void StateUpdate()
    {
        _ctx.CameraController.RotatePlayer();
    }
    public override void StateFixedUpdate()
    {
        _ctx.MovementController.Movement();
    }
    public override void StateCheckChange()
    {
    }
    public override void StateExit()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
