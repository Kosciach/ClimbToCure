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



    public PlayerBaseState Combat()
    {
        return new PlayerCombatState(_ctx, this, "Combat");
    }
}
