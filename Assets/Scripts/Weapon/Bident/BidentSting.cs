using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BidentSting : StateBase<Bident.State, Bident>
{
    float attackDelay = 0.3f;
    float chargePossibleDelay = 0.2f;
    float endDelay = 0.6f;
    float enterTime;
    public BidentSting(Bident owner, StateMachine<Bident.State, Bident> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.Player.PlayAnim("Jab");
        owner.Player.SetAnimAttackSpeed();
        owner.Player.onAttackBtn1Pressed.AddListener(GoAirborne);
        owner.BoxAttack(owner.Damage / 2, owner.Player.dir, 1.5f, 3f, 0f, attackDelay);
    }

    public override void Exit()
    {
        owner.Player.SetAnimAttackSpeed();
        owner.Player.onAttackBtn1Pressed.RemoveListener(GoAirborne);
    }

    private void GoAirborne()
    {
        if (Time.time > enterTime + chargePossibleDelay)
        {
            stateMachine.ChangeState(Bident.State.Airborne);
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