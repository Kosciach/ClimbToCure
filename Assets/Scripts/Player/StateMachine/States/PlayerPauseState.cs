using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseState : PlayerBaseState
{
    public PlayerPauseState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        _ctx.CanvasController.TogglePause(true);
    }

    public override void StateUpdate()
    {

    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateCheckChange()
    {
        if (!_ctx.Switch.Pause) StateChange(_factory.Grounded());
    }

    public override void StateExit()
    {
        _ctx.CanvasController.TogglePause(false);
    }


}
