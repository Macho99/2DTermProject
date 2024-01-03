using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] string curStateStr;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float airControlMultiple = 0.5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float doubleJumpForce = 5f;

    public Vector2 inputVec;

    PlayerState curState;
    Rigidbody2D rb;
    PlayerState[] states;
    Animator anim;

    Collider2D col;

    public bool blockInput;

    public bool isGroundChecked = false;
    public bool isGround = false;
    public float AirControlMultiple { get { return airControlMultiple; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();


        // TODO: 상태 변경 관리하기
        states = new PlayerState[(int) PlayerStateType.Size];

        int idx = 0;
        states[idx++] = new PlayerIdle(this);
        states[idx++] = new PlayerWalk(this);
        states[idx++] = new PlayerDuck(this);
        states[idx++] = new PlayerJump(this);
        states[idx++] = new PlayerOnAir(this);
        states[idx++] = new PlayerDoubleJump(this);
        states[idx++] = new PlayerLand(this);
        states[idx++] = new PlayerHurt(this);
        states[idx++] = new PlayerBlock(this);
        states[idx++] = new PlayerAttack(this);

        curState = states[0];
        curStateStr = curState.ToString();
    }

    public void ChangeState(PlayerStateType type)
    {
        curState.Exit();

        curState = states[(int)type];
        curStateStr = curState.ToString();
        anim.SetInteger("State", (int)type);
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

    private void OnAttack(InputValue value)
    {
        curState.Attack(value);
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        if(inputVec.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(inputVec.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void OnBlock(InputValue value)
    {
        blockInput = value.Get<float>() > 0.9f ? true : false ;
    }

    public void HorizonMove(float time)
    {
        float value = inputVec.x;

        if (value < -0.5f)
            value = -1f;
        else if (value > 0.5f)
            value = 1f;

        HorizonMove(value, time);
    }

    public void HorizonMove(float value, float time)
    {
        if(rb.velocity.x < -maxSpeed && value < 0f)
        {
            return;
        }
        if(rb.velocity.x > maxSpeed && value > 0f)
        {
            return;
        }

        rb.AddForce(Vector2.right * value * moveSpeed * time, ForceMode2D.Force);
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

    public void Down()
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        int num = col.GetContacts(contactPoints);
        for (int i = 0; i < num; i++)
        {
            if (contactPoints[i].collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                isGround = false;
                col.isTrigger = true;
                break;
            }
        }
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public bool IsAnimatorStateName(string str)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(str);
    }

    public bool CheckCollisioning()
    {
        Collider2D[] cols =  new Collider2D[1];
        return col.GetContacts(cols) < 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print($"{collision.gameObject.name}, enter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
           || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print($"{collision.gameObject.name}, exit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
           || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
        {
            isGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
           || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
        {
            col.isTrigger = false;
        }
    }
}
