using System.Collections.Generic;
using UnityEngine;

public class ChickDie : StateBase<Chick.State, Chick>
{
    public ChickDie(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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