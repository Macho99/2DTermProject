using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStateType
{
    Idle = 0,
    Walk,
    Duck,
    OnAir,
    OneJump,
    DoubleJump,
    Hurt,
    Attack,

    Size
}

public abstract class PlayerState
{
    protected static bool downPressed;

    protected Player player;
    protected PlayerState(Player player)
    {
        this.player = player;
    }

    public abstract void Jump(InputValue value);

    public abstract void Attack(InputValue value);

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}