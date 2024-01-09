using System.Collections.Generic;

public abstract class EquipItem : Item
{
    private static ItemType type = ItemType.Equip;
    public EquipItem(ItemID id) : base(id, type)
    {
    }
}