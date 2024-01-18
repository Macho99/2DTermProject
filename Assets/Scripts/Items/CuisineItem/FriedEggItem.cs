using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FriedEggItem : CuisineItem
{
    private static readonly ItemID id = ItemID.FriedEgg;
    private static new readonly int price = 200;
    public FriedEggItem(int amount = 1) : base(id, amount, price)
    {
        requireList.Add(Tuple.Create(ItemID.DuckEgg, 1));
    }

    public override object Clone()
    {
        return new FriedEggItem();
    }
}