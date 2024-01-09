using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Item[] items;
    private string[] itemNames;
    private string[] itemSummary;
    private string[] itemDetailDesc;

    private void Awake()
    {
        items = new Item[(int) ItemType.Size];
        itemNames = new string[(int) ItemType.Size];
        itemSummary = new string[(int) ItemType.Size];
        itemDetailDesc = new string[(int) ItemType.Size];

        int idx = (int)ItemType.RoosterMeat;
        items[idx] = null;
        itemNames[idx] = "�߰��";
        itemSummary[idx] = "�丮���";
        itemDetailDesc[idx] = "�̰ɷ� ��Ʃ, ���� ���� ���� �� ���� �� ����";
    }

    public Item GetItem(ItemType type, int amount = 1)
    {
        //��Ÿ ������
        if (null == items[(int)type]){
            return new IngredientItem(type, amount);
        }

        return items[(int)type];
    }

    public string GetItemName(ItemType type)
    {
        return itemNames[(int) type];
    }
    public string GetItemSummary(ItemType type)
    {
        return itemSummary[(int) type];
    }

    public string GetItemDetailDesc(ItemType type)
    {
        return itemDetailDesc[(int) type];
    }
}