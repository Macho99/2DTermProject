using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    const int maxInvenSize = 20;
    private OtherItem[] otherInv;
    private EquipItem[] equipInv;

    [HideInInspector] public UnityEvent<Item> onItemGet;

    private void Awake()
    {
        onItemGet = new UnityEvent<Item>();
        otherInv = new OtherItem[maxInvenSize];
        equipInv = new EquipItem[maxInvenSize];
    }

    public void GetItem(Item item)
    {
        onItemGet?.Invoke(item);
    }
}
