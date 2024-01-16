using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : Monster
{
    public enum State { Idle, LookAround, Detect, Turn, Walk, Trace, ChargeWarn, Charge, Stun, Die, Size };
    StateMachine<State, Buffalo> stateMachine;

    [SerializeField] float attackDist = 5f;
    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float chargeDuration = 3f;
    [SerializeField] float chargeCoolTime = 3f;
    [SerializeField] ParticleSystem chargeParticle;
    [SerializeField] string curState;

    public float LastChargeTime { get; set; }
    public float ChargeCoolTime { get { return chargeCoolTime; } }
    public float ChargeDuration { get { return chargeDuration; } }
    public float KnockbackForce { get { return knockbackForce; } }
    public float LastAttackTime { get; set; }
    public float AttackDist { get { return attackDist; } }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<State, Buffalo>(this);
        stateMachine.AddState(State.Idle, new BuffaloIdle(this, stateMachine));
        stateMachine.AddState(State.LookAround, new BuffaloLookAround(this, stateMachine));
        stateMachine.AddState(State.Detect, new BuffaloDetect(this, stateMachine));
        stateMachine.AddState(State.Turn, new BuffaloTurn(this, stateMachine));
        stateMachine.AddState(State.Walk, new BuffaloWalk(this, stateMachine));
        stateMachine.AddState(State.Trace, new BuffaloTrace(this, stateMachine));
        stateMachine.AddState(State.ChargeWarn, new BuffaloChargeWarn(this, stateMachine));
        stateMachine.AddState(State.Charge, new BuffaloCharge(this, stateMachine));
        stateMachine.AddState(State.Stun, new BuffaloStun(this, stateMachine));
        stateMachine.AddState(State.Die, new BuffaloDie(this, stateMachine));

        LastAttackTime = Time.time;
        LastChargeTime = Time.time - chargeCoolTime;
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

    public void PlayChargeParticle(bool val)
    {
        chargeParticle.gameObject.SetActive(val);
    }
}