using System.Collections.Generic;
using UnityEngine;

public class RoosterTrace : StateBase<Rooster.State, Rooster>
{
    int layLayerMask = LayerMask.GetMask("Player", "Platform");
    public RoosterTrace(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        if(true == CheckDirection())
        {
            return;
        }
        owner.AnimPlay("Run");
    }

    public override void Exit()
    {
        owner.SetVel(Vector2.zero);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        Vector2 targetPos = owner.Target.position;
        Vector2 ownerPos = owner.transform.position;
        float sqrMag = (targetPos - ownerPos).sqrMagnitude;

        //if(sqrMag > owner.TraceRange * owner.TraceRange)
        //{
        //    owner.Target = null;
        //    stateMachine.ChangeState(Rooster.State.Idle);
        //    return;
        //}

        if (sqrMag < owner.AttackDist * owner.AttackDist)
        {
            stateMachine.ChangeState(Rooster.State.Attack);
            return;
        }

        Vector2 target = new Vector2(targetPos.x, targetPos.y + 0.4f);
        Vector2 current = new Vector2(ownerPos.x, ownerPos.y + 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(current, target - current, owner.TraceRange, layLayerMask);
        Debug.DrawRay(current, (target - current).normalized * owner.TraceRange, Color.red);

        if(null == hit.collider || false == hit.collider.gameObject.tag.Equals("Player"))
        {
            owner.Target = null;
            stateMachine.ChangeState(Rooster.State.Walk);
            owner.UIStateChange(MonsterUIState.Miss);
            return;
        }
    }

    public override void Update()
    {
        owner.SetVel(Vector2.right * owner.dir * owner.RunSpeed);
        CheckDirection();
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