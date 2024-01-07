using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterDrop : MonoBehaviour, IInteractable
{
    [SerializeField] int dropAmount = 1;
    //[SerializeField] int dropItemType;
    [SerializeField] int maxInteractCount = 1;
    [SerializeField] int curInteractCount;
    [SerializeField] float interactDuration = 3f;
    [SerializeField] GameObject interactable;

    [HideInInspector] public UnityEvent onInteractorEnter;
    [HideInInspector] public UnityEvent onInteractorExit;
    [HideInInspector] public UnityEvent onSelected;
    [HideInInspector] public UnityEvent onUnSelected;
    [HideInInspector] public UnityEvent onInteractProgress;
    [HideInInspector] public float progressRatio;

    private Coroutine progressCoroutine;
    private Interactor curInteractor;

    private void Awake()
    {
        curInteractCount = maxInteractCount;
    }

    public void EnableInteractable()
    {
        interactable.SetActive(true);
    }

    private IEnumerator CoInteractProgress()
    {
        progressRatio = 0f;
        float curInteractingDuration = 0f;
        while(curInteractingDuration < interactDuration)
        {
            curInteractingDuration += Time.unscaledDeltaTime;
            progressRatio = curInteractingDuration / interactDuration;
            onInteractProgress?.Invoke();
            yield return null;
        }

        if(curInteractCount > 0)
        {
            curInteractCount--;
            InteractSuccess(curInteractor);
        }
        else
        {
            InteractFail(curInteractor);
        }

        if(curInteractCount == 0)
        {
            Destroy(gameObject);
        }
    }

    public void InteractStart(Interactor interactor)
    {
        curInteractor = interactor;
        progressCoroutine = StartCoroutine(CoInteractProgress());
    }

    public void InteractStop(Interactor interactor)
    {
        if(curInteractor == interactor)
        {
            StopCoroutine(progressCoroutine);
            progressRatio = 0f;
            onInteractProgress?.Invoke();
        }
        else
        {
            Debug.LogError("상호작용을 시작한 interactor와 멈추는 interactor가 다릅니다");
        }
    }

    public void InteractSuccess(Interactor interactor)
    {
        interactor.InteractResult(true);
    }

    public void InteractFail(Interactor interactor)
    {
        interactor.InteractResult(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Interactor>(out _))
        {
            onInteractorEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Interactor>(out _))
        {
            onInteractorExit?.Invoke();
        }
    }

    public void Selected()
    {
        onSelected?.Invoke();
    }

    public void UnSelected()
    {
        onUnSelected?.Invoke();
    }
}
