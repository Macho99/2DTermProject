using System.Collections.Generic;
using UnityEngine;

public class RoosterDie : StateBase<Rooster.State, Rooster>
{
    public RoosterDie(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
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