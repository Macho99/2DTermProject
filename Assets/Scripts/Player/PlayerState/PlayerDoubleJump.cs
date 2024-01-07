using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDoubleJump : PlayerState
{
    public PlayerDoubleJump(Player player) : base(player)
    {
    }


    public override void Enter()
    {
        player.PlayAnim("OnAir");
        player.DoubleJump();
        player.doubleJumped = true;
    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        player.HorizonMove(Time.unscaledDeltaTime, player.AirControlMultiple);
        if (true == player.isGround)
        {
            player.ChangeState(PlayerStateType.Land);
        }
    }

    public override void Exit()
    {

    }

}
