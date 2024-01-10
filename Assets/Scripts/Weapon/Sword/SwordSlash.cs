using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSlash : StateBase<Sword.State, Sword>
{
    float attackDuration = 0.2f;
    float japPossibleDuration = 0.2f;
    float endDuration = 0.7f;
    float enterTime;
    bool attacked;
    public SwordSlash(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.player.onAttackBtn2Pressed.AddListener(GoSting);
        owner.player.PlayAnim("Slash");
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
        if(Time.time > enterTime + endDuration)
        {
            stateMachine.ChangeState(Sword.State.Idle);
            owner.player.ChangeState(PlayerStateType.Idle);
        }
    }

    public override void Update()
    {
        if(false == attacked)
        {
            if(Time.time > enterTime + attackDuration)
            {
                owner.BoxAttack(owner.Damage, owner.player.dir, 2f, 0f);
                attacked = true;
            }
        }
    }

    private void GoSting()
    {
        if(Time.time > enterTime + japPossibleDuration)
        {
            stateMachine.ChangeState(Sword.State.Sting);
        }
    }
}