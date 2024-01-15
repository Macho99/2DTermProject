using System.Collections.Generic;
using UnityEngine;

public class ChickStun : StateBase<Chick.State, Chick>
{
    public ChickStun(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(Chick.State.Idle);
        }
    }

    public override void Update()
    {

    }
}

