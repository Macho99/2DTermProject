using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GrilledSkewersItem : CuisineItem
{
    private static readonly ItemID id = ItemID.GrilledSkewers;
    private static new readonly int price = 2;
    public GrilledSkewersItem(int amount = 1) : base(id, amount, price)
    {
        requireList.Add(Tuple.Create(ItemID.ChickMeat, 2));
    }

    public override object Clone()
    {
        return new GrilledSkewersItem();
    }
}