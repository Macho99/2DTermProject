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

    public virtual void TakeDamage(Monster monster, int damage, Vector2 knockback, float stunDuration)
    {
        player.PlayerTakeDamage(damage, knockback, stunDuration);
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