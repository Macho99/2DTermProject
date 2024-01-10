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
        if(player.CurWeapon == null)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }
        player.IsAttackState = true;
        player.onAttackState?.Invoke();
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        player.IsAttackState = false;
        player.LastCombatTime = Time.time;
    }
}
