using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] ParticleSystem chargeParticle;
    [SerializeField] ParticleSystem groundCrackParticle;
    public enum State { Idle, Slash, Jab, Sting ,Charge};
    StateMachine<State, Sword> stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<State, Sword>(this);
        stateMachine.AddState(State.Idle, new SwordIdle(this, stateMachine));
        stateMachine.AddState(State.Slash, new SwordSlash(this, stateMachine));
        stateMachine.AddState(State.Jab, new SwordJab(this, stateMachine));
        stateMachine.AddState(State.Sting, new SwordSting(this, stateMachine));
        stateMachine.AddState(State.Charge, new SwordCharge(this, stateMachine));
    }

    public override void ForceIdle()
    {
        stateMachine.ChangeState(State.Idle);
    }

    public override void WeaponUpdate()
    {
        stateMachine.Update();
        curState = stateMachine.GetCurStateStr();
    }

    protected override void CallStateExit()
    {
        stateMachine.ForceExit();
    }

    protected override void StateMachineSetUp()
    {
        stateMachine.SetUp(State.Idle);
    }

    public void PlayChargeParticle(bool val)
    {
        chargeParticle.gameObject.SetActive(val);
    }

    public void PlayGroundCrackParticle(bool val, float scale = 1)
    {
        groundCrackParticle.gameObject.SetActive(val);
        groundCrackParticle.transform.localScale = Vector3.one * scale;
    }
}