using System.Collections.Generic;
using UnityEngine;

public class BuffaloDie : StateBase<Buffalo.State, Buffalo>
{
    public BuffaloDie(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.AnimPlay("Die");
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {

    }

    public override void Update()
    {

    }
}