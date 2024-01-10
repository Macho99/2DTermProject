using System.Collections.Generic;
using UnityEngine;

public class SwordCharge: StateBase<Sword.State, Sword>
{
    float attackDuration = 0.2f;
    //float japPossibleDuration = 0.5f;
    float endDuration = 0.3f;
    float enterTime;
    bool attacked;
    public SwordCharge(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("차지샷");
        attacked = false;
        enterTime = Time.time;
        owner.player.PlayAnim("Slash");
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
                owner.BoxAttack((int)((float)owner.Damage * 2f), owner.player.dir, 5f, 5f);
                attacked = true;
            }
        }
    }
}