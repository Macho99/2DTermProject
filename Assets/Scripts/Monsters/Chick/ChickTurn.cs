using System.Collections.Generic;
using UnityEngine;

public class ChickTurn : StateBase<Chick.State, Chick>
{
    public ChickTurn(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Chick.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

