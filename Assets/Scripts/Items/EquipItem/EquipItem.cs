using System;
using System.Collections.Generic;

public abstract class EquipItem : Item, ICloneable
{
    private static ItemType type = ItemType.Equip;
    public EquipItem(ItemID id) : base(id, type)
    {
    }

    public abstract object Clone();
}