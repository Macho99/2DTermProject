using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] ParticleSystem moneyParticle;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] public int tableIdx;
    [SerializeField] string curState;

    [HideInInspector] public UnityEvent<Interactor> onInteract;


    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spRenderer;

    public bool IsProperFood { get; set; }
    public CuisineItem SelectedMenu { get; set; }
    public CuisineItem ReceivedMenu { get; set; }
    public float MoveSpeed { get { return moveSpeed; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spRenderer = GetComponentInChildren<SpriteRenderer>();

        onInteract = new UnityEvent<Interactor>();

        stateMachine = new StateMachine<State, Customer>(this);
        stateMachine.AddState(State.Enter, new CustomerEnter(this, stateMachine));
        stateMachine.AddState(State.Choose, new CustomerChoose(this, stateMachine));
        stateMachine.AddState(State.Wait, new CustomerWait(this, stateMachine));
        stateMachine.AddState(State.Eat, new CustomerEat(this, stateMachine));

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

    public void Flip(bool val)
    {
        spRenderer.flipX = val;
    }

    public void SetAnimFloat(string str, float val)
    {
        anim.SetFloat(str, val);
    }

    public void SetAnimBool(string str, bool val)
    {
        anim.SetBool(str, val);
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
                Debug.LogError($"{type.ToString()}에 해당하는 case가 없음");
                break;
        }
    }

    public void SetWaitMaskRatio(float ratio)
    {
        mask.fillAmount = ratio;
    }

    public void Interact(Interactor interactor)
    {
        onInteract?.Invoke(interactor);
    }

    public void PlayerMoneyParticle()
    {
        moneyParticle.Play();
    }
}