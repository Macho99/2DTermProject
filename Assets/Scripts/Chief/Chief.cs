using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Customer;

public class Chief : MonoBehaviour
{
    public enum State { Idle, Cook }
    StateMachine<State, Chief> stateMachine;

    Queue<CuisineItem> orderQueue;
    CuisineItem curCuisine;
    Queue<CuisineItem> finishedQueue;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] string curState;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        orderQueue = new Queue<CuisineItem>();
        curCuisine = null;
        finishedQueue = new Queue<CuisineItem>();

        stateMachine = new StateMachine<State, Chief>(this);
    }

    private void Start()
    {
        stateMachine.SetUp(State.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        curState = stateMachine.GetCurStateStr();
    }

    public void SetAnimFloat(string str, float val)
    {
        anim.SetFloat(str, val);
    }
}
