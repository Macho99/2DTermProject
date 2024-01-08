using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Item[] items;
    private string[] itemNames;
    private string[] itemDescs;

    private void Awake()
    {
        items = new Item[(int) ItemType.Size];
        itemNames = new string[(int) ItemType.Size];
        itemDescs = new string[(int) ItemType.Size];

        int idx = (int)ItemType.RoosterMeat;
        items[idx] = null;
        itemNames[idx] = "�߰��";
        itemDescs[idx] = "�������� �� ������ ������ �� ����";
    }

    public Item GetItem(ItemType type)
    {
        //��Ÿ ������
        if (null == items[(int)type]){
            return new OtherItem(type);
        }

        return items[(int)type];
    }

    public string GetItemName(ItemType type)
    {
        return itemNames[(int) type];
    }
    public string GetItemDesc(ItemType type)
    {
        return itemDescs[(int) type];
    }
}