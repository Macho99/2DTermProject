using System;
using System.Collections.Generic;

public abstract class CuisineItem : MultipleItem, ICloneable
{
    private static readonly ItemType type = ItemType.Cuisine;
    protected List<Tuple<ItemID, int>> requireList;
    public readonly int price;

    protected CuisineItem(ItemID id, int amount, int cost) : base(id, type, amount)
    {
        requireList = new List<Tuple<ItemID, int>>();
        this.price = cost;
    }

    public abstract object Clone();
}
