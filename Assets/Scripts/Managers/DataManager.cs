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
        itemNames[idx] = "닭고기";
        itemDescs[idx] = "질기지만 잘 익히면 맛있을 것 같다";
    }

    public Item GetItem(ItemType type)
    {
        //기타 아이템
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