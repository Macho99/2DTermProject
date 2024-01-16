using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public UnityEvent onInteractorEnter;
    [HideInInspector] public UnityEvent onInteractorExit;
    [HideInInspector] public UnityEvent onSelected;
    [HideInInspector] public UnityEvent onUnSelected;
    [HideInInspector] public UnityEvent onInteractProgress;
    [HideInInspector] public float progressRatio;

    protected Interactor curInteractor;

    public virtual void InteractStart(Interactor interactor)
    {
        curInteractor = interactor;
    }

    public virtual void InteractStop(Interactor interactor)
    {
        curInteractor = null;
    }

    public virtual void InteractSuccess()
    {
        curInteractor.InteractResult(true);
        curInteractor = null;
    }

    public virtual void InteractFail()
    {
        curInteractor.InteractResult(false);
        curInteractor = null;
    }

    public void Selected()
    {
        onSelected?.Invoke();
    }

    public void UnSelected()
    {
        onUnSelected?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactor>(out _))
        {
            onInteractorEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactor>(out _))
        {
            onInteractorExit?.Invoke();
        }
    }
}