using System.Collections.Generic;
using UnityEngine;

public class SwordIdle : StateBase<Sword.State, Sword>
{
    public SwordIdle(Sword owner, StateMachine<Sword.State, Sword> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
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
        if(true == owner.player.AttackBtn1Input)
        {
            stateMachine.ChangeState(Sword.State.Slash);
        }
        else if(true == owner.player.AttackBtn2Input)
        {
            //미구현
            owner.player.ChangeState(PlayerStateType.Idle);
        }
        else
        {
            Debug.Log("공격 시작 했는데 버튼이 안눌림");
        }
    }
}