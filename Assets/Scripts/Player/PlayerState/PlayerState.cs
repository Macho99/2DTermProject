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
    Stun,
    Block,
    Attack,
    Interact,

    Size
}

public abstract class PlayerState
{
    protected Player player;
    protected PlayerState(Player player)
    {
        this.player = player;
    }

    public virtual void TakeDamage(int damage, float force, float stunDuration)
    {
        // TODO : �����ϱ�
    }

    protected void Attack()
    {
        player.ChangeState(PlayerStateType.Attack);
    }

    public abstract void Jump(InputValue value);

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}