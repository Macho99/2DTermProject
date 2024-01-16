using System.Collections.Generic;
using UnityEngine;

public class RoosterAttack : StateBase<Rooster.State, Rooster>
{
    public RoosterAttack(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Idle");
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == owner.IsAnimatorStateName("Attack"))
            return;

        if (true == CheckDist())
            return;
        if (true == CheckDirection())
            return;
    }

    public override void Update()
    {
        if (owner.LastAttackTime + owner.AttackDuration < Time.time)
        {
            Attack();
        }
    }

    private void Attack()
    {
        owner.LastAttackTime = Time.time;
        owner.AnimPlay("Attack");
        owner.Target.GetComponent<FieldPlayer>().TakeDamage(owner, owner.Damage, Vector2.right * owner.dir * owner.KnockbackForce);
    }

    private bool CheckDist()
    {
        Vector2 targetPos = owner.Target.position;
        Vector2 ownerPos = owner.transform.position;
        float sqrMag = (targetPos - ownerPos).sqrMagnitude;

        if (sqrMag > owner.AttackDist * owner.AttackDist)
        {
            stateMachine.ChangeState(Rooster.State.Trace);
            return true;
        }
        return false;
    }

    private bool CheckDirection()
    {
        //타겟이 왼쪽에 있고
        if (owner.Target.position.x < owner.transform.position.x)
        {
            //몬스터가 오른쪽을 보고있을때
            if (owner.dir == 1)
            {
                stateMachine.ChangeState(Rooster.State.Turn);
                return true;
            }
        }
        //타겟이 오른쪽에 있고
        else
        {
            //몬스터가 왼쪽을 보고있을때
            if (owner.dir == -1)
            {
                stateMachine.ChangeState(Rooster.State.Turn);
                return true;
            }
        }
        return false;
    }
}
