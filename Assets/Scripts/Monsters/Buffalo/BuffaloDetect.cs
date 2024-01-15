using System.Collections.Generic;
using UnityEngine;

public class BuffaloDetect : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloDetect(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Detect");
        owner.UIStateChange(MonsterUIState.Detect);
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
            stateMachine.ChangeState(Buffalo.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
