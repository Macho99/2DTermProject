using System.Collections.Generic;
using UnityEngine;

public class DuckTurn : StateBase<Duck.State, Duck>
{
    public DuckTurn (Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
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

        if(null != owner.Target)
        {
            owner.SetGravityScale(0f);
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
            stateMachine.ChangeState(Duck.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

