using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChiefIdle : StateBase<Chief.State, Chief>
{
    public ChiefIdle(Chief owner, StateMachine<Chief.State, Chief> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.transform.localScale = new Vector3(-1, 1, 1);
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if(owner.OrderCount > 0)
        {
            owner.CurCuisine = owner.OrderDequeue();
            stateMachine.ChangeState(Chief.State.Cook);
        }
    }

    public override void Update()
    {

    }
}