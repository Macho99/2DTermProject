using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSlash : StateBase<Sword.State, Sword>
{
    float attackDelay = 0.2f;
    float stingPossibleDelay = 0.2f;
    float endDelay = 0.7f;
    float enterTime;
    public SwordSlash(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.player.onAttackBtn2Pressed.AddListener(GoSting);
        owner.player.PlayAnim("Slash");
        owner.BoxAttack(owner.Damage, owner.player.dir, 1f, 2f, 0f, attackDelay);
    }

    public override void Exit()
    {
        owner.player.onAttackBtn2Pressed.RemoveListener(GoSting);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if(Time.time > enterTime + endDelay)
        {
            stateMachine.ChangeState(Sword.State.Idle);
        }
    }

    public override void Update()
    {

    }

    private void GoSting()
    {
        if(Time.time > enterTime + stingPossibleDelay)
        {
            stateMachine.ChangeState(Sword.State.Sting);
        }
    }
}