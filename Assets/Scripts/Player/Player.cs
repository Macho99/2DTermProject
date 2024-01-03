using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] string curStateStr;


    public Vector2 inputVec;

    PlayerMove playerMove;

    PlayerState curState;
    Rigidbody2D rb;
    PlayerState[] states;
    Animator anim;

    Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
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

    public PlayerMove GetMove()
    {
        return playerMove;
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
