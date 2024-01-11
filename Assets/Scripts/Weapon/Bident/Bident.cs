using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bident : Weapon
{
    [SerializeField] private ParticleSystem chargeParticle;
    [SerializeField] private ParticleSystem explosionParticle;
    public enum State { Idle, Slash, Sting, Airborne, Charge };
    StateMachine<State, Bident> stateMachine;
    public float AttackSpeed {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        AttackSpeed = 0.5f;
        stateMachine = new StateMachine<State, Bident>(this);
        stateMachine.AddState(State.Idle, new BidentIdle(this, stateMachine));
        stateMachine.AddState(State.Slash, new BidentSlash(this, stateMachine));
        stateMachine.AddState(State.Sting, new BidentSting(this, stateMachine));
        stateMachine.AddState(State.Airborne, new BidentAirborne(this, stateMachine));
        stateMachine.AddState(State.Charge, new BidentCharge(this, stateMachine));
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

    public void PlayExplosionParticle(bool val, float delay = 0f, float scale = 1f)
    {
        _ = StartCoroutine(CoPlayExplosion(val, delay, scale));
    }

    private IEnumerator CoPlayExplosion(bool val, float delay, float scale)
    {
        yield return new WaitForSeconds(delay);
        scale = Mathf.Min(scale, 0.6f);

        explosionParticle.transform.localScale = Vector3.one * scale;
        explosionParticle.gameObject.SetActive(val);
    }

    public void PlayerTranslate(Vector2 vec, float duration = 0.2f)
    {
        StartCoroutine(CoPlayerTranslate(vec, duration));
    }

    private IEnumerator CoPlayerTranslate(Vector2 vec, float duration)
    {
        float endTime = Time.time + duration;
        Vector2 movePerSecond = vec / duration;

        while (Time.time < endTime)
        {
            yield return null;
            player.transform.Translate(movePerSecond * Time.deltaTime);
        }
    }
}