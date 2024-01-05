using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int damage = 10;
    [SerializeField] float moveSpeed = 3;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected BoxCollider2D col;
    protected int curHp;
    protected Transform target;
    
    public Transform Target { get { return target; } set { target = value; } }
    public bool IsRight { get; set; }
    public float MoveSpeed { get { return moveSpeed; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        IsRight = true;

        curHp = maxHp;
    }

    public void SetVel(Vector2 vel)
    {
        rb.velocity = vel;
    }

    protected virtual void Die()
    {
        //TODO: 애니메이터 세팅
    }

    public void TakeDamage(int damage, Vector2 knockBack, float stunDuration = 0f)
    {
        curHp -= damage;
        rb.AddForce(knockBack, ForceMode2D.Impulse);
        if (curHp < 0)
        {
            curHp = 0;
            Die();
        }
        //TODO : stun
    }

    public void HitPlayer(int damage, Vector2 knockBack, float stunDuration = 0f)
    {
        //TODO : 구현
    }

    public void AnimPlay(string name)
    {
        anim.Play(name);
    }
    public bool IsAnimatorStateName(string str)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(str);
    }

    public abstract void DetectPlayer(Player player);
}