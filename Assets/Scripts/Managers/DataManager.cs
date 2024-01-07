using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Dictionary<ItemType, string> dictItemName;
    public Dictionary<ItemType, string> dictItemDesc;
    public Dictionary<ItemType, Item> dictItem;

    private void Awake()
    {
        dictItem = new Dictionary<ItemType, Item>();
        dictItemDesc = new Dictionary<ItemType, string>();
        dictItemName = new Dictionary<ItemType, string>();
    }
}