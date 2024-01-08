using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    const int maxInvenSize = 20;
    [SerializeField] private OtherItem[] otherInv;
    [SerializeField] private EquipItem[] equipInv;

    [HideInInspector] public UnityEvent<Item> onItemGet;

    private void Awake()
    {
        onItemGet = new UnityEvent<Item>();
        otherInv = new OtherItem[maxInvenSize];
        equipInv = new EquipItem[maxInvenSize];
    }

    public void GetItem(Item item)
    {
        if(item is OtherItem newItem)
        {
            AddMultipleInv(otherInv, newItem);
        }
        onItemGet?.Invoke(item);
    }

    private void AddMultipleInv(MultipleItem[] inv, MultipleItem newItem)
    {
        int emptyIdx = inv.Length;
        bool find = false;
        for (int i = 0; i < inv.Length; i++)
        {
            if (null == inv[i])
                emptyIdx = Mathf.Min(i, emptyIdx);
            else if (inv[i].Type == newItem.Type)
            {
                find = true;
                inv[i].AddAmount(newItem.Amount);
                break;
            }
        }

        if (false == find)
        {
            if (inv.Length == emptyIdx)
            {
                GameManager.UI.InvenFullAlarm();
                return;
            }

            inv[emptyIdx] = newItem;
        }
        onItemGet?.Invoke(newItem);
    }
}
