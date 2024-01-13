using System.Collections.Generic;
using UnityEngine;

public class SwordCharge: StateBase<Sword.State, Sword>
{
    const int targetNum = 5;
    float attackDelay = 0.2f;
    float attackStartTime;

    float endDelay = 1.2f;
    float noChargedEndDelay = 3f;
    float enterTime;

    bool attacked;
    public SwordCharge(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.Player.PlayAnim("SlashCharge");
        owner.PlayChargeParticle(true);
    }

    public override void Exit()
    {
        owner.PlayChargeParticle(false);
        owner.Player.SetMultiPurposeBar(0f);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == attacked)
        {
            if(Time.time > attackStartTime + endDelay)
            {
                stateMachine.ChangeState(Sword.State.Idle);
            }
        }
        else
        {
            if(Time.time > enterTime + noChargedEndDelay)
            {
                stateMachine.ChangeState(Sword.State.Idle);
            }
        }
    }

    public override void Update()
    {
        if(false == attacked)
        {
            float chargeRatio = (Time.time - enterTime) / noChargedEndDelay;
            if (false == owner.Player.AttackBtn1Input)
            {
                attacked = true;
                attackStartTime = Time.time;
                owner.Player.PlayAnim("Slash");
                owner.PlayChargeParticle(false);
                owner.BoxAttackAll((int) (owner.Damage * 2 * chargeRatio), 
                    owner.Player.dir, 
                    1.5f, 
                    5f * chargeRatio, 
                    5f * chargeRatio, 
                    attackDelay, targetNum);
                owner.PlayGroundCrackParticle(attackDelay, chargeRatio);
            }
            else
            {
                owner.Player.SetMultiPurposeBar(chargeRatio);
            }
        }
    }
}