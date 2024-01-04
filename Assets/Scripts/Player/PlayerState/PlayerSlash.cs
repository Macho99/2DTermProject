using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlash : PlayerState
{
    bool readyToIdle;
    public PlayerSlash(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        readyToIdle = false;
    }

    public override void Slash(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if (readyToIdle)
        {
            if (true == player.IsAnimatorStateName("Idle"))
            {
                player.ChangeState(PlayerStateType.Idle);
                return;
            }
        }
        else
        {
            readyToIdle = player.IsAnimatorStateName("Slash");
        }
        player.HorizonBreak(Time.unscaledDeltaTime);
    }

    public override void Exit()
    {

    }
}
