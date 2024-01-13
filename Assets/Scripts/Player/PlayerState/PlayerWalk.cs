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
        player.onAttackBtn1Pressed.AddListener(Attack);
        player.onAttackBtn2Pressed.AddListener(Attack);
        player.PlayWalkParticle(true);
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
        
        if(false == player.IsAnimatorStateName("Walk"))
        {
            player.PlayAnim("Walk");
        }

        player.HorizonMove(TimeExtension.UnscaledDeltaTime);
    }

    public override void Exit()
    {
        player.onAttackBtn1Pressed.RemoveListener(Attack);
        player.onAttackBtn2Pressed.RemoveListener(Attack);
        player.PlayWalkParticle(false);
    }
}
