using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BidentAirborne : StateBase<Bident.State, Bident>
{
    float attackDelay = 0.1f;
    float chargePossibleDelay = 0.1f;
    float endDelay = 0.5f;
    float enterTime;

    public BidentAirborne(Bident owner, StateMachine<Bident.State, Bident> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.Player.PlayAnim("Airborne");
        owner.Player.onAttackBtn2Pressed.AddListener(GoCharge);
        owner.BoxAttack(owner.Damage / 2, owner.Player.dir, 1.5f, new Vector2(owner.Player.dir * 0.1f, 0.9f) * 8f, 1f, attackDelay);
    }

    public override void Exit()
    {
        owner.Player.onAttackBtn2Pressed.RemoveListener(GoCharge);
    }

    private void GoCharge()
    {
        if (Time.time > enterTime + chargePossibleDelay)
        {
            stateMachine.ChangeState(Bident.State.Charge);
        }
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > enterTime + endDelay)
        {
            stateMachine.ChangeState(Bident.State.Idle);
        }
    }

    public override void Update()
    {

    }
}