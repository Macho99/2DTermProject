using System.Collections.Generic;
using UnityEngine;


public class RoosterWalk : StateBase<Rooster.State, Rooster>
{
    float maxWalkTime = 2f;
    float minWalkTime = 1f;
    float curWalkTime;

    public RoosterWalk(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        owner.AnimPlay("Walk");
        curWalkTime = Random.Range(minWalkTime, maxWalkTime);
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
        if(curWalkTime < 0f)
        {
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {
        owner.SetVel(new Vector2(owner.MoveSpeed * owner.dir, 0f));
        curWalkTime -= Time.deltaTime;
    }
}
