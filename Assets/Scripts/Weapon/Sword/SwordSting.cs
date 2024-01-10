using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSting : StateBase<Sword.State, Sword>
{
    float attackDuration = 0.1f;
    float chargePossibleDuration = 0f;
    float endDuration = 0.5f;
    float enterTime;
    bool attacked;
    public SwordSting(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("Jab");
        owner.player.onAttackBtn1Pressed.AddListener(GoCharge);
    }

    public override void Exit()
    {
        owner.player.onAttackBtn1Pressed.RemoveListener(GoCharge);
    }

    private void GoCharge()
    {
        if(Time.time > enterTime + chargePossibleDuration)
        {
            stateMachine.ChangeState(Sword.State.Charge);
        }
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > enterTime + endDuration)
        {
            stateMachine.ChangeState(Sword.State.Idle);
            owner.player.ChangeState(PlayerStateType.Idle);
        }
    }

    public override void Update()
    {
        if (false == attacked)
        {
            if (Time.time > enterTime + attackDuration)
            {
                owner.BoxAttack((int) ((float) owner.Damage * 0.5f), owner.player.dir, 5f, 0f);
                attacked = true;
            }
        }
    }
}