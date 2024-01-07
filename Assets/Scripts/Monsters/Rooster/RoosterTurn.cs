using System.Collections.Generic;
using UnityEngine;

public class RoosterTurn : StateBase<Rooster.State, Rooster>
{
    public RoosterTurn(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if(1 == owner.dir)
        {
            owner.AnimPlay("TurnLeft");
        }
        else
        {
            owner.AnimPlay("TurnRight");
        }
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == owner.IsAnimatorStateName("Wait"))
        {
            owner.Flip();
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

