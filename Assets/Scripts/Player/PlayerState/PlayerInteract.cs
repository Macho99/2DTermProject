using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : PlayerState
{
    public PlayerInteract(FieldPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        if (player.Interactor.InteractStart() == false)
        {
            player.ChangeState(PlayerStateType.Idle); 
            return;
        }
        player.PlayAnim("Interact");
    }

    public override void Exit()
    {
        player.Interactor.InteractStop();
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if(false == player.InteractInput)
        {
            player.ChangeState(PlayerStateType.Idle);
            return;
        }
    }
}