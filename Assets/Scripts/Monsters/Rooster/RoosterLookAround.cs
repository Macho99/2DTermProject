using System.Collections.Generic;

public class RoosterLookAround : StateBase<Rooster.State, Rooster>
{
    public RoosterLookAround(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
