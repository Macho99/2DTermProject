using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class PlayerDie : PlayerState
{
    public PlayerDie(FieldPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Die");
        FieldSceneFlowController.Instance.PlayerDie();
    }

    public override void Exit()
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {

    }
}