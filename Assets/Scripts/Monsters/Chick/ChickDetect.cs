using System.Collections.Generic;
using UnityEngine;

public class ChickDetect : StateBase<Chick.State, Chick>
{
    public ChickDetect(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Chick.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
