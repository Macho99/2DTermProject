using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    private const int maxInvenSize = 20;
    [SerializeField] private EquipItem[] equipInv;
    [SerializeField] private ConsumptionItem[] consumInv;
    [SerializeField] private IngredientItem[] ingredientInv;

    [HideInInspector] public UnityEvent<Item> onItemGet;
    public int MaxInvenSize {  get { return maxInvenSize; } }

    private void Awake()
    {
        onItemGet = new UnityEvent<Item>();
        equipInv = new EquipItem[maxInvenSize];
        consumInv = new ConsumptionItem[maxInvenSize];
        ingredientInv = new IngredientItem[maxInvenSize];
    }

    public void PlayerGetItem(Item item)
    {
        if(item is IngredientItem newOtherItem)
        {
            AddMultipleInv(ingredientInv, newOtherItem);
        }
        else if(item is ConsumptionItem newConsumItem)
        {
            AddMultipleInv(consumInv, newConsumItem);
        }
        else if(item is EquipItem equipItem)
        {
            AddInv(equipInv, equipItem);
        }
    }

    public IngredientItem[] GetIngredientInv()
    {
        return ingredientInv;
    }

    public EquipItem[] GetEquipInv()
    {
        return equipInv;
    }
    
    public ConsumptionItem[] GetConsumpInv()
    {
        return consumInv;
    }

    private void AddInv(Item[] inv, Item item)
    {
        int idx = GetEmptySlot(inv);
        if(idx == -1)
        {
            GameManager.UI.InvenFullAlarm();
            return;
        }

        inv[idx] = item;
        onItemGet?.Invoke(item);
    }

    private int GetEmptySlot(Item[] inv)
    {
        int idx = -1;
        for(int i = 0; i < inv.Length; i++) {
            if (inv[i] == null)
            {
                idx = i;
                break;
            }
        }
        return idx;
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

    public void Refresh()
    {

    }
}