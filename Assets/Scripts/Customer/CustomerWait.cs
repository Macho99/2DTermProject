using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerWait : StateBase<Customer.State, Customer>
{
    const float minWaitTime = 30f;
    const float maxWaitTime = 40f;
    float transitionTime;
    float enterTime;
    public CustomerWait(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
        owner.onInteract.AddListener(Interact);
    }

    public override void Exit()
    {
        owner.SetWaitMaskRatio(0f);
        owner.onInteract.RemoveListener(Interact);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > transitionTime)
        {
            owner.SetStateViewSprite(Customer.ViewerState.Angry);
            stateMachine.ChangeState(Customer.State.Exit);
        }
    }

    public override void Update()
    {
        float ratio = (Time.time - enterTime) / (transitionTime - enterTime);
        if(ratio > 0.15f)
            owner.SetWaitMaskRatio(ratio);
    }

    private void Interact(Interactor interactor)
    {
        RestauInteractor restauInter = (RestauInteractor)interactor;

        CuisineItem cuisine = restauInter.GiveCuisine();
        if (cuisine == null) return;

        if (cuisine.ID == owner.SelectedMenu.ID)
        {
            owner.IsProperFood = true;
            stateMachine.ChangeState(Customer.State.Eat);
        }
        else
        {
            owner.IsProperFood = false;
            stateMachine.ChangeState(Customer.State.Eat);
        }
    }
}