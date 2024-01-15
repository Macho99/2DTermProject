using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    private const int maxInvenSize = 20;
    [SerializeField] private EquipItem[] equipInv;
    [SerializeField] private ConsumptionItem[] consumpInv;
    [SerializeField] private IngredientItem[] ingredientInv;

    [HideInInspector] public UnityEvent<Item> onItemGet;
    [HideInInspector] public UnityEvent<Item> onItemDelete;
    [HideInInspector] public UnityEvent<Item> onItemAmountChanged;

    public int MaxInvenSize {  get { return maxInvenSize; } }

    private void Awake()
    {
        onItemGet = new UnityEvent<Item>();
        onItemDelete= new UnityEvent<Item>();
        onItemAmountChanged = new UnityEvent<Item>();

        equipInv = new EquipItem[maxInvenSize];
        consumpInv = new ConsumptionItem[maxInvenSize];
        ingredientInv = new IngredientItem[maxInvenSize];
    }

    private void Start()
    {
        PlayerGetItem(GameManager.Data.GetItem(ItemID.RoosterMeat, 2));
        PlayerGetItem(GameManager.Data.GetItem(ItemID.DuckEgg, 4));
        PlayerGetItem(GameManager.Data.GetItem(ItemID.Sword));
        PlayerGetItem(GameManager.Data.GetItem(ItemID.Bident));
        PlayerGetItem(GameManager.Data.GetItem(ItemID.Bow));
        PlayerGetItem(GameManager.Data.GetItem(ItemID.RedPotion, 10));
    }

    public void PlayerGetItem(Item item)
    {
        if(item is IngredientItem newOtherItem)
        {
            AddMultipleInv(ingredientInv, newOtherItem);
        }
        else if(item is ConsumptionItem newConsumItem)
        {
            AddMultipleInv(consumpInv, newConsumItem);
        }
        else if(item is EquipItem equipItem)
        {
            AddInv(equipInv, equipItem);
            if(equipItem is WeaponItem weaponItem)
            {
                weaponItem.AddWeapon();
            }
        }
    }

    private Item[] GetInv(ItemType invType)
    {
        Item[] inv;
        switch (invType)
        {
            case ItemType.Equip:
                inv = equipInv;
                break;
            case ItemType.Consump:
                inv = consumpInv;
                break;
            case ItemType.Ingredient:
            default:
                inv = ingredientInv;
                break;
        }
        return inv;
    }

    public void Refresh(ItemType invType, int idx, Item item)
    {
        Item[] inv = GetInv(invType);

        if (idx >= inv.Length)
        {
            Debug.LogError($"{idx}는 inv의 최대 길이 밖임");
            return;
        }
        inv[idx] = item;
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
        return consumpInv;
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
            else if (inv[i].ID == newItem.ID)
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

    public void SortInv(ItemType openInvenType)
    {
        Item[] inv = GetInv(openInvenType);
        bool changed = false;
        bool finish = false;

        for(int i = 0; i < inv.Length; i++)
        {
            if(true == finish)
            {
                break;
            }
            if (null != inv[i])
            {
                continue;
            }

            for(int j = i + 1; j < inv.Length; j++)
            {
                if(null != inv[j])
                {
                    Item temp = inv[j];
                    inv[j] = inv[i];
                    inv[i] = temp;
                    changed = true;
                    break;
                }

                if(j == inv.Length - 1)
                {
                    finish = true;
                }
            }
        }

        if (true == changed) return;

        Array.Sort(inv, (a, b) =>
        {
            if (a == null)
            {
                if (b == null)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (b == null)
                    return -1;
                else
                    return string.Compare(a.Name,b.Name);
            }
        });
    }

    public void DeleteItem(ItemType type, int idx)
    {
        Item[] inv = GetInv(type);
        Item item = inv[idx];
        item.DeleteWeapon();
        inv[idx] = null;
        onItemDelete?.Invoke(item);
    }

    public void ItemAmountChanged(Item item)
    {
        if(item is MultipleItem multi)
        {
            if(0 == multi.Amount)
            {
                int idx;
                FindItem(item, out idx);
                if(-1 == idx)
                {
                    Debug.LogError($"인벤토리에 {item.Name}이 없습니다!");
                    return;
                }
                DeleteItem(item.Type, idx);
                return;
            }
        }
        
        onItemAmountChanged?.Invoke(item);
    }

    private void FindItem(Item item, out int idx)
    {
        Item[] inv = GetInv(item.Type);
        idx = -1;
        for (int i = 0; i < inv.Length; i++)
        {
            if (inv[i] == item)
            {
                idx = i;
                return;
            }
        }
    }
}