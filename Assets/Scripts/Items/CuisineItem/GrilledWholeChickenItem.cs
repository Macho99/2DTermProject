using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GrilledWholeChickenItem : CuisineItem
{
    private static readonly ItemID id = ItemID.GrilledWholeChicken;
    private static new readonly int price = 300;
    public GrilledWholeChickenItem(int amount = 1) : base(id, amount, price)
    {
        requireList.Add(Tuple.Create(ItemID.RoosterMeat, 1));
    }

    public override object Clone()
    {
        return new GrilledWholeChickenItem();
    }
}