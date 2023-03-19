using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpWallState : PlayerBaseState
{
    public PlayerJumpWallState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {

    }

    public override void StateUpdate()
    {
        _ctx.CameraController.RotatePlayer();
        if (!_ctx.IsPause) _ctx.HealthController.Poisoning();
    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateCheckChange()
    {
        if (_ctx.MovementController.GetIsGrounded()) StateChange(_factory.Grounded());
        else if (_ctx.Switch.Fall) StateChange(_factory.Fall());
    }

    public override void StateExit()
    {
        _ctx.Switch.WallJump = false;
        _ctx.JumpCount = 2;
    }


}
