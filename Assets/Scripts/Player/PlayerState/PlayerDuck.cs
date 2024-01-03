using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDuck : PlayerState
{
    public PlayerDuck(Player player) : base(player)
    {
    }

    public override void Enter()
    {

    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {
        player.Down();
        player.ChangeState(PlayerStateType.OnAir);
    }

    public override void Update()
    {
        if(player.inputVec.y > -0.9f)
        {
            player.ChangeState(PlayerStateType.Idle);
        }
    }

    public override void Exit()
    {

    }

}
