using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] string curStateStr;
    [SerializeField] private float accelSpeed = 5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float airControlMultiple = 0.5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float doubleJumpForce = 5f;
    [SerializeField] private float readyDuration = 3f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float stunTime = 0f;

    public Vector2 inputVec; 
    public bool BlockInput { get; private set; }
    public bool InteractInput { get; private set; }
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
        curStateStr = curState.ToString();
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
    }

    private void OnJump(InputValue value) {
        curState.Jump(value);
    }

    private void OnAttackBtn1(InputValue value)
    {
        curState.Attack(value);
    }

    private void OnHorizonMove(InputValue value)
    {
        inputVec.x = value.Get<float>();
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
        if(rb.velocity.x < -maxSpeed * maxMulti && inputVec.x< 0f)
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

    public void NormalAttack()
    {
        Vector2 origin = transform.position;
        origin.y += 0.5f;
        RaycastHit2D hit = Physics2D.BoxCast(origin, Vector2.one, 0f, Vector2.right * dir * 0.5f, 1f, LayerMask.GetMask("Monster"));

        if (hit.collider == null) return;

        if(hit.collider.TryGetComponent<Monster>(out Monster monster))
        {
            Vector2 knockbackDir;
            //거리가 가까우면 플레이어의 공격 방향으로 넉백 되도록
            if ((monster.transform.position - transform.position).sqrMagnitude < 0.4f * 0.4f)
            {
                knockbackDir = Vector2.right * dir;
            }
            else
            {
                knockbackDir = monster.transform.position - transform.position;
                knockbackDir.Normalize();
            }
            monster.TakeDamage(attackDamage, knockbackDir * knockbackForce, stunTime);
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
}
