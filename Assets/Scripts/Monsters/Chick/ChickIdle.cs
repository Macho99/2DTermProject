using System.Collections.Generic;
using UnityEngine;

public class ChickIdle : StateBase<Chick.State, Chick>
{
    float maxIdleDuration = 3f;
    float minIdleDuration = 1f;
    float curIdleDuration;
    public ChickIdle(Chick owner, StateMachine<Chick.State, Chick> stateMachine) : base(owner, stateMachine)
    {

    }

    public override void Enter()
    {
        if (owner.Target != null)
        {
            stateMachine.ChangeState(Chick.State.Runaway);
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
            Chick.State type;
            switch (idx)
            {
                case 0:
                    type = Chick.State.LookAround;
                    break;
                case 1:
                    type = Chick.State.Turn;
                    break;
                case 2:
                    type = Chick.State.Walk;
                    break;
                default:
                    type = Chick.State.LookAround;
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