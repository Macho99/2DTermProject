using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrawl : PlayerState
{
    public PlayerCrawl(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.PlayAnim("Crawl");
    }

    public override void Exit()
    {
    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {
        if (false == player.isGround)
        {
            player.ChangeState(PlayerStateType.OnAir);
        }

        float inputX = player.inputVec.x;

        if (Mathf.Abs(inputX) < 0.01f)
        {
            player.ChangeState(PlayerStateType.Duck);
            return;
        }

        player.HorizonMove(TimeExtension.UnscaledDeltaTime , 1f, 0.4f);
    }
}
