using System.Collections.Generic;

public class ChickLookAround : StateBase<Chick.State, Chick>
{
    public ChickLookAround(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Chick.State.Idle);
        }
    }

    public override void Update()
    {

    }
}
