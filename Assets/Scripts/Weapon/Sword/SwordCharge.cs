using System.Collections.Generic;
using UnityEngine;

public class SwordCharge: StateBase<Sword.State, Sword>
{
    float attackDelay = 0.2f;
    float attackStartTime;

    float endDelay = 1.2f;
    float noChargedEndDelay = 3f;
    float enterTime;

    float groundParticleOffDelay = 1f;
    bool attacked;
    public SwordCharge(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("SlashCharge");
        owner.PlayChargeParticle(true);
    }

    public override void Exit()
    {
        owner.PlayChargeParticle(false);
        owner.PlayGroundCrackParticle(false, groundParticleOffDelay);
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
            if(false == owner.player.AttackBtn1Input)
            {
                float chargeRatio = (Time.time - enterTime) / noChargedEndDelay;
                attacked = true;
                attackStartTime = Time.time;
                owner.player.PlayAnim("Slash");
                owner.PlayChargeParticle(false);
                owner.BoxAttack((int) (owner.Damage * 2 * chargeRatio), 
                    owner.player.dir, 
                    1f, 
                    5f * chargeRatio, 
                    5f * chargeRatio, 
                    attackDelay);
                owner.PlayGroundCrackParticle(true, attackDelay, chargeRatio);
            }
        }
    }
}