using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSting : StateBase<Sword.State, Sword>
{
    float attackDelay = 0.1f;
    float chargePossibleDelay = 0.1f;
    float endDelay = 0.5f;
    float enterTime;
    public SwordSting(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.player.PlayAnim("Jab");
        owner.player.onAttackBtn1Pressed.AddListener(GoCharge);
        owner.BoxAttack(owner.Damage / 2, owner.player.dir, 1f, 5f, 0f, attackDelay);
    }

    public override void Exit()
    {
        owner.player.onAttackBtn1Pressed.RemoveListener(GoCharge);
    }

    private void GoCharge()
    {
        if(Time.time > enterTime + chargePossibleDelay)
        {
            stateMachine.ChangeState(Sword.State.Charge);
        }
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > enterTime + endDelay)
        {
            stateMachine.ChangeState(Sword.State.Idle);
        }
    }

    public override void Update()
    {

    }
}