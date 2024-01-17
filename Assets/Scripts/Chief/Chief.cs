using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Customer;

public class Chief : MonoBehaviour
{
    public enum State { Idle, Cook }
    StateMachine<State, Chief> stateMachine;

    [SerializeField] GameObject stateViewer;
    [SerializeField] Image mask;
    [SerializeField] Image stateImage;
    [SerializeField] Canvas orderCanvas;
    [SerializeField] GameObject finishObj;

    Queue<CuisineItem> orderQueue;
    Queue<CuisineItem> finishQueue;
    Image[] orderImages;
    Image[] orderBacks;
    SpriteRenderer[] finishRenderers;

    public CuisineItem CurCuisine { get; set; }
    public int OrderCount { get { return orderQueue.Count; } }

    [Space(20)]
    [Header("Debug")]
    [SerializeField] string curState;

    Animator anim;
    SpriteRenderer spRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spRenderer = GetComponentInChildren<SpriteRenderer>();

        orderQueue = new Queue<CuisineItem>();
        finishQueue = new Queue<CuisineItem>();
        CurCuisine = null;

        Image[] temp = orderCanvas.GetComponentsInChildren<Image>();
        orderImages = new Image[temp.Length / 2];
        orderBacks = new Image[temp.Length / 2];
        for (int i = 0; i < temp.Length; i++)
        {
            if(i % 2 == 1)
            {
                orderImages[i/2] = temp[i];
            }
            else
            {
                orderBacks[i/2] = temp[i];
            }
        }
        
        finishRenderers = finishObj.GetComponentsInChildren<SpriteRenderer>();

        foreach (Image image in orderBacks)
        {
            image.gameObject.SetActive(false);
        }
        foreach(SpriteRenderer sp in finishRenderers)
        {
            sp.gameObject.SetActive(false);
        }

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

    public void OrderEnqueue(CuisineItem cuisine)
    {
        orderQueue.Enqueue(cuisine);

        int idx = orderQueue.Count - 1;
        orderImages[idx].sprite = cuisine.Sprite;
        orderBacks[idx].gameObject.SetActive(true);
    }

    public CuisineItem OrderDequeue()
    {
        CuisineItem result = orderQueue.Dequeue();

        int idx = orderQueue.Count;
        orderImages[idx].sprite = null;
        orderBacks[idx].gameObject.SetActive(false);

        int i = 0;
        foreach(var item in orderQueue)
        {
            orderImages[i].sprite = item.Sprite;
            i++;
        }

        return result;
    }

    public void FinishedEnqueue(CuisineItem cuisine)
    {
        finishQueue.Enqueue(cuisine);

        int idx = finishQueue.Count - 1;
        finishRenderers[idx].sprite = cuisine.Sprite;
        finishRenderers[idx].gameObject.SetActive(true);
    }

    public CuisineItem FinishedDequeue()
    {
        CuisineItem result = finishQueue.Dequeue();

        int idx = finishQueue.Count;
        finishRenderers[idx].sprite = null;
        finishRenderers[idx].gameObject.SetActive(false);

        int i = 0;
        foreach (var item in finishQueue)
        {
            finishRenderers[i].sprite = item.Sprite;
            i++;
        }

        return result;
    }

    public void Interact(Interactor interactor)
    {
        if(finishQueue.Count == 0)
        {
            print("요리 미완성");
        }
        else
        {
            RestauInteractor restauInteractor = (RestauInteractor)interactor;
            restauInteractor.ReceiveCuisine(FinishedDequeue());
        }
    }

    public void SetStateViewActive(bool val)
    {
        stateViewer.SetActive(val);
    }

    public void SetStateSprite()
    {
        stateImage.sprite = CurCuisine.Sprite;
    }

    public void SetCookTimeMaskRatio(float ratio)
    {
        mask.fillAmount = ratio;
    }
}
