using System.Collections.Generic;
using UnityEngine;

public class RoosterIdle : StateBase<Rooster.State, Rooster>
{
    float maxIdleDuration = 3f;
    float minIdleDuration = 1f;
    float curIdleDuration;
    public RoosterIdle(Rooster owner, StateMachine<Rooster.State, Rooster> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if(owner.Target != null)
        {
            stateMachine.ChangeState(Rooster.State.Trace);
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
            Rooster.State type;
            switch (idx)
            {
                case 0:
                    type = Rooster.State.LookAround;
                    break;
                case 1:
                    type = Rooster.State.Turn;
                    break;
                case 2:
                    type = Rooster.State.Walk;
                    break;
                default:
                    type = Rooster.State.LookAround;
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

