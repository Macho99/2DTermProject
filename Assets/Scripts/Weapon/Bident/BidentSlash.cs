using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BidentSlash : StateBase<Bident.State, Bident>
{
    float attackDelay = 0.3f;
    float endDelay = 1f;
    float enterTime;
    public BidentSlash(Bident owner, StateMachine<Bident.State, Bident> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.player.PlayAnim("Slash");
        owner.player.SetAnimAttackSpeed(owner.AttackSpeed);
        owner.BoxAttack(owner.Damage, owner.player.dir, 1.5f, 3f, 0f, attackDelay);
    }

    public override void Exit()
    {
        owner.player.SetAnimAttackSpeed();
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