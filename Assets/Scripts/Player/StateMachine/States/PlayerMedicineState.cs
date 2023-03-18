using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMedicineState : PlayerBaseState
{
    private CinemachinePOV _cinePOV;
    public PlayerMedicineState(PlayerStateMachine ctx, PlayerStateFactory factory, string stateName) : base(ctx, factory, stateName) { }




    public override void StateEnter()
    {
        _ctx.CineInput.enabled = false;
        _cinePOV = _ctx.CineCamera.GetCinemachineComponent<CinemachinePOV>();
        _cinePOV.m_VerticalAxis.m_MinValue = -180f;
        _cinePOV.m_VerticalAxis.m_MaxValue = 180f;
        _cinePOV.m_VerticalAxis.m_Wrap = true;

        LeanTween.value(_cinePOV.m_VerticalAxis.Value, 90, 0.5f).setOnUpdate((float val) =>
        {
            _cinePOV.m_VerticalAxis.Value = val;

        }).setOnComplete(() =>
        {
            _ctx.PathController.SwitchPath();
            LeanTween.value(_cinePOV.m_VerticalAxis.Value, 430, 2f).setOnUpdate((float val) =>
            {
                _cinePOV.m_VerticalAxis.Value = val;

            }).setOnComplete(() =>
            {
                _cinePOV.m_VerticalAxis.m_Wrap = false;
                _cinePOV.m_VerticalAxis.m_MinValue = -70f;
                _cinePOV.m_VerticalAxis.m_MaxValue = 70f;
                _ctx.CineInput.enabled = true;
                _ctx.Switch.Medicine = false;
            });
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
        if (!_ctx.Switch.Medicine) StateChange(_factory.Grounded());
    }

    public override void StateExit()
    {

    }


}
