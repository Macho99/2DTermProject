using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Alarm Alarm { private get; set; }

    public ItemInfoUI ItemInfo { get; set; }
    public DragInfoUI DragInfo { get; set; }

    private void Start()
    {
        GameManager.Inven.onItemGet.AddListener(ItemAlarmSet);
    }

    public void ItemAlarmSet(Item item)
    {
        Alarm?.Set(item);
    }

    public void InfoAlarmSet(Sprite sprite, string upperStr, string lowerStr)
    {
        Alarm?.Set(sprite, upperStr, lowerStr);
    }

    public void InvenFullAlarm()
    {
        InfoAlarmSet(GameManager.Resource.spriteDict["Bag"], "습득 불가", "아이템 가방이 꽉 찼습니다..");
    }
}