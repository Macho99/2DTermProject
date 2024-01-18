using System.Collections.Generic;
using UnityEngine;

public class BuffaloTrace : StateBase<Buffalo.State, Buffalo>
{
    int layLayerMask = LayerMask.GetMask("Player", "Platform");
    public BuffaloTrace(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        if (true == CheckDirection())
        {
            return;
        }
        owner.AnimPlay("Walk");
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

        Vector2 target = new Vector2(targetPos.x, targetPos.y + 0.4f);
        Vector2 current = new Vector2(ownerPos.x, ownerPos.y + 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(current, target - current, owner.LookRange, layLayerMask);
        Debug.DrawRay(current, (target - current).normalized * owner.LookRange, Color.red);

        if (null != hit.collider && true == hit.collider.gameObject.tag.Equals("Player"))
        {
            owner.LastWatchTime = Time.time;
        }

        if (Time.time > owner.LastWatchTime + owner.WatchDuration)
        {
            owner.Target = null;
            stateMachine.ChangeState(Buffalo.State.Walk);
            owner.UIStateChange(MonsterUIState.Miss);
            return;
        }

        if (sqrMag < owner.AttackDist * owner.AttackDist)
        {
            if(Time.time > owner.LastChargeTime + owner.ChargeCoolTime)
            {
                stateMachine.ChangeState(Buffalo.State.ChargeWarn);
                return;
            }
        }
    }

    public override void Update()
    {
        owner.HorizonMove(owner.dir, owner.RunSpeed, Time.deltaTime);
        CheckDirection();
    }

    private bool CheckDirection()
    {
        if (Mathf.Abs(owner.transform.position.x - owner.Target.transform.position.x) < 0.1f) { return false; }

        //타겟이 왼쪽에 있고
        if (owner.Target.position.x < owner.transform.position.x)
        {
            //몬스터가 오른쪽을 보고있을때
            if (owner.dir == 1)
            {
                stateMachine.ChangeState(Buffalo.State.Turn);
                return true;
            }
        }
        //타겟이 오른쪽에 있고
        else
        {
            //몬스터가 왼쪽을 보고있을때
            if (owner.dir == -1)
            {
                stateMachine.ChangeState(Buffalo.State.Turn);
                return true;
            }
        }
        return false;
    }
}