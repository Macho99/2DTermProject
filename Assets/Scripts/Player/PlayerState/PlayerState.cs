using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStateType
{
    Idle = 0,
    Walk,
    Duck,
    Crawl,
    Jump,
    OnAir,
    DoubleJump,
    Land,
    Hurt,
    Block,
    Slash,

    Size
}

public abstract class PlayerState
{
    protected Player player;
    protected PlayerState(Player player)
    {
        this.player = player;
    }

    public abstract void Jump(InputValue value);

    public abstract void Slash(InputValue value);

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}