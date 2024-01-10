using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    [SerializeField] string curStateStr;
    [SerializeField] private float accelSpeed = 5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float airControlMultiple = 0.5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float doubleJumpForce = 5f;
    [SerializeField] private float readyDuration = 3f;
    [SerializeField] private int curHp;
    [SerializeField] private int maxHp = 100;
    [SerializeField] private Transform weaponFolder;
    [SerializeField] private SpriteLibrary weaponLibrary;
    [SerializeField] private Weapon curWeapon;

    private SpriteRenderer weaponSpRenderer;

    public UnityEvent onHpChanged;
    public UnityEvent onAttackBtn1Pressed;
    public UnityEvent onAttackBtn2Pressed;
    public UnityEvent onAttackState;

    public Weapon CurWeapon { 
        get {
            return curWeapon;
        }
        set
        {
            curWeapon = value;
            if(value == null)
            {
                weaponLibrary.spriteLibraryAsset = null;
                weaponSpRenderer.sprite = null;
                return;
            }
            weaponLibrary.spriteLibraryAsset = curWeapon.SpriteLibraryAsset;
        }
    }

    public int MaxHp { get { return maxHp; } }
    public int CurHp { get { return curHp; } }
    public Vector2 inputVec; 
    public bool AttackBtn1Input { get; private set; }
    public bool AttackBtn2Input { get; private set; } 
    public bool BlockInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool IsAttackState { get; set; }
    public bool isGround = false;
    public bool doubleJumped = false;
    public int dir = 1; // 1이면 오른쪽, -1이면 왼쪽
    public float LastCombatTime { get; set; }
    public float ReadyDuration { get { return readyDuration; } }
    public float AirControlMultiple { get { return airControlMultiple; } }
    public Interactor Interactor { get; private set; }

    PlayerState curState;
    PlayerState[] states;
    Rigidbody2D rb;
    Animator anim;

    CapsuleCollider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        Interactor = GetComponentInChildren<Interactor>();
        weaponSpRenderer = weaponLibrary.GetComponent<SpriteRenderer>();

        curHp = maxHp;

        // TODO: 상태 변경 관리하기
        states = new PlayerState[(int) PlayerStateType.Size];

        int idx = 0;
        states[idx++] = new PlayerIdle(this);
        states[idx++] = new PlayerWalk(this);
        states[idx++] = new PlayerDuck(this);
        states[idx++] = new PlayerCrawl(this);
        states[idx++] = new PlayerJump(this);
        states[idx++] = new PlayerOnAir(this);
        states[idx++] = new PlayerDoubleJump(this);
        states[idx++] = new PlayerLand(this);
        states[idx++] = new PlayerStun(this);
        states[idx++] = new PlayerBlock(this);
        states[idx++] = new PlayerAttack(this);
        states[idx++] = new PlayerInteract(this);

        curState = states[0];
        curState.Enter();
        curStateStr = curState.ToString();
    }

    private void Start()
    {
        onHpChanged?.Invoke();
    }

    public void ChangeState(PlayerStateType type)
    {
        curState.Exit();

        curState = states[(int)type];
        curStateStr = curState.ToString();
        //print(type.ToString());

        curState.Enter();
    }

    private void Update()
    {
        curState.Update();
        CurWeapon?.WeaponUpdate();
    }

    private void OnJump(InputValue value) {
        curState.Jump(value);
    }

    private void OnAttackBtn1(InputValue value)
    {
        AttackBtn1Input = (value.Get<float>() > 0.9f) ? true : false;

        if(true == AttackBtn1Input)
        {
            onAttackBtn1Pressed?.Invoke();
        }
    }

    private void OnAttackBtn2(InputValue value)
    {
        AttackBtn2Input = (value.Get<float>() > 0.9f) ? true : false;

        if(true == AttackBtn2Input)
        {
            onAttackBtn2Pressed?.Invoke();
        }
    }

    private void OnHorizonMove(InputValue value)
    {
        inputVec.x = value.Get<float>();
        if (true == IsAttackState) return;

        AddjustFlip();
    }

    public void AddjustFlip()
    {
        if (inputVec.x < 0f)
        {
            dir = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputVec.x > 0f)
        {
            dir = 1;
            transform.localScale = Vector3.one;
        }
    }

    private void OnVerticalMove(InputValue value)
    {
        inputVec.y  = value.Get<float>();
    }

    private void OnBlock(InputValue value)
    {
        BlockInput = value.Get<float>() > 0.9f ? true : false;
    }

    private void OnInteract(InputValue value)
    {
        InteractInput = value.Get<float>() > 0.9f ? true : false;
    }

    public void PlayAnim(string name)
    {
        anim.Play(name);
    }

    public void HorizonMove(float time, float accelMulti = 1f, float maxMulti = 1f)
    {
        if(rb.velocity.x < -maxSpeed * maxMulti && inputVec.x < 0f)
        {
            return;
        }
        if(rb.velocity.x > maxSpeed * maxMulti && inputVec.x > 0f)
        {
            return;
        }
        rb.AddForce(Vector2.right * inputVec.x * (accelMulti * accelSpeed) * time, ForceMode2D.Force);
    }

    public void HorizonBreak(float time)
    {
        time *= 100f;
        rb.AddForce(new Vector2(-rb.velocity.x * 0.5f * time, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Jump(float force)
    {
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    public void DoubleJump()
    {
        float dir;
        if(inputVec.x == 0f)
        {
            dir = transform.localScale.x;
        }
        else
        {
            dir = inputVec.x;
        }
        rb.velocity = new Vector2(0f, rb.velocity.y * 0.5f);
        Jump(jumpForce * 0.5f);
        rb.AddForce(Vector2.right * dir * doubleJumpForce, ForceMode2D.Impulse);
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public bool IsAnimatorStateName(string str)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(str);
    }

    public bool CheckTop()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position + Vector3.up, new Vector2(0.5f, 0.2f), 0f, Vector2.zero, 0f, LayerMask.GetMask("Platform"));

        return hit.collider == null ? false : true;
    }

    public bool CheckGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position, new Vector2(0.2f, 0.2f), 0f, Vector2.zero, 0f, LayerMask.GetMask("Platform"));

        return hit.collider == null ? false : true;
    }

    public void ForceInteractStop()
    {
        if (curState.GetType().Equals(typeof(PlayerInteract)))
        {
            ChangeState(PlayerStateType.Idle);
        }
    }

    public void SetColliderSize(bool isBig)
    {
        if(isBig == true)
        {
            col.offset = new Vector2(0f, 0.5f);
            col.size = new Vector2(0.5f, 1f);
        }
        else if(isBig == false)
        {
            col.offset = new Vector2(0f, 0.3f);
            col.size = new Vector2(0.5f, 0.6f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print($"{collision.gameObject.name}, enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if(CheckGround() == true)
            {
                isGround = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print($"{collision.gameObject.name}, exit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if (CheckGround() == false)
            {
                isGround = false;
            }
        }
    }

    public void TakeDamage(int damage, Vector2 knockBack, float stunDuration = 0f)
    {
        curHp -= damage;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        //hitParticle.Play();
        anim.SetTrigger("Hit");
        rb.AddForce(knockBack, ForceMode2D.Impulse);
        LastCombatTime = Time.time;

        onHpChanged?.Invoke();

        if (curHp <= 0)
        {
            curHp = 0;
            onHpChanged?.Invoke();
            Die();
            return;
        }

        //if (stunDuration > 0.1f)
        //{
        //    StunEndTime = Time.time + stunDuration;
        //    Stun();
        //}
    }

    private void Die()
    {

    }
}