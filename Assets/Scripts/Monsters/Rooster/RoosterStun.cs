using System.Collections.Generic;
using UnityEngine;

public class RoosterStun : StateBase<Rooster.State, Rooster>
{
    public RoosterStun(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Stun");
        owner.UIStateChange(MonsterUIState.Stun);
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if(owner.StunEndTime < Time.time)
        {
            stateMachine.ChangeState(Rooster.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

