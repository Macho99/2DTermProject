
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BasicSoupItem : CuisineItem
{
    private static readonly ItemID id = ItemID.BasicSoup;
    private static new readonly int price = 1;
    public BasicSoupItem(int amount = 1) : base(id, amount, price)
    {
    }

    public override object Clone()
    {
        return new BasicSoupItem();
    }
}
