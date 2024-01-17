using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Customer;

public class Chief : MonoBehaviour
{
    public enum State { Idle, Cook }
    StateMachine<State, Chief> stateMachine;

    Queue<CuisineItem> orderQueue;
    Queue<CuisineItem> finishedQueue;

    public CuisineItem CurCuisine { get; set; }
    public int OrderCount { get { return orderQueue.Count; } }

    [Space(20)]
    [Header("Debug")]
    [SerializeField] string curState;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        orderQueue = new Queue<CuisineItem>();
        CurCuisine = null;
        finishedQueue = new Queue<CuisineItem>();

        stateMachine = new StateMachine<State, Chief>(this);
        stateMachine.AddState(State.Idle, new ChiefIdle(this, stateMachine));
        stateMachine.AddState(State.Cook, new ChiefCook(this, stateMachine));
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

    public void OrderEnqueue(CuisineItem cuisine)
    {
        orderQueue.Enqueue(cuisine);
    }

    public CuisineItem OrderDequeue()
    {
        return orderQueue.Dequeue();
    }

    public void FinishedEnqueue(CuisineItem cuisine)
    {
        finishedQueue.Enqueue(cuisine);
    }

    public CuisineItem FinishedDequeue()
    {
        return finishedQueue.Dequeue();
    }
}
