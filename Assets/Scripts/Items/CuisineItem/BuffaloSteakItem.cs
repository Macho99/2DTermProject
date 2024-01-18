using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuffaloSteakItem : CuisineItem
{
    private static readonly ItemID id = ItemID.BuffaloSteak;
    private static new readonly int price = 1000;
    public BuffaloSteakItem(int amount = 1) : base(id, amount, price)
    {
        requireList.Add(Tuple.Create(ItemID.BuffaloMeat, 2));
    }

    public override object Clone()
    {
        return new BuffaloSteakItem();
    }
}
