using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChiefCook : StateBase<Chief.State, Chief>
{
    const float minCookTime = 5f;
    const float maxCookTime = 10f;
    float transitionTime;
    float enterTime;

    public ChiefCook(Chief owner, StateMachine<Chief.State, Chief> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.Flip(false);
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minCookTime, maxCookTime);
        owner.SetAnimBool("Cook", true);
        owner.SetStateViewActive(true);
        owner.SetStateSprite();
    }

    public override void Exit()
    {
        owner.SetAnimBool("Cook", false);
        owner.SetStateViewActive(false);
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
        float ratio = (Time.time - enterTime)/(transitionTime - enterTime);
        owner.SetCookTimeMaskRatio(ratio);
    }
}