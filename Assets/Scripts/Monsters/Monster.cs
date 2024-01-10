using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterUIState { Detect, Miss, Stun}
public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected int curHp;
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int damage = 10;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] Transform flipable;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] float knockbackTime = 0.5f;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected BoxCollider2D col;
    protected Transform target;

    [HideInInspector] public UnityEvent onHpChanged;
    public MonsterUIState curUIState;
    [HideInInspector] public UnityEvent onUIStateChanged;

    public int Damage { get {  return damage; } }
    public float CurHp { get { return curHp; } }
    public float MaxHp { get { return maxHp; } }
    public Transform Target { get { return target; } set { target = value; } }
    public int dir { get; set; }
    public float MoveSpeed { get { return moveSpeed; } }
    public float RunSpeed { get { return runSpeed; } }

    public float StunEndTime { get; private set; }

    private float lastHitTime = 0f;
    public void Flip()
    {
        if (dir == 1)
        {
            flipable.rotation = Quaternion.Euler(0f, 180f, 0f);
            dir = -1;
        }
        else
        {
            flipable.rotation = Quaternion.Euler(0f, 0f, 0f);
            dir = 1;
        }
    }

    public void UIStateChange(MonsterUIState type)
    {
        curUIState = type;
        onUIStateChanged?.Invoke();
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        dir = 1;

        curHp = maxHp;
    }

    public void HorizonMove(float velX)
    {
        if (Time.time < lastHitTime + knockbackTime)
        {
            return;
        }
        rb.velocity = new Vector2(velX, rb.velocity.y);
    }

    public void SetVel(Vector2 vel)
    {
        if(Time.time < lastHitTime + knockbackTime)
        {
            return;
        }
        rb.velocity = vel;
    }

    protected abstract void Die();
    protected abstract void Stun();
    protected abstract void HittedDetect();

    public void TakeDamage(int damage, Vector2 knockBack, float stunDuration = 0f)
    {
        if(curHp <= 0)
            return;

        curHp -= damage;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        hitParticle.Play();
        
        lastHitTime = Time.time;
        onHpChanged?.Invoke();
        if(target == null)
        {
            target = FieldSceneFlowController.Player.transform;
            HittedDetect();
        }

        if (curHp <= 0)
        {
            if(Mathf.Sign(knockBack.x * dir) > 0f)
            {
                Flip();
            }

            rb.AddForce(knockBack.normalized * 7f, ForceMode2D.Impulse);
            curHp = 0;
            Die();

            GetComponent<MonsterDrop>().EnableInteractable();

            return;
        }

        rb.AddForce(knockBack, ForceMode2D.Impulse);

        if (stunDuration > 0.1f)
        {
            StunEndTime = Time.time + stunDuration;
            Stun();
        }
    }

    public void HitPlayer(int damage, Vector2 knockBack, float stunDuration = 0f)
    {
        //TODO : ±¸Çö
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