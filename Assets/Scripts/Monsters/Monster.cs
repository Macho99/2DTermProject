using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterUIState { Detect, Miss }
public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected int curHp;
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int damage = 10;
    [SerializeField] float moveMaxSpeed = 2f;
    [SerializeField] float runMaxSpeed = 3f;
    [SerializeField] protected float accelSpeed = 1000f;
    [SerializeField] Transform flipable;
    [SerializeField] Transform arrowHolder;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem stunParticle;
    [SerializeField] protected float knockbackTime = 0.3f;
    [SerializeField] float lookRange = 10f;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected BoxCollider2D col;
    protected Transform target;

    [HideInInspector] public UnityEvent onHpChanged;
    public MonsterUIState curUIState;
    [HideInInspector] public UnityEvent onUIStateChanged;

    public float LookRange { get { return lookRange; } }
    public int Damage { get {  return damage; } }
    public float CurHp { get { return curHp; } }
    public float MaxHp { get { return maxHp; } }
    public Transform Target { get { return target; } set { target = value; } }
    public int dir { get; set; }
    public float MoveSpeed { get { return moveMaxSpeed; } }
    public float RunSpeed { get { return runMaxSpeed; } }
    public Transform ArrowHolder { get { return arrowHolder; } }
    public float StunEndTime { get; private set; }

    protected float lastHitTime = 0f;

    protected virtual void Awake()
    {
        StunEndTime = -10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        dir = 1;

        curHp = maxHp;
    }

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

    public void PlayStunParticle(bool val)
    {
        stunParticle.gameObject.SetActive(val);
    }

    public void UIStateChange(MonsterUIState type)
    {
        curUIState = type;
        onUIStateChanged?.Invoke();
    }

    //public void HorizonMove(float velX)
    //{
    //    if (Time.time < lastHitTime + knockbackTime)
    //    {
    //        return;
    //    }
    //    rb.velocity = new Vector2(velX, rb.velocity.y);
    //}

    public void HorizonMove(float direction, float maxSpeed , float time)
    {
        if (Time.time < lastHitTime + knockbackTime)
        {
            return;
        }

        if (rb.velocity.x < -maxSpeed && direction < 0f)
        {
            return;
        }
        if (rb.velocity.x > maxSpeed && direction > 0f)
        {
            return;
        }
        rb.AddForce(Vector2.right * direction * accelSpeed * time, ForceMode2D.Force);
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

    public void TakeDamage(int damage, Vector2 knockback, float stunDuration = 0f)
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
            if(knockback.x * dir > 0f)
            {
                Flip();
            }
            Vector2 dieKnockback;
            if((knockback.normalized * 7f).sqrMagnitude < knockback.sqrMagnitude){
                dieKnockback = knockback;
            }
            else{
                dieKnockback = knockback.normalized * 7f;
            }
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
            rb.AddForce(dieKnockback, ForceMode2D.Impulse);
            curHp = 0;
            Die();

            GetComponent<MonsterDrop>().EnableInteractable();

            return;
        }

        rb.AddForce(knockback, ForceMode2D.Impulse);

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

    public void ArrowPullOut(int damagePerArrow)
    {
        int cnt = 0;
        foreach(Transform arrow in arrowHolder)
        {
            cnt++;
            _ = StartCoroutine(CoPullOut(arrow.GetComponent<Arrow>()));
        }
        if (0 == cnt) return;
        TakeDamage(damagePerArrow * cnt, Vector2.right * -dir);
    }

    private IEnumerator CoPullOut(Arrow arrow)
    {
        float dist = 1f;
        float speed = 3f;
        float curDist = 0f;
        while (curDist < dist)
        {
            if (false == arrow.gameObject.activeSelf) break;

            float moveDist = speed * Time.deltaTime;
            curDist += moveDist;
            arrow.transform.Translate(-Vector3.right * moveDist, Space.Self);
            yield return null;
        }
        arrow.ObjReturn();
    }
}