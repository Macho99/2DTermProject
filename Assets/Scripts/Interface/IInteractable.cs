using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Selected();
    public void UnSelected();
    public void InteractStart(Interactor interactor);
    public void InteractStop(Interactor interactor);
}