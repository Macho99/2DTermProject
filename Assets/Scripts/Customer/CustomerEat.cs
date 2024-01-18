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

    SpriteRenderer food;

    public CustomerEat(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        transitionTime = Time.time + Random.Range(minEatTime, maxEatTime);
        owner.SetAnimBool("Interact", true);
        food = new GameObject("Food").AddComponent<SpriteRenderer>();
        food.transform.position = owner.transform.position;
        food.transform.Translate(new Vector3(1.05f, 0.3f, -1f));
        food.transform.rotation = Quaternion.Euler(-45f, 0f, 0f);

        if (true == owner.IsProperFood)
        {
            owner.SetStateViewSprite(Customer.ViewerState.Happy);
        }
        else
        {
            owner.SetStateViewSprite(Customer.ViewerState.Angry);
        }
        food.sprite = owner.ReceivedMenu.Sprite;
    }

    public override void Exit()
    {
        GameObject.Destroy(food.gameObject);
        owner.SetAnimBool("Interact", false);
        if (true == owner.IsProperFood)
        {
            GameManager.Inven.AddMoney(owner.SelectedMenu.price);
        }
        else
        {
            GameManager.Inven.AddMoney(owner.SelectedMenu.price / 2);
        }
        owner.PlayerMoneyParticle();
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