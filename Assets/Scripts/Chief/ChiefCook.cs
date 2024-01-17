using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChiefCook : StateBase<Chief.State, Chief>
{
    const float minCookTime = 5f;
    const float maxCookTime = 8f;
    float transitionTime;
    float enterTime;
    public ChiefCook(Chief owner, StateMachine<Chief.State, Chief> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.transform.localScale = Vector3.one;
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minCookTime, maxCookTime);
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > transitionTime)
        {
            owner.FinishedEnqueue(owner.CurCuisine);
            owner.CurCuisine = null;
            stateMachine.ChangeState(Chief.State.Idle);
        }
    }

    public override void Update()
    {

    }
}