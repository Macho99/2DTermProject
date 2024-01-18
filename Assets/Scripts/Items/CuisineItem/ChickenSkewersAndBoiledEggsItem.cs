using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChickenSkewersAndBoiledEggsItem : CuisineItem
{
    private static readonly ItemID id = ItemID.ChickenSkewersAndBoiledEggs;
    private static new readonly int price = 700;
    public ChickenSkewersAndBoiledEggsItem(int amount = 1) : base(id, amount, price)
    {
        requireList.Add(Tuple.Create(ItemID.ChickMeat, 1));
        requireList.Add(Tuple.Create(ItemID.DuckEgg, 1));
    }

    public override object Clone()
    {
        return new ChickenSkewersAndBoiledEggsItem();
    }
}