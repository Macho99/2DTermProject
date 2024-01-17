using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomerInteractable : Interactable
{
    Customer owner;

    protected virtual void Awake()
    {
        owner = GetComponentInParent<Customer>();
    }

    public override void InteractStart(Interactor interactor)
    {
        base.InteractStart(interactor);
        owner.Interact(interactor);
    }
}