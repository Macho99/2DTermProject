using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Alarm Alarm { private get; set; }

    private void Start()
    {
        GameManager.Inven.onItemGet.AddListener(ItemAlarmSet);
    }

    public void ItemAlarmSet(Item item)
    {
        Alarm?.Init(item.Sprite, "아이템 획득!!", item.Name);
    }
}