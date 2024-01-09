using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : PlayerState
{

    public PlayerWalk(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Walk");
    }

    public override void Attack(InputValue value)
    {
        player.ChangeState(PlayerStateType.Attack);
    }

    public override void Jump(InputValue value)
    {
        if (player.CheckTop() == false)
        {
            player.ChangeState(PlayerStateType.Jump);
        }
    }

    public override void Update()
    {
        if (true == player.BlockInput)
        {
            player.ChangeState(PlayerStateType.Block);
            return;
        }

        if (false == player.isGround)
        {
            player.ChangeState(PlayerStateType.OnAir);
        }

        float inputX = player.inputVec.x;
        if (Mathf.Abs(inputX) < 0.01f)
        {
            player.ChangeState(PlayerStateType.Idle); 
            return;
        }

        player.HorizonMove(TimeExtension.UnscaledDeltaTime);
    }

    public override void Exit()
    {

    }
}
