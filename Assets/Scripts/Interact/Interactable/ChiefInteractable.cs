using System;
using System.Collections.Generic;
using UnityEngine;

public class ChiefInteractable : Interactable
{
    Chief owner;

    protected virtual void Awake()
    {
        owner = GetComponentInParent<Chief>();
    }

    public override void InteractStart(Interactor interactor)
    {
        base.InteractStart(interactor);
        owner.Interact(interactor);
        InteractSuccess();
    }
}