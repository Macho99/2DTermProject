using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class FieldPlayer : MonoBehaviour, IInteractorOwner
{
    [SerializeField] string curStateStr;
    [SerializeField] private float accelSpeed = 1000f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float airControlMultiple = 0.5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float doubleJumpForce = 8f;
    [SerializeField] private float readyDuration = 3f;
    [SerializeField] private int curHp;
    [SerializeField] private int maxHp = 100;
    [SerializeField] private float hpDownDuration = 5f;
    [SerializeField] private int hpDownAmount = 2;
    [SerializeField] private Transform weaponFolder;
    [SerializeField] private SpriteLibrary weaponLibrary;
    [SerializeField] private List<Weapon> weaponList;
    [SerializeField] private Weapon curWeapon;
    [SerializeField] private ParticleSystem walkParticle;
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem stunParticle;
    [SerializeField] private ParticleSystem healParticle;

    private SpriteRenderer weaponSpRenderer;
    private RectTransform canvasRect;

    [HideInInspector] public UnityEvent onHpChanged;
    [HideInInspector] public UnityEvent onAttackBtn1Pressed;
    [HideInInspector] public UnityEvent onAttackBtn2Pressed;
    [HideInInspector] public UnityEvent onAttackState;
    [HideInInspector] public UnityEvent onHit;
    [HideInInspector] public UnityEvent onBlockUse;
    [HideInInspector] public UnityEvent<float> onMultiPurposeBarChanged;

    public Weapon CurWeapon { 
        get {
            return curWeapon;
        }
        set
        {
            curWeapon = value;
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
    public bool IsStunState { get; set; }
    public bool IsGround { get; set; }
    public bool DoubleJumped { get; set; }
    public int dir = 1; // 1이면 오른쪽, -1이면 왼쪽
    public float LastCombatTime { get; set; }
    public float ReadyDuration { get { return readyDuration; } }
    public float AirControlMultiple { get { return airControlMultiple; } }
    public Interactor Interactor { get; private set; }
    public float LastBlockTime { get; set; }
    public bool IsBlockState { get; set; }
    public bool IsDuckState { get; set; }
    public float StunEndTime { get; private set; }

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
        weaponList = new List<Weapon>();
        canvasRect = GetComponentInChildren<Canvas>().GetComponent<RectTransform>();

        curHp = maxHp;
        LastBlockTime = -Constants.BlockCoolTime;

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
        states[idx++] = new PlayerDie(this);

        curState = states[0];
        curState.Enter();
        curStateStr = curState.ToString();
    }

    public void PlayerStart()
    {
        onHpChanged?.Invoke();
        StartCoroutine(CoHpDown());
    }

    private IEnumerator CoHpDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(hpDownDuration);
            PlayerTakeDamage(hpDownAmount, Vector2.zero, 0f, false);
        }
    }

    public void ChangeState(PlayerStateType type)
    {
        if(curState == null) return;
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
        if (true == IsStunState) return;

        AddjustFlip();
    }

    public void AddjustFlip()
    {
        if (inputVec.x < 0f)
        {
            dir = -1;
            Vector3 vec = new Vector3(-1, 1, 1);
            transform.localScale = vec;
            canvasRect.localScale = vec;
        }
        else if (inputVec.x > 0f)
        {
            dir = 1;
            transform.localScale = Vector3.one;
            canvasRect.localScale = Vector3.one;
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

    public void SetAnimAttackSpeed(float speed = 1f)
    {
        anim.SetFloat("AttackSpeed", speed);
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

    public void Jump(Vector2 vec)
    {
        rb.AddForce(vec, ForceMode2D.Impulse);
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
        hit = Physics2D.BoxCast(transform.position + Vector3.up, new Vector2(0.5f, 0.2f), 0f, Vector2.up, 0.1f, LayerMask.GetMask("Platform"));

        return hit.collider == null ? false : true;
    }

    public bool CheckGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position, new Vector2(0.2f, 0.2f), 0f, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));

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
            col.offset = new Vector2(0f, 0.6f);
            col.size = new Vector2(0.6f, 1.2f);
        }
        else if(isBig == false)
        {
            col.offset = new Vector2(0f, 0.3f);
            col.size = new Vector2(0.6f, 0.6f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print($"{collision.gameObject.name}, enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if(CheckGround() == true)
            {
                DoubleJumped = false;
                IsGround = true;
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
                IsGround = false;
            }
        }
    }

    public void TakeDamage(Monster monster, int damage, Vector2 knockback, float stunDuration = 0f)
    {
        curState.TakeDamage(monster, damage, knockback, stunDuration);
    }

    public void PlayerTakeDamage(int damage, Vector2 knockback, float stunDuration, bool hitTrigger = true)
    {
        if(curHp == 0)
        {
            return;
        }

        onHit?.Invoke();
        curHp -= damage;

        if(hitTrigger == true)
        {
            anim.SetTrigger("Hit");
        }

        if(knockback.sqrMagnitude > 0.1f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }

        LastCombatTime = Time.time;

        onHpChanged?.Invoke();

        if (curHp <= 0)
        {
            curHp = 0;
            onHpChanged?.Invoke();
            Die();
            return;
        }

        else if(stunDuration > 0.01f)
        {
            StunEndTime = Time.time + stunDuration;
            if(true == IsAttackState)
            {
                CurWeapon.ForceIdle();
            }
            ChangeState(PlayerStateType.Stun);
        }
    }

    public void PlayWalkParticle(bool val)
    {
        walkParticle.gameObject.SetActive(val);
    }

    public void PlayJumpParticle()
    {
        jumpParticle.Play();
    }

    public void PlayStunParticle(bool val)
    {
        stunParticle.gameObject.SetActive(val);
    }
    
    public void Heal(int amount)
    {
        curHp += amount;
        if(curHp > maxHp)
            curHp = maxHp;

        onHpChanged?.Invoke();
        healParticle.Play();
    }

    public Weapon AddWeapon(Weapon weaponPrefab)
    {
        Weapon weaponObj = Instantiate(weaponPrefab, weaponFolder);
        weaponList.Add(weaponObj);
        return weaponObj;
    }

    public void DeleteWeapon(Weapon weaponObj)
    {
        if(weaponObj == curWeapon) curWeapon= null;

        Destroy(weaponObj.gameObject);
        weaponList.Remove(weaponObj);
    }

    public void WeaponSlotChanged(Weapon newWeaponObj)
    {
        SetCurWeapon(newWeaponObj);
    }

    public void SetCurWeapon(Weapon weaponObj = null)
    {
        curWeapon?.gameObject.SetActive(false);

        if (!weaponList.Contains(weaponObj))
        {
            if (weaponObj == null)
            {
                curWeapon = null;
                weaponLibrary.spriteLibraryAsset = null;
                weaponSpRenderer.sprite = null;
                return;
            }
            else
            {
                Debug.LogError("weaponList에 등록되지 않은 weapon입니다");
                return;
            }
        }

        curWeapon = weaponObj;
        weaponLibrary.spriteLibraryAsset = curWeapon.SpriteLibraryAsset;
        curWeapon.gameObject.SetActive(true);
    }

    public void SetMultiPurposeBar(float ratio)
    {
        onMultiPurposeBarChanged?.Invoke(ratio);
    }

    public void BlockUse()
    {
        onBlockUse?.Invoke();
    }

    private void Die()
    {
        StopAllCoroutines();
        ChangeState(PlayerStateType.Die);
    }
}