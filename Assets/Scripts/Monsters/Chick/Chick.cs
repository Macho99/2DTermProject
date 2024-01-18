using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : Monster
{
    public enum State { Idle, LookAround, Detect, Turn, Walk, Runaway, Stun, Die, Size };
    StateMachine<State, Chick> stateMachine;

    [SerializeField] string curState;


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<State, Chick>(this);
        stateMachine.AddState(State.Idle, new ChickIdle(this, stateMachine));
        stateMachine.AddState(State.LookAround, new ChickLookAround(this, stateMachine));
        stateMachine.AddState(State.Detect, new ChickDetect(this, stateMachine));
        stateMachine.AddState(State.Turn, new ChickTurn(this, stateMachine));
        stateMachine.AddState(State.Walk, new ChickWalk(this, stateMachine));
        stateMachine.AddState(State.Runaway, new ChickRunaway(this, stateMachine));
        stateMachine.AddState(State.Stun, new ChickStun(this, stateMachine));
        stateMachine.AddState(State.Die, new ChickDie(this, stateMachine));
    }

    private void Start()
    {
        stateMachine.SetUp(State.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        curState = stateMachine.GetCurStateStr();
    }

    public override void DetectPlayer(FieldPlayer player)
    {
        base.DetectPlayer(player);
        if (target == null)
        {
            target = player.transform;
            if (StunEndTime < Time.time)
            {
                stateMachine.ChangeState(State.Detect);
            }
        }
    }

    protected override void Die()
    {
        stateMachine.ChangeState(State.Die);
    }

    protected override void Stun()
    {
        stateMachine.ChangeState(State.Stun);
    }

    protected override void HittedDetect()
    {
        stateMachine.ChangeState(State.Detect);
    }
}
