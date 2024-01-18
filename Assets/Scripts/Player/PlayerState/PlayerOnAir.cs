using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnAir : PlayerState
{
    bool readyToLand;
    public PlayerOnAir(FieldPlayer player) : base(player)
    {

    }
    public override void Enter()
    {
        player.PlayAnim("OnAir");
        readyToLand = !(player.IsGround);
    }

    public override void Jump(InputValue value)
    {
        if(false == player.DoubleJumped)
        {
            player.ChangeState(PlayerStateType.DoubleJump);
        }
    }

    public override void Update()
    {
        player.HorizonMove(TimeExtension.UnscaledDeltaTime, player.AirControlMultiple);

        if (readyToLand)
        {
            if (true == player.IsGround)
            {
                player.ChangeState(PlayerStateType.Land);
            }
        }
        else
        {
            if(false == player.IsGround)
            {
                readyToLand = true;
            }
        }

    }

    public override void Exit()
    {

    }
}
