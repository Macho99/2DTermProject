using System.Collections.Generic;
using UnityEngine;

public class BowIdle : StateBase<Bow.State, Bow>
{
    public BowIdle(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.player.ChangeState(PlayerStateType.Idle);
        owner.player.onAttackState.AddListener(AttackStart);
    }

    public override void Exit()
    {
        owner.player.onAttackState.RemoveListener(AttackStart);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {

    }

    public override void Update()
    {

    }

    private void AttackStart()
    {
        if (true == owner.player.AttackBtn1Input)
        {
            stateMachine.ChangeState(Bow.State.Charge);
        }
        else if (true == owner.player.AttackBtn2Input)
        {
            //stateMachine.ChangeState(Bow.State.BackStep);
        }
        else
        {
            Debug.LogError("공격 시작 했는데 버튼이 안눌림");
        }
    }
}