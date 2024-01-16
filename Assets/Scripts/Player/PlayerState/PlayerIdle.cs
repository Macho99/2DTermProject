using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerIdle : PlayerState
{
    bool readyState;
    public PlayerIdle(FieldPlayer player) : base(player) { }

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

        player.onHit.AddListener(OnHit);
        player.onAttackBtn1Pressed.AddListener(Attack);
        player.onAttackBtn2Pressed.AddListener(Attack);
        player.AddjustFlip();
    }

    public override void Exit()
    {
        player.onHit.RemoveListener(OnHit);
        player.onAttackBtn1Pressed.RemoveListener(Attack);
        player.onAttackBtn2Pressed.RemoveListener(Attack);
    }

    private void OnHit()
    {
        readyState = true;
        player.LastCombatTime = Time.time;
        player.PlayAnim("Ready");
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

        //X축 입력이 없으면서 X축 속도가 있을 때 감속
        if(Mathf.Abs(player.inputVec.x) < 0.01f && Mathf.Abs(player.GetVelocity().x) > 0.1f)
        {
            player.HorizonBreak(TimeExtension.UnscaledDeltaTime);
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

        //X축 입력이 들어올 때
        if (Mathf.Abs(player.inputVec.x) > 0.01f)
        {
            player.ChangeState(PlayerStateType.Walk);
            return true;
        }

        return false;
    }
}
