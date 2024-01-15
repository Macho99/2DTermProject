using System.Collections.Generic;
using UnityEngine;

public class DuckDetect : StateBase<Duck.State, Duck>
{
    public DuckDetect(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Duck.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
