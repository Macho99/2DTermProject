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
    [SerializeField] private float jumpForce = 3f;

    public Vector2 inputVec;

    PlayerState curState;
    Rigidbody2D rb;
    PlayerState[] states;
    Animator anim;

    Collider2D col;

    public bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        states = new PlayerState[(int) PlayerStateType.Size];
        states[0] = new PlayerIdle(this);
        states[1] = new PlayerWalk(this);
        states[2] = new PlayerDuck(this);
        states[3] = new PlayerOnAir(this);
        states[4] = new PlayerOneJump(this);
        states[5] = new PlayerDoubleJump(this);
        states[6] = new PlayerHurt(this);
        states[7] = new PlayerAttack(this);

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
    }

    public void HorizonMove(float value)
    {
        if(rb.velocity.x < -maxSpeed || rb.velocity.x > maxSpeed)
        {
            return;
        }

        rb.AddForce(Vector2.right * value * moveSpeed, ForceMode2D.Force);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Down()
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        int num = col.GetContacts(contactPoints);
        for (int i = 0; i < num; i++)
        {
            if (contactPoints[i].collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                IsGrounded = false;
                SetTriggerTrue();
                break;
            }
        }
    }

    public void SetTriggerTrue()
    {
        if (rb.velocity.y > -0.01f) //상승 중일때만
            col.isTrigger = true;
    }

    public void SetColTriggerFalse()
    {
        col.isTrigger = false;
    }

    public bool CheckCollisioning()
    {
        Collider2D[] cols =  new Collider2D[1];
        return col.GetContacts(cols) < 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            col.isTrigger = false;
        }
    }

}
