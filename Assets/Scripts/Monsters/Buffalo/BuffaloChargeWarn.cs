using System.Collections.Generic;
using UnityEngine;

public class BuffaloChargeWarn : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloChargeWarn(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("ChargeWarn");
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (true == owner.IsAnimatorStateName("Wait")){
            stateMachine.ChangeState(Buffalo.State.Charge);
        }
    }

    public override void Update()
    {

    }
}
