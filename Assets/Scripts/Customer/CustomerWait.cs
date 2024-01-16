using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerWait : StateBase<Customer.State, Customer>
{
    const float minWaitTime = 20f;
    const float maxWaitTime = 30f;
    float transitionTime;
    float enterTime;
    public CustomerWait(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
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
            stateMachine.ChangeState(Customer.State.Exit);
        }
    }

    public override void Update()
    {
        float ratio = (Time.time - enterTime) / (transitionTime - enterTime);
        if(ratio > 0.15f)
            owner.SetWaitMaskRatio(ratio);
    }
}