using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnAir : PlayerState
{
    public PlayerOnAir(Player player) : base(player)
    {
    }
    public override void Enter()
    {
    }

    public override void Attack(InputValue value)
    {

    }

    public override void Jump(InputValue value)
    {

    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
