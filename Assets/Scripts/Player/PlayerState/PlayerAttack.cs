using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : PlayerState
{
    bool readyToIdle;
    public PlayerAttack(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        readyToIdle = false;
        player.SetAnimState(PlayerStateType.Attack);
    }

    public override void Attack(InputValue value)
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
            readyToIdle = player.IsAnimatorStateName("Attack");
        }
        player.HorizonBreak(Time.unscaledDeltaTime);
    }

    public override void Exit()
    {
        player.LastCombatTime = Time.time;
    }
}
