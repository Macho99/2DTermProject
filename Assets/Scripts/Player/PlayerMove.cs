using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 3f;

    private Player player;
    private SpriteRenderer spRenderer;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;

    public bool isGrounded;
    public bool IsGrounded { 
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    private void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        spRenderer = GetComponentInChildren<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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
        for(int i = 0; i < num; i++) 
        {
            if (contactPoints[i].collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                IsGrounded = false;
                player.SetTriggerTrue();
                break;
            }
        }
    }
}