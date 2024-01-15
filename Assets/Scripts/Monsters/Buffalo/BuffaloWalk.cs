using System.Collections.Generic;
using UnityEngine;


public class BuffaloWalk : StateBase<Buffalo.State, Buffalo>
{
    float maxWalkTime = 2f;
    float minWalkTime = 1f;
    float curWalkTime;

    public BuffaloWalk(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
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
        if (curWalkTime < 0f)
        {
            stateMachine.ChangeState(Buffalo.State.Idle);
        }
    }

    public override void Update()
    {
        owner.HorizonMove(owner.dir, owner.MoveSpeed, Time.deltaTime);
        curWalkTime -= Time.deltaTime;
    }
}
