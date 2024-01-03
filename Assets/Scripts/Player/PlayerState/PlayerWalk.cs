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

    }

    public override void Attack(InputValue value)
    {
        //nothing
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        float inputX = player.inputVec.x;
        if (Mathf.Abs(inputX) < 0.01f)
        {
            player.ChangeState(PlayerStateType.Idle); 
            return;
        }

        player.HorizonMove(player.inputVec.x);
    }

    public override void Exit()
    {

    }
}
