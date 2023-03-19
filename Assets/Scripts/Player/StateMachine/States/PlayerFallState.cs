using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        _ctx.HealthController.ResetHealth();

        _ctx.CineInput.enabled = false;
        _ctx.PathController.Fall();
        LeanTween.move(_ctx.gameObject, new Vector3(0f, 0.5f, 0f), 0.5f).setOnComplete(() =>
        {
            _ctx.Switch.Fall = false;
        });
    }

    public override void StateUpdate()
    {

    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateCheckChange()
    {
        if (!_ctx.Switch.Fall) StateChange(_factory.Grounded());
    }

    public override void StateExit()
    {
        _ctx.CineInput.enabled = true;
    }
}
