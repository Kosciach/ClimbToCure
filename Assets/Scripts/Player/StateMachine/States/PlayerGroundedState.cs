using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _ctx.JumpCount = 0;
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
        if (_ctx.Switch.InAir) StateChange(_factory.InAir());
        else if (_ctx.Switch.Slide) StateChange(_factory.Slide());
        else if (_ctx.Switch.Medicine) StateChange(_factory.Medicine());
    }
    public override void StateExit()
    {

    }


}
