using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordJab : StateBase<Sword.State, Sword>
{
    float attackDelay = 0.1f;
    float endDelay = 0.3f;
    float enterTime;
    public SwordJab(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.player.PlayAnim("Jab");
        owner.BoxAttack(owner.Damage / 3, owner.player.dir, 1f, 3f, 0f, attackDelay);
    }

    public override void Exit()
    {
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