using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainMenuState : PlayerBaseState
{
    private CinemachinePOV _cinePOV;
    public PlayerMainMenuState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        _ctx.CineInput.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void StateUpdate()
    {

    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateCheckChange()
    {
        if (!_ctx.Switch.MainMenu) StateChange(_factory.Grounded());
    }

    public override void StateExit()
    {
        _ctx.CineInput.enabled = true;
    }
}
