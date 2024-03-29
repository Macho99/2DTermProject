using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RestaurantPlayer : MonoBehaviour, IInteractorOwner
{
    //[SerializeField] private float moveAccel = 1000f;
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private SpriteRenderer handRenderer;

    private Rigidbody2D rb;
    private Vector2 inputVec;
    private Animator anim;
    private Interactor interactor;

    private CuisineItem handedCuisine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        interactor = GetComponentInChildren<Interactor>();
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();

        if(inputVec.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(inputVec.x > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void OnInteract(InputValue value)
    {
        if(value.isPressed == true)
        {
            interactor.InteractStart();
        }
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    private void Move()
    {
        rb.velocity = inputVec * maxMoveSpeed;
    }

    public void ForceInteractStop()
    {

    }

    public CuisineItem GiveCuisine()
    {
        CuisineItem result = handedCuisine;
        handedCuisine = null;
        handRenderer.sprite = null;
        return result;
    }

    public void ReceiveCuisine(CuisineItem item)
    {
        handedCuisine = item;
        handRenderer.sprite = item.Sprite;
    }
}
