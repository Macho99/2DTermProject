using System.Collections.Generic;

public class BuffaloLookAround : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloLookAround(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("LookAround");
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
