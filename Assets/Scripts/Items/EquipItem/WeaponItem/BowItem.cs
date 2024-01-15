using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BowItem : WeaponItem
{
    const ItemID id = ItemID.Bow;
    public BowItem() : base(id)
    {
    }

    public override object Clone()
    {
        return new BowItem();
    }
}
