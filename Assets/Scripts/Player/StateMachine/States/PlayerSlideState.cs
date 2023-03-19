using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    private float _timer;
    public PlayerSlideState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        _ctx.CineInput.enabled = false;

        _ctx.MovementController.GetSlideVector();

        _timer = _ctx.SlideTime;

        LeanTween.scaleY(_ctx.gameObject, _ctx.SlideSize, 0.1f);
    }

    public override void StateUpdate()
    {
        _timer--;
        _timer = Mathf.Clamp(_timer, 0, _ctx.SlideTime);
        _ctx.HealthController.Poisoning();
    }

    public override void StateFixedUpdate()
    {
        _ctx.MovementController.Slide();
    }

    public override void StateCheckChange()
    {
        if (_ctx.Switch.InAir) StateChange(_factory.InAir());
        else if (_timer <= 0) StateChange(_factory.Grounded());
        else if (_ctx.Switch.Medicine) StateChange(_factory.Medicine());
        else if (_ctx.Switch.Fall) StateChange(_factory.Fall());
        else if (_ctx.Switch.MainMenu) StateChange(_factory.MainMenu());
    }

    public override void StateExit()
    {
        _ctx.Switch.Slide = false;
        _ctx.MovementController.RestoreSpeed();
        _ctx.CineInput.enabled = true;
        LeanTween.scaleY(_ctx.gameObject, 1f, 0.1f);
    }


}
