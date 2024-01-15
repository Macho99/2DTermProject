using System.Collections.Generic;

public class SwordItem : WeaponItem
{
    const ItemID id = ItemID.Sword;
    public SwordItem() : base(id)
    {
    }

    public override object Clone()
    {
        return new SwordItem();
    }
}