using System.Collections.Generic;
using UnityEngine;


public class ChickWalk : StateBase<Chick.State, Chick>
{
    float maxWalkTime = 2f;
    float minWalkTime = 1f;
    float curWalkTime;

    public ChickWalk(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Chick.State.Idle);
        }
    }

    public override void Update()
    {
        owner.HorizonMove(owner.dir, owner.MoveSpeed, Time.deltaTime);
        curWalkTime -= Time.deltaTime;
    }
}
