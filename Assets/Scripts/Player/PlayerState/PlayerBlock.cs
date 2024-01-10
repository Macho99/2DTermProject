using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : PlayerState
{
    const float counterAttackDuration = 0.5f;
    const float counterAttackStunDuration = 1.5f;
    const float blockCoolTime = 5f;

    float lastBlockTime = -5f;
    float enterTime;
    public PlayerBlock(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        if(Time.time < lastBlockTime + blockCoolTime)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }

        enterTime = Time.time;
        player.PlayAnim("Block");
    }

    public override void Exit()
    {
        player.LastCombatTime = Time.time;
    }

    public override void Jump(InputValue value)
    {

    }

    public override void TakeDamage(Monster monster, int damage, Vector2 knockback, float stunDuration)
    {
        if(knockback.x * player.dir < 0f)
        {
            if(Time.time < enterTime + counterAttackDuration)
            {
                monster.TakeDamage(0, -knockback, counterAttackStunDuration);
            }
            else
            {
                player.PlayerTakeDamage(damage / 2, knockback, 0f, false);
            }
            player.ChangeState(PlayerStateType.Idle);
            lastBlockTime = Time.time;
            return;
        }

        player.PlayerTakeDamage(damage, knockback, stunDuration);
    }

    public override void Update()
    {
        if (false == player.BlockInput)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }
        lastBlockTime = Time.time;
    }
}