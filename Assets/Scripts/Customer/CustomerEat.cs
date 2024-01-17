using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        owner.SetAnimBool("Interact", true);
        if(true == owner.IsProperFood)
        {
            owner.SetStateViewSprite(Customer.ViewerState.Happy);
        }
        else
        {
            owner.SetStateViewSprite(Customer.ViewerState.Angry);
        }
    }

    public override void Exit()
    {
        owner.SetAnimBool("Interact", false);
        if (true == owner.IsProperFood)
        {
            GameManager.Inven.AddMoney(owner.SelectedMenu.price);
        }
        else
        {
            GameManager.Inven.AddMoney(owner.SelectedMenu.price / 2);
        }
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