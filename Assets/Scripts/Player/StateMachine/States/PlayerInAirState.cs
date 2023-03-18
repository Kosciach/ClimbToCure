using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{

    public PlayerInAirState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {

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
        if (_ctx.MovementController.GetIsGrounded() && _ctx.Rigidbody.velocity.y < 0) StateChange(_factory.Grounded());
        else if (_ctx.Switch.Medicine) StateChange(_factory.Medicine());
    }
    public override void StateExit()
    {
        _ctx.Switch.InAir = false;
    }


}
