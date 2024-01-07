using System.Collections.Generic;
using UnityEngine;

public class RoosterDetect : StateBase<Rooster.State, Rooster>
{
    public RoosterDetect(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
