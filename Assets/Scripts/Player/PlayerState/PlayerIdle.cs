using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : PlayerState
{
    bool readyState;
    public PlayerIdle(Player player) : base(player) { }

    public override void Enter()
    {
        float curTime = Time.time;

        if(false == player.isGround)
        {
            player.ChangeState(PlayerStateType.OnAir); 
            return;
        }

        if(curTime < player.LastCombatTime + player.ReadyDuration)
        {
            player.PlayAnim("Ready");
            readyState = true;
        }

        else
        {
            player.PlayAnim("Idle");
            readyState = false;
        }
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
        if (Transition() == true)
        {
            return;
        }

        //X�� �Է��� �����鼭 X�� �ӵ��� ���� �� ����
        if(Mathf.Abs(player.inputVec.x) < 0.01f && Mathf.Abs(player.GetVelocity().x) > 0.1f)
        {
            player.HorizonBreak(Time.unscaledDeltaTime);
        }

        if (readyState)
        {
            float curTime = Time.time;
            if(curTime > player.LastCombatTime + player.ReadyDuration)
            {
                player.PlayAnim("Idle");
                readyState = false;
            }
        }
    }

    private bool Transition()
    {
        if (true == player.BlockInput)
        {
            player.ChangeState(PlayerStateType.Block);
            return true;
        }

        if (player.inputVec.y < -0.9f)
        {
            player.ChangeState(PlayerStateType.Duck);
            return true;
        }

        if (false == player.isGround)
        {
            player.ChangeState(PlayerStateType.OnAir);
            return true;
        }

        if(true == player.InteractInput)
        {
            if(true == player.Interactor.CanInteract())
            {
                player.ChangeState(PlayerStateType.Interact);
            }
        }

        //X�� �Է��� ���� ��
        if (Mathf.Abs(player.inputVec.x) > 0.01f)
        {
            player.ChangeState(PlayerStateType.Walk);
            return true;
        }

        return false;
    }

    public override void Exit()
    {

    }
}
