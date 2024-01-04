using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player) { }

    public override void Enter()
    {

    }

    public override void Slash(InputValue value)
    {
        player.ChangeState(PlayerStateType.Slash);
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
        if(true == player.blockInput)
        {
            player.ChangeState(PlayerStateType.Block);
            return;
        }

        if(player.inputVec.y < -0.9f)
        {
            player.ChangeState(PlayerStateType.Duck);
            return;
        }

        if(false == player.isGround)
        {
            player.ChangeState(PlayerStateType.OnAir);
        }

        //X축 입력이 들어올 때
        if(Mathf.Abs(player.inputVec.x) > 0.01f)
        {
            player.ChangeState(PlayerStateType.Walk);
            return;
        }

        //X축 입력이 없으면서 X축 속도가 있을 때 감속
        else if(Mathf.Abs(player.GetVelocity().x) > 0.1f)
        {
            player.HorizonBreak(Time.unscaledDeltaTime);
        }
    }

    public override void Exit()
    {

    }
}
