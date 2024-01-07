using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerJump : PlayerState
{
    public PlayerJump(Player player) : base(player)
    {

    }

    public override void Attack(InputValue value)
    {

    }

    public override void Enter()
    {
        player.PlayAnim("Jump");
    }

    public override void Exit()
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if (true == player.IsAnimatorStateName("Wait"))
        {
            if(false == player.CheckTop())
            {
                player.ChangeState(PlayerStateType.OnAir);
                player.Jump();
                return;
            }
            else
            {
                player.ChangeState(PlayerStateType.Idle);
                return;
            }
        }

        player.HorizonMove(Time.unscaledDeltaTime);
    }
}