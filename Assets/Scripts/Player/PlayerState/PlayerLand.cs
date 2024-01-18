using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLand : PlayerState
{
    public PlayerLand(FieldPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Land");
    }

    public override void Exit()
    {

    }

    public override void Jump(InputValue value)
    {
        player.ChangeState(PlayerStateType.Jump);
    }

    public override void Update()
    {
        if (true == player.IsAnimatorStateName("Wait"))
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }

        player.HorizonMove(TimeExtension.UnscaledDeltaTime);
    }
}