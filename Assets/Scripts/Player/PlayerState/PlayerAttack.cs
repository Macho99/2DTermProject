using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : PlayerState
{
    float attackOffset = 0.2f;
    float attackStartTime;
    bool attacked;
    public PlayerAttack(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Attack");
        attackStartTime = Time.time;
        attacked = false;
    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if(false == attacked)
        {
            if(Time.time > attackStartTime + attackOffset) {
                player.NormalAttack();
                attacked = true;
            }
        }

        if (true == player.IsAnimatorStateName("Wait"))
        {
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
