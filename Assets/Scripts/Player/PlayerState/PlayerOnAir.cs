using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnAir : PlayerState
{
    bool readyToLand;
    public PlayerOnAir(Player player) : base(player)
    {

    }
    public override void Enter()
    {
        player.SetAnimState(PlayerStateType.OnAir);
        readyToLand = !(player.isGround);
    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {
        player.ChangeState(PlayerStateType.DoubleJump);
    }

    public override void Update()
    {
        player.HorizonMove(player.AirControlMultiple, Time.unscaledDeltaTime);

        if (readyToLand)
        {
            if (true == player.isGround)
            {
                player.ChangeState(PlayerStateType.Land);
            }
        }
        else
        {
            if(false == player.isGround)
            {
                readyToLand = true;
            }
        }

    }

    public override void Exit()
    {

    }
}
