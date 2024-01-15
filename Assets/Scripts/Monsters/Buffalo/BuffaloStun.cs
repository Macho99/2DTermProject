using System.Collections.Generic;
using UnityEngine;

public class BuffaloStun : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloStun(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Buffalo.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

