using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : PlayerState
{
    public PlayerAttack(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Attack");
    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if (true == player.IsAnimatorStateName("Wait"))
        {
            player.NormalAttack();
            player.ChangeState(PlayerStateType.Idle);
            return;
        }
        player.HorizonBreak(Time.unscaledDeltaTime);
    }

    public override void Exit()
    {
        player.LastCombatTime = Time.time;
    }
}
