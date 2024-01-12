using System.Collections.Generic;
using UnityEngine;

public class BidentCharge : StateBase<Bident.State, Bident>
{
    const int targetNum = 5;
    float attackDelay = 0f;
    float attackStartTime;

    float endDelay = 1.5f;
    float noChargedEndDelay = 3f;
    float enterTime;

    bool attacked;
    public BidentCharge(Bident owner, StateMachine<Bident.State, Bident> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.Player.PlayAnim("JabCharge");
        owner.PlayChargeParticle(true);
    }

    public override void Exit()
    {
        owner.PlayChargeParticle(false);
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
                stateMachine.ChangeState(Bident.State.Idle);
            }
        }
        else
        {
            if (Time.time > enterTime + noChargedEndDelay)
            {
                stateMachine.ChangeState(Bident.State.Idle);
            }
        }
    }

    public override void Update()
    {
        if (false == attacked)
        {
            if (false == owner.Player.AttackBtn2Input)
            {
                owner.PlayChargeParticle(false);
                float chargeRatio = (Time.time - enterTime) / noChargedEndDelay;
                attacked = true;
                attackStartTime = Time.time;
                owner.Player.PlayAnim("Jab");
                //owner.PlayChargeParticle(false);
                owner.BoxAttackAll((int)(owner.Damage * 2 * chargeRatio), 
                    owner.Player.dir, 
                    5f * chargeRatio, 
                    12f * chargeRatio, 
                    3f * chargeRatio,
                    attackDelay, targetNum);
                owner.PlayExplosionParticle(attackDelay, chargeRatio);

                Vector2 origin = owner.transform.position;
                origin.y += 0.6f;
                RaycastHit2D hit = Physics2D.BoxCast(origin, Vector2.one, 0f, 
                    Vector2.right * owner.Player.dir, 5f * chargeRatio, LayerMask.GetMask("Platform"));
                if(hit.collider == null)
                {
                    owner.PlayerTranslate(Vector2.right * owner.Player.dir * 5f * chargeRatio);
                }
                else
                {
                    owner.PlayerTranslate(Vector2.right * owner.Player.dir * hit.distance * chargeRatio);
                }
            }
        }
    }
}