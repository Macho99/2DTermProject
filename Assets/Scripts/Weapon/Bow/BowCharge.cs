using System.Collections.Generic;
using UnityEngine;

public class BowCharge : StateBase<Bow.State, Bow>
{
    float attackStartTime;

    float endDelay = 0.2f;
    float noChargedEndDelay = 1f;
    float enterTime;

    bool attacked;
    public BowCharge(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("Shot");
    }

    public override void Exit()
    {
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
            if (false == owner.player.AttackBtn1Input)
            {
                float chargeRatio = (Time.time - enterTime) / noChargedEndDelay;
                attacked = true;
                attackStartTime = Time.time;
                owner.player.PlayAnim("ShotEnd");
                owner.ShotArrow(owner.Damage, Vector2.right * owner.player.dir, 10f * chargeRatio, 1f);
            }
        }
    }
}