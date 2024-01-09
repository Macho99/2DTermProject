using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfoUI : MonoBehaviour
{
    protected RectTransform rect;

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public void Move(Vector3 pos)
    {
        rect.position = pos;
    }

    public abstract void Set(Item item);

    public void InActive()
    {
        gameObject.SetActive(false);
    }
}
