using System.Collections.Generic;
using UnityEngine;

public class SwordCharge: StateBase<Sword.State, Sword>
{
    float attackDuration = 0.2f;
    float attackDelayDuration = 1.2f;
    float attackStartTime;
    //float japPossibleDuration = 0.5f;
    float endDuration = 3f;
    float enterTime;
    bool attacked;
    bool charged;
    public SwordCharge(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        charged = false;
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("Charge");
        owner.PlayChargeParticle(true);
    }

    public override void Exit()
    {
        owner.PlayChargeParticle(false);
        owner.PlayGroundCrackParticle(false);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == attacked)
        {
            if(Time.time > attackStartTime + attackDelayDuration)
            {
                stateMachine.ChangeState(Sword.State.Idle);
                owner.player.ChangeState(PlayerStateType.Idle);
            }
        }

        if (Time.time > enterTime + endDuration)
        {
            if(false == charged)
            {
                stateMachine.ChangeState(Sword.State.Idle);
                owner.player.ChangeState(PlayerStateType.Idle);
            }
        }
    }

    public override void Update()
    {
        if(false == charged)
        {
            if(false == owner.player.AttackBtn1Input)
            {
                charged = true;
                attackStartTime = Time.time;
                owner.player.PlayAnim("Slash");
                owner.PlayChargeParticle(false);
            }
        }
        else if (false == attacked)
        {
            if (Time.time > attackStartTime + attackDuration)
            {
                owner.BoxAttack((int)((float)owner.Damage * 2f), owner.player.dir, 5f, 5f);
                attacked = true;
                owner.PlayGroundCrackParticle(true);
            }
        }
    }
}