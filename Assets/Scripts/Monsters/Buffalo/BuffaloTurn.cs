using System.Collections.Generic;
using UnityEngine;

public class BuffaloTurn : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloTurn(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if (1 == owner.dir)
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
            stateMachine.ChangeState(Buffalo.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

