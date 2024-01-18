using System;
using System.Collections.Generic;
using UnityEngine;

public class DuckLanding : StateBase<Duck.State, Duck>
{
    public DuckLanding(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.SetGravityScale(0f);
    }
        
    public override void Exit()
    {
    }

    public override void Setup()
    {
    }

    public override void Transition()
    {
        if(true == owner.IsGround)
        {
            stateMachine.ChangeState(Duck.State.Idle);
            owner.SetGravityScale(1f);
        }
    }

    public override void Update()
    {
        owner.HorizonMove(owner.dir, owner.MoveSpeed, Time.deltaTime);
        owner.SetVel(Vector2.down);
    }
}