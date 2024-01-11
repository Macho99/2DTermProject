using System.Collections.Generic;
using UnityEngine;

public class BowIdle : StateBase<Bow.State, Bow>
{
    public BowIdle(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        owner.Player.ChangeState(PlayerStateType.Idle);
        owner.Player.onAttackState.AddListener(AttackStart);
    }

    public override void Exit()
    {
        owner.Player.onAttackState.RemoveListener(AttackStart);
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
        if (true == owner.Player.AttackBtn1Input)
        {
            stateMachine.ChangeState(Bow.State.Charge);
        }
        else if (true == owner.Player.AttackBtn2Input)
        {
            //stateMachine.ChangeState(Bow.State.BackStep);
        }
        else
        {
            Debug.LogError("공격 시작 했는데 버튼이 안눌림");
        }
    }
}