using System.Collections.Generic;
using UnityEngine;

public class BuffaloCharge : StateBase<Buffalo.State, Buffalo>
{
    float endTime;
    bool attacked;
    public BuffaloCharge(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Run");
        endTime = Time.time + owner.ChargeDuration;
        attacked = false;
        owner.PlayChargeParticle(true);
    }

    public override void Exit()
    {
        owner.LastChargeTime = Time.time;
        owner.PlayChargeParticle(false);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if(Time.time > endTime)
        {
            stateMachine.ChangeState(Buffalo.State.Trace);
        }
    }

    public override void Update()
    {
        owner.HorizonMove(owner.dir, owner.RunSpeed * 3f, Time.deltaTime);
        Attack();
    }

    private void Attack()
    {
        if (true == attacked) return;

        Vector2 targetPos = owner.Target.position;
        Vector2 ownerPos = owner.transform.position;
        float sqrMag = (targetPos - ownerPos).sqrMagnitude;

        if (sqrMag < 1f)
        {
            owner.Target.GetComponent<FieldPlayer>().TakeDamage(owner, owner.Damage, Vector2.right * owner.dir * owner.KnockbackForce, 2f);
            attacked = true;
        }
    }
}
