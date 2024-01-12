using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BowDoubleArrow : StateBase<Bow.State, Bow>
{
    float endDelay = 0.3f;
    float enterTime;
    public BowDoubleArrow(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.Player.PlayAnim("Shot"); 
        Vector2 direction = Vector2.right * owner.Player.dir;
        owner.ShotArrow(owner.Damage, direction + Vector2.up * 0.15f, 10f, 0.5f);
        owner.ShotArrow(owner.Damage, direction, 10f, 0.5f);
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
            stateMachine.ChangeState(Bow.State.Idle);
        }
    }

    public override void Update()
    {

    }
}