using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster : Monster
{
    public enum State { Idle, LookAround, Detect, Turn, Walk, Trace, Attack, Stun, Die, Size};
    StateMachine<State, Rooster> stateMachine;

    [SerializeField] float traceRange;
    [SerializeField] float attackDist;
    [SerializeField] float attackDuration;
    [SerializeField] float knockbackForce = 3f;
    [SerializeField] string curState;

    public float KnockbackForce {  get { return knockbackForce; } }
    public float AttackDuration { get { return attackDuration; } }
    public float LastAttackTime {  get; set; }
    public float AttackDist { get { return attackDist; } }
    public float TraceRange { get { return traceRange; } }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<State, Rooster> (this);
        stateMachine.AddState(State.Idle, new RoosterIdle(this, stateMachine));
        stateMachine.AddState(State.LookAround, new RoosterLookAround(this, stateMachine));
        stateMachine.AddState(State.Detect, new RoosterDetect(this, stateMachine));
        stateMachine.AddState(State.Turn, new RoosterTurn(this, stateMachine));
        stateMachine.AddState(State.Walk, new RoosterWalk(this, stateMachine));
        stateMachine.AddState(State.Trace, new RoosterTrace(this, stateMachine));
        stateMachine.AddState(State.Attack, new RoosterAttack(this, stateMachine));
        stateMachine.AddState(State.Stun, new RoosterStun(this, stateMachine));
        stateMachine.AddState(State.Die, new RoosterDie(this, stateMachine));

        LastAttackTime = Time.time;
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

    public override void DetectPlayer(Player player)
    {
        if(target == null)
        {
            target = player.transform;
            if(StunEndTime < Time.time)
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