using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDuck : PlayerState
{
    public PlayerDuck(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Duck");
        player.SetColliderSize(false);
    }
    public override void Exit()
    {

    }

    public override void Jump(InputValue value)
    {
        if (player.CheckTop() == false)
        {
            player.SetColliderSize(true);
            player.ChangeState(PlayerStateType.Jump);
        }
    }

    public override void Update()
    {
        if(player.inputVec.y > -0.5f)
        {
            //Debug.Log(player.CheckTop());
            if (player.CheckTop() == false)
            {
                player.ChangeState(PlayerStateType.Idle);
                player.SetColliderSize(true);
                return;
            }
        }

        if(Mathf.Abs(player.inputVec.x) > 0.01f)
        {
            player.ChangeState(PlayerStateType.Crawl);
            return;
        }

        //X축 입력이 없으면서 X축 속도가 있을 때 감속
        else if (Mathf.Abs(player.GetVelocity().x) > 0.1f)
        {
            player.HorizonBreak(TimeExtension.UnscaledDeltaTime);
        }
    }
}
