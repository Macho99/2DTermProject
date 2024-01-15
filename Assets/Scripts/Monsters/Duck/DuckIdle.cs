using System.Collections.Generic;
using UnityEngine;

public class DuckIdle : StateBase<Duck.State, Duck>
{
    float maxIdleDuration = 3f;
    float minIdleDuration = 1f;
    float curIdleDuration;
    public DuckIdle(Duck owner, StateMachine<Duck.State, Duck> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if (owner.Target != null)
        {
            stateMachine.ChangeState(Duck.State.FlyAway);
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
            Duck.State type;
            switch (idx)
            {
                case 0:
                    type = Duck.State.LookAround;
                    break;
                case 1:
                    type = Duck.State.Turn;
                    break;
                case 2:
                    type = Duck.State.Walk;
                    break;
                default:
                    type = Duck.State.LookAround;
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