using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerEat : StateBase<Customer.State, Customer>
{
    const float minEatTime = 5f;
    const float maxEatTime = 8f;
    float transitionTime;
    float enterTime;
    public CustomerEat(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minEatTime, maxEatTime);
        owner.SetStateViewSprite(Customer.ViewerState.Happy);
        owner.SetAnimBool("Interact", true);
    }

    public override void Exit()
    {
        owner.SetAnimBool("Interact", false);
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

    }
}