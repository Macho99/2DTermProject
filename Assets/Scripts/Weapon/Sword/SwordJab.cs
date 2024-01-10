using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordJab : StateBase<Sword.State, Sword>
{
    float attackDuration = 0.1f;
    //float chargePossibleDuration = 0.2f;
    float endDuration = 0.3f;
    float enterTime;
    bool attacked;
    public SwordJab(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("Jab");
    }

    public override void Exit()
    {
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
                owner.BoxAttack((int)((float)owner.Damage * 0.3f), owner.player.dir, 3f, 0f);
                attacked = true;
            }
        }
    }
}