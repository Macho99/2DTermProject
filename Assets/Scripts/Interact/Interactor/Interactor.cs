using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    BoxCollider2D col;
    List<Collider2D> contactCols;
    protected IInteractorOwner player;
    Interactable curInteractable;

    bool interacting;

    private void Awake()
    {
        curInteractable = null;
        player = GetComponentInParent<IInteractorOwner>();
        col = GetComponent<BoxCollider2D>();
        contactCols = new List<Collider2D>();
    }

    public void TakeItem(Item item)
    {
        GameManager.Inven.PlayerGetItem(item);
    }

    public bool InteractStart()
    {
        if(curInteractable == null)
            return false;

        curInteractable.InteractStart(this);
        interacting = true;

        return true;
    }

    public void InteractStop()
    {
        curInteractable?.InteractStop(this);
        curInteractable?.UnSelected();
        curInteractable = null;
        interacting = false;
    }

    public void InteractResult(bool result)
    {
        player.ForceInteractStop();
    }

    public bool CanInteract()
    {
        if(contactCols.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Interactable GetClosestInteractable()
    {
        float minSqrDist = 99999f;
        Collider2D minCol = null;
        foreach(var contactCol in contactCols)
        {
            float curSqrDist = (contactCol.transform.position - transform.position).sqrMagnitude;
            if (minSqrDist > curSqrDist)
            {
                minSqrDist = curSqrDist;
                minCol = contactCol;
            }
        }
        Interactable closestInter = minCol?.transform.parent.GetComponent<Interactable>();
        return closestInter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        contactCols.Add(collision);
        if(contactCols.Count == 1)
        {
            _ = StartCoroutine(CoSelectUpdate());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        contactCols.Remove(collision);
        if(interacting == true &&  curInteractable == collision.transform.parent.GetComponent<Interactable>())
        {
            player.ForceInteractStop();
        }
    }

    private IEnumerator CoSelectUpdate()
    {
        while(contactCols.Count > 0)
        {
            Interactable newInter = GetClosestInteractable();
            newInter?.Selected();

            if (curInteractable != null && curInteractable != newInter)
            {
                curInteractable.UnSelected();
            }
            curInteractable = newInter;
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => { return !interacting; });
        }
        curInteractable?.UnSelected();
        curInteractable = null;
    }
}