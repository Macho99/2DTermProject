using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public enum ViewerState { Choose, Menu, Happy, Angry }
    public enum State { Enter, Choose, Wait, Eat, Exit }
    StateMachine<State, Customer> stateMachine;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] GameObject stateViewer;
    [SerializeField] Image mask;
    [SerializeField] Image stateImage;
    [SerializeField] Sprite chooseSprite;
    [SerializeField] Sprite happySprite;
    [SerializeField] Sprite angrySprite;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] public int tableIdx;
    [SerializeField] string curState;

    public CuisineItem SelectedMenu { get; set; }

    Rigidbody2D rb;
    Animator anim;

    public float MoveSpeed { get { return moveSpeed; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        stateMachine = new StateMachine<State, Customer>(this);
        stateMachine.AddState(State.Enter, new CustomerEnter(this, stateMachine));
        stateMachine.AddState(State.Choose, new CustomerChoose(this, stateMachine));
        stateMachine.AddState(State.Wait, new CustomerWait(this, stateMachine));


        stateMachine.AddState(State.Exit, new CustomerExit(this, stateMachine));
    }

    private void Start()
    {
        stateMachine.SetUp(State.Enter);
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

    public void SetVel(Vector2 vel)
    {
        rb.velocity = vel;
    }

    public void SetStateViewActive(bool val)
    {
        stateViewer.SetActive(val);
    }

    public void SetStateViewSprite(ViewerState type)
    {
        switch (type)
        {
            case ViewerState.Choose:
                stateImage.sprite = chooseSprite;
                break;
            case ViewerState.Menu:
                stateImage.sprite = SelectedMenu.Sprite;
                break;
            case ViewerState.Happy:
                stateImage.sprite = happySprite;
                break;
            case ViewerState.Angry:
                stateImage.sprite = angrySprite;
                break;
            default:
                Debug.LogError($"{type.ToString()}�� �ش��ϴ� case�� ����");
                break;
        }
    }

    public void SetWaitMaskRatio(float ratio)
    {
        mask.fillAmount = ratio;
    }
}