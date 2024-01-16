using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterDrop : Interactable
{
    [SerializeField] int dropAmount = 1;
    [SerializeField] ItemID dropItemType;
    [SerializeField] int maxInteractCount = 1;
    [SerializeField] int curInteractCount;
    [SerializeField] float interactDuration = 3f;
    [SerializeField] GameObject interactable;

    private Coroutine progressCoroutine;

    private void Awake()
    {
        curInteractCount = maxInteractCount;
    }

    public void EnableInteractable()
    {
        interactable.SetActive(true);
    }

    public override void InteractStart(Interactor interactor)
    {
        base.InteractStart(interactor);
        progressCoroutine = StartCoroutine(CoInteractProgress());
    }

    private IEnumerator CoInteractProgress()
    {
        progressRatio = 0f;
        float curInteractingDuration = 0f;
        while (curInteractingDuration < interactDuration)
        {
            curInteractingDuration += TimeExtension.UnscaledDeltaTime;
            progressRatio = curInteractingDuration / interactDuration;
            onInteractProgress?.Invoke();
            yield return null;
        }

        if (curInteractCount > 0)
        {
            curInteractCount--;
            InteractSuccess();
        }
        else
        {
            InteractFail();
        }

        if (curInteractCount == 0)
        {
            Destroy(gameObject);
        }
    }

    public override void InteractStop(Interactor interactor)
    {
        if (curInteractor == interactor)
        {
            StopCoroutine(progressCoroutine);
            progressRatio = 0f;
            onInteractProgress?.Invoke();
        }
        else
        {
            Debug.LogError($"상호작용을 시작한 interactor: {curInteractor.name} \n멈추는 interactor: {interactor.name}");
        }
        base.InteractStop(interactor);
    }

    public override void InteractSuccess()
    {
        curInteractor.TakeItem(GameManager.Data.GetItem(dropItemType, dropAmount));
        base.InteractSuccess();
    }
}
