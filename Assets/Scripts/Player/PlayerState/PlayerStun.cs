using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStun : PlayerState
{
    public PlayerStun(FieldPlayer player) : base(player)
    {

    }

    public override void Enter()
    {
        player.PlayAnim("Stun");
        player.PlayStunParticle(true);
        player.IsStunState = true;
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if(Time.time > player.StunEndTime)
        {
            player.IsStunState = false;
            player.ChangeState(PlayerStateType.Idle);
        }
    }

    public override void Exit()
    {
        player.PlayStunParticle(false);
    }

}
