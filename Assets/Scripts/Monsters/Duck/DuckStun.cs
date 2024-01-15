using System.Collections.Generic;
using UnityEngine;

public class DuckStun : StateBase<Duck.State, Duck>
{
    public DuckStun(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Stun");
        owner.PlayStunParticle(true);
    }

    public override void Exit()
    {
        owner.PlayStunParticle(false);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (owner.StunEndTime < Time.time)
        {
            stateMachine.ChangeState(Duck.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

