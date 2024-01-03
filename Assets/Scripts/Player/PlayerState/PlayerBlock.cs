using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class PlayerBlock : PlayerState
{
    public PlayerBlock(Player player) : base(player)
    {
    }

    public override void Attack(InputValue value)
    {

    }
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if (false == player.blockInput)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }
    }
}