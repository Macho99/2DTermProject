using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragInfoUI : InfoUI
{
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        GameManager.UI.DragInfo = this;
    }

    public override void Set(Item item = null)
    {
        if(item == null)
        {
            InActive();
        }
        image.sprite = item.Sprite;
        gameObject.SetActive(true);
    }
}
