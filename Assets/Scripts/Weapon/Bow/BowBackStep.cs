using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BowBackStep : StateBase<Bow.State, Bow>
{
    float nextPossibleDelay = 0.1f;
    float endDelay = 0.5f;
    float enterTime;
    public BowBackStep(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.Player.PlayAnim("BackStep");

        Vector2 jumpDir = Vector2.right * -owner.Player.dir * 5f;
        jumpDir.y += 1f;
        owner.Player.Jump(jumpDir);

        owner.Player.onAttackBtn1Pressed.AddListener(GoArrowRain);
        owner.Player.onAttackBtn2Pressed.AddListener(GoDoubleArrow);
    }

    public override void Exit()
    {
        owner.Player.onAttackBtn1Pressed.RemoveListener(GoArrowRain);
        owner.Player.onAttackBtn2Pressed.RemoveListener(GoDoubleArrow);
    }

    private void GoDoubleArrow()
    {
        if(Time.time > enterTime + nextPossibleDelay)
        {
            stateMachine.ChangeState(Bow.State.DoubleArrow);
        }
    }

    private void GoArrowRain()
    {
        if (Time.time > enterTime + nextPossibleDelay)
        {
            stateMachine.ChangeState(Bow.State.ArrowRain);
        }
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