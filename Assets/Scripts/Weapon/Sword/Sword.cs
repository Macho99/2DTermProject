using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] ParticleSystem chargeParticle;
    Transform crackTrans;
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
        
        crackTrans = new GameObject("CrackPoint").transform;
        crackTrans.parent = transform;
        crackTrans.localPosition = new Vector2(0.953f, 0.04f);
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

    public void PlayGroundCrackParticle(float delay, float scale = 1)
    {
        _ = StartCoroutine(CoPlayGroundCrack(delay, scale));
    }

    private IEnumerator CoPlayGroundCrack(float delay, float scale = 1)
    {
        yield return new WaitForSeconds(delay);
        scale += 0.2f;
        scale = Mathf.Min(scale, 0.8f);

        GameObject particle = FieldObjPool.Instance.AllocateObj(ObjPoolType.GroundCrackParticle);
        particle.transform.position = crackTrans.position;
        particle.transform.localScale = Vector3.one * scale;
    }
}