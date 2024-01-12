using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public enum State { Idle, Charge, BackStep, DoubleArrow, ArrowRain, PullOut }; 
    StateMachine<State, Bow> stateMachine;
    public Transform ShotPoint {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ShotPoint = new GameObject("ShotPoint").transform;
        ShotPoint.parent = transform;
        ShotPoint.localPosition = new Vector2(0.5f, 0.5f);

        stateMachine = new StateMachine<State, Bow>(this);
        stateMachine.AddState(State.Idle, new BowIdle(this, stateMachine));
        stateMachine.AddState(State.Charge, new BowCharge(this, stateMachine));
        stateMachine.AddState(State.BackStep, new BowBackStep(this, stateMachine));
        stateMachine.AddState(State.DoubleArrow, new BowDoubleArrow(this, stateMachine));
        stateMachine.AddState(State.ArrowRain, new BowArrowRain(this, stateMachine));
        stateMachine.AddState(State.PullOut, new BowPullOut(this, stateMachine));
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

    public void ShotArrow(int damage, Vector2 dir, float speed, float knockbackForce = 1f, float offTime = 10f)
    {
        Arrow arrow = FieldObjPool.Instance.AllocateObj(ObjPoolType.Arrow).GetComponent<Arrow>();
        arrow.Init(damage, ShotPoint.position, dir, speed, knockbackForce, offTime);
    }
}
