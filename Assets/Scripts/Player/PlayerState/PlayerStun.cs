using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStun : PlayerState
{
    public PlayerStun(Player player) : base(player)
    {

    }

    public override void Enter()
    {
        player.SetAnimState(PlayerStateType.Stun);

    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }

}
