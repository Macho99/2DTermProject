using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : PlayerState
{
    const float counterAttackStunDuration = 1.5f;

    float enterTime;
    public PlayerBlock(FieldPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        if(Time.time < player.LastBlockTime + Constants.BlockCoolTime)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }

        enterTime = Time.time;
        player.PlayAnim("Block");
        player.IsBlockState = true;
        player.BlockUse();
    }

    public override void Exit()
    {
        player.LastCombatTime = Time.time;
        player.IsBlockState = false;
    }

    public override void Jump(InputValue value)
    {

    }

    public override void TakeDamage(Monster monster, int damage, Vector2 knockback, float stunDuration)
    {
        if (knockback.x * player.dir < 0f)
        {
            if(Time.time < enterTime + Constants.CounterAttackDuration)
            {
                monster.TakeDamage(0, -knockback / 2, counterAttackStunDuration);
            }
            else
            {
                player.PlayerTakeDamage(damage / 2, knockback, 0f, false);
            }
            player.ChangeState(PlayerStateType.Idle);
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
        player.LastBlockTime = Time.time;
    }
}