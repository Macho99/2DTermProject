using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDoubleJump : PlayerState
{
    public PlayerDoubleJump(FieldPlayer player) : base(player)
    {
    }


    public override void Enter()
    {
        player.PlayAnim("OnAir");
        player.DoubleJump();
        player.PlayJumpParticle();
        player.doubleJumped = true;
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        player.HorizonMove(TimeExtension.UnscaledDeltaTime, player.AirControlMultiple);
        if (true == player.isGround)
        {
            player.ChangeState(PlayerStateType.Land);
        }
    }

    public override void Exit()
    {

    }

}
