using System.Collections.Generic;
using UnityEngine;

public class RoosterTurn : StateBase<Rooster.State, Rooster>
{
    public RoosterTurn(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if(true == owner.IsRight)
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
            if (owner.IsRight == true)
            {
                owner.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                owner.IsRight = false;
            }
            else
            {
                owner.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                owner.IsRight = true;
            }
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

