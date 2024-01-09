using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public enum ItemID
{
    RoosterMeat,
    DuckEgg,

    // TODO: 아이템 추가하면 1. DataManager Awake() 추가, 2. Resource 폴더에 sprite 추가
    Size
}

[Serializable]
public enum ItemType { Equip, Consump, Ingredient };

[Serializable]
public abstract class Item {
    private ItemID id;
    private ItemType type;

    protected Item(ItemID id, ItemType type)
    {
        this.id = id;
        this.type = type;
    }

    public ItemID ID { get { return id;} }
    public ItemType Type { get { return type;} }
    public string Name { get { return GameManager.Data.GetItemName(id); } }
    public string Summary { get { return GameManager.Data.GetItemSummary(id); } }
    public string DetailDesc { get { return GameManager.Data.GetItemDetailDesc(id); } }
    public Sprite Sprite { get { return GameManager.Resource.GetItemSprite(id); } }
}