using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerChoose : StateBase<Customer.State, Customer>
{
    const float minDelay = 3f;
    const float maxDelay = 10f;
    float transitionTime;
    public CustomerChoose(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        transitionTime = Time.time + Random.Range(minDelay, maxDelay);
        owner.SetStateViewSprite(Customer.ViewerState.Choose);
        owner.SetStateViewActive(true);
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if(Time.time > transitionTime)
        {
            owner.SelectedMenu = RestauSceneFlowController.Instance.AllocateMenu();
            owner.SetStateViewSprite(Customer.ViewerState.Menu);
            stateMachine.ChangeState(Customer.State.Wait);
        }
    }

    public override void Update()
    {

    }
}