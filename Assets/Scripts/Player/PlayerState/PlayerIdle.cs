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

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {
        player.ChangeState(PlayerStateType.Jump);
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

        //X�� �Է��� ���� ��
        if(Mathf.Abs(player.inputVec.x) > 0.01f)
        {
            player.ChangeState(PlayerStateType.Walk);
            return;
        }

        //X�� �Է��� �����鼭 X�� �ӵ��� ���� �� ����
        else if(Mathf.Abs(player.GetVelocity().x) > 0.1f)
        {
            player.HorizonMove(player.GetVelocity().x * -0.5f, Time.unscaledDeltaTime);
        }
    }

    public override void Exit()
    {

    }
}
