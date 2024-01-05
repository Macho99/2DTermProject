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

    public Vector2 inputVec; 
    public bool blockInput;
    public bool isGround = false;
    public bool isRight = true;
    public float LastCombatTime { get; set; }
    public float ReadyDuration { get { return readyDuration; } }
    public float AirControlMultiple { get { return airControlMultiple; } }


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
        idx++;//Ready 상태는 Idle과 같으므로 만들지 않음

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
            isRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputVec.x > 0f)
        {
            isRight = true;
            transform.localScale = Vector3.one;
        }
    }

    private void OnVerticalMove(InputValue value)
    {
        inputVec.y  = value.Get<float>();
    }

    private void OnBlock(InputValue value)
    {
        blockInput = value.Get<float>() > 0.9f ? true : false ;
    }

    public void SetAnimState(PlayerStateType type)
    {
        anim.SetInteger("State", (int)type);
    }

    public void HorizonMove(float time)
    {
        HorizonMove(1f, 1f, time);
    }

    public void HorizonMove(float accelMulti, float time)
    {
        HorizonMove(accelMulti, 1f, time);
    }

    public void HorizonMove(float accelMulti, float maxMulti, float time)
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

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
    //       || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
    //    {
    //        col.isTrigger = false;
    //    }
    //}
}
