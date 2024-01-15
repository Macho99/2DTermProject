using System.Collections.Generic;

public class DuckLookAround : StateBase<Duck.State, Duck>
{
    public DuckLookAround(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Duck.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
