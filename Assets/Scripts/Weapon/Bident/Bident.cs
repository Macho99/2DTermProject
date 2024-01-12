using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bident : Weapon
{
    [SerializeField] private ParticleSystem chargeParticle;
    private Transform CannonParticlePoint;

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

        CannonParticlePoint = new GameObject("CannonParticlePoint").transform;
        CannonParticlePoint.parent = transform;
        CannonParticlePoint.localPosition = new Vector2(0.5f, 0.5f);
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

    public void PlayExplosionParticle(float delay = 0f, float scale = 1f)
    {
        _ = StartCoroutine(CoPlayExplosion(delay, scale));
    }

    private IEnumerator CoPlayExplosion(float delay, float scale)
    {
        yield return new WaitForSeconds(delay);
        scale = Mathf.Min(scale, 0.6f);

        GameObject particle = FieldObjPool.Instance.AllocateObj(ObjPoolType.CannonShotParticle);
        particle.transform.position = CannonParticlePoint.position;
        particle.transform.localScale = Vector3.one * scale;
        if(-1 == Player.dir)
        {
            particle.transform.Rotate(Vector3.up, 180f);
        }
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
            Player.transform.Translate(movePerSecond * Time.deltaTime);
        }
    }
}