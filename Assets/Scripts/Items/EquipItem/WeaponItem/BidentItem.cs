using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BidentItem : WeaponItem
{
    const ItemID id = ItemID.Bident;
    public BidentItem() : base(id)
    {
    }

    public override object Clone()
    {
        return new BidentItem();
    }
}