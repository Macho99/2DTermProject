using System.Collections.Generic;
using UnityEngine;

public class BowCharge : StateBase<Bow.State, Bow>
{
    float attackStartTime;

    float endDelay = 0.1f;
    float minimumChargeRatio = 0.15f;
    float maxChargeTime = 1f;
    float noChargedEndDelay = 3f;
    float enterTime;

    bool attacked;
    public BowCharge(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.Player.PlayAnim("ShotStart");
    }

    public override void Exit()
    {
        owner.Player.SetMultiPurposeBar(0f);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == attacked)
        {
            if (Time.time > attackStartTime + endDelay)
            {
                stateMachine.ChangeState(Bow.State.Idle);
            }
        }
        else
        {
            if (Time.time > enterTime + noChargedEndDelay)
            {
                stateMachine.ChangeState(Bow.State.Idle);
            }
        }
    }

    public override void Update()
    {
        if (false == attacked)
        {
            float chargeRatio = (Time.time - enterTime) / maxChargeTime;
            if (false == owner.Player.AttackBtn1Input)
            {
                if (chargeRatio < minimumChargeRatio)
                {
                    stateMachine.ChangeState(Bow.State.Idle);
                    return;
                }
                chargeRatio = Mathf.Min(chargeRatio, 1f);
                attacked = true;
                attackStartTime = Time.time;
                owner.Player.PlayAnim("ShotEnd");
                Vector2 direction = Vector2.right * owner.Player.dir + Vector2.up * (chargeRatio * 0.3f);   
                owner.ShotArrow(owner.Damage, direction, owner.Damage * 2f * chargeRatio, 1f);
            }
            else
            {
                owner.Player.SetMultiPurposeBar(chargeRatio);
            }
        }
    }
}