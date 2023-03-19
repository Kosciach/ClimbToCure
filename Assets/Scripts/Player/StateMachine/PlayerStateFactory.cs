using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine _ctx;

    public PlayerStateFactory(PlayerStateMachine ctx)
    {
        _ctx = ctx;
    }



    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_ctx, this, "Grounded");
    }
    public PlayerBaseState InAir()
    {
        return new PlayerInAirState(_ctx, this, "InAir");
    }
    public PlayerBaseState Slide()
    {
        return new PlayerSlideState(_ctx, this, "Slide");
    }
    public PlayerBaseState Medicine()
    {
        return new PlayerMedicineState(_ctx, this, "Medicine");
    }
    public PlayerBaseState MainMenu()
    {
        return new PlayerMainMenuState(_ctx, this, "MainMenu");
    }
    public PlayerBaseState Fall()
    {
        return new PlayerFallState(_ctx, this, "Fall");
    }
}
