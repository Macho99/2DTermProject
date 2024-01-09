using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public enum ItemType
{
    RoosterMeat,

    Size
}

[Serializable]
public abstract class Item {
    private ItemType type;

    protected Item(ItemType type)
    {
        this.type = type;
    }

    public ItemType Type { get { return type;} }
    public string Name { get { return GameManager.Data.GetItemName(type); } }
    public string Summary { get { return GameManager.Data.GetItemSummary(type); } }
    public string DetailDesc { get { return GameManager.Data.GetItemDetailDesc(type); } }
    public Sprite Sprite { get { return GameManager.Resource.GetItemSprite(type); } }
}