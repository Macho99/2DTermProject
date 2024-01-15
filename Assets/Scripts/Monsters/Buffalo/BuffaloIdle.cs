using System.Collections.Generic;
using UnityEngine;

public class BuffaloIdle : StateBase<Buffalo.State, Buffalo>
{
    float maxIdleDuration = 3f;
    float minIdleDuration = 1f;
    float curIdleDuration;
    public BuffaloIdle(Buffalo owner, StateMachine<Buffalo.State, Buffalo> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if (owner.Target != null)
        {
            stateMachine.ChangeState(Buffalo.State.Trace);
            return;
        }
        owner.AnimPlay("Idle");
        curIdleDuration = Random.Range(minIdleDuration, maxIdleDuration);
    }

    public override void Exit()
    {

    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (curIdleDuration < 0f)
        {
            int idx = Random.Range(0, 3);
            Buffalo.State type;
            switch (idx)
            {
                case 0:
                    type = Buffalo.State.LookAround;
                    break;
                case 1:
                    type = Buffalo.State.Turn;
                    break;
                case 2:
                    type = Buffalo.State.Walk;
                    break;
                default:
                    type = Buffalo.State.LookAround;
                    break;
            }

            stateMachine.ChangeState(type);
        }
    }

    public override void Update()
    {
        curIdleDuration -= Time.deltaTime;
    }
}