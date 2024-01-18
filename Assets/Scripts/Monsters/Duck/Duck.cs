using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Monster
{
    public enum State { Idle, LookAround, Detect, Turn, Walk, FlyAway, Landing, Stun, Die, Size };
    StateMachine<State, Duck> stateMachine;

    [SerializeField] string curState;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<State, Duck>(this);
        stateMachine.AddState(State.Idle, new DuckIdle(this, stateMachine));
        stateMachine.AddState(State.LookAround, new DuckLookAround(this, stateMachine));
        stateMachine.AddState(State.Detect, new DuckDetect(this, stateMachine));
        stateMachine.AddState(State.Turn, new DuckTurn(this, stateMachine));
        stateMachine.AddState(State.Walk, new DuckWalk(this, stateMachine));
        stateMachine.AddState(State.FlyAway, new DuckFlyAway(this, stateMachine));
        stateMachine.AddState(State.Landing, new DuckLanding(this, stateMachine));
        stateMachine.AddState(State.Stun, new DuckStun(this, stateMachine));
        stateMachine.AddState(State.Die, new DuckDie(this, stateMachine));
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

    public void SetGravityScale(float val)
    {
        rb.gravityScale = val;
    }

    public void FlyMove(Vector2 vel)
    {
        if (Time.time < lastHitTime + knockbackTime)
        {
            return;
        }
        rb.velocity = vel;
    }

    public void FlyMove(float direction, float maxSpeed, float time)
    {
        if (Time.time < lastHitTime + knockbackTime)
        {
            return;
        }

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            return;
        }

        rb.AddForce(Vector2.right * direction * accelSpeed * time + Vector2.up * 2f, ForceMode2D.Force);
    }
}
