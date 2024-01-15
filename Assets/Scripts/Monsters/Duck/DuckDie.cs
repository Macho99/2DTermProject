using System.Collections.Generic;
using UnityEngine;

public class DuckDie : StateBase<Duck.State, Duck>
{
    public DuckDie(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Die");
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {

    }

    public override void Update()
    {

    }
}