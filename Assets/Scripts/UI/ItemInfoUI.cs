using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : InfoUI
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI summary;
    [SerializeField] private TextMeshProUGUI detailDesc;

    protected override void Awake()
    {
        base.Awake();
        GameManager.UI.ItemInfo = this;
    }

    private void OnDestroy()
    {
        GameManager.UI.ItemInfo = null;
    }

    public override void Set(Item item = null)
    {
        if(item == null)
        {
            InActive();
            return;
        }

        else
        {
            image.sprite = item.Sprite;
            itemName.text = item.Name;
            summary.text = item.Summary;
            detailDesc.text = item.DetailDesc;

            gameObject.SetActive(true);
        }
    }
}