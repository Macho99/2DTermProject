using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RedPotionItem : ConsumptionItem
{
    const ItemID id = ItemID.RedPotion;
    const int healAmount = 50;
    public RedPotionItem(int amount = 1) : base(id, amount)
    {
    }

    public override object Clone()
    {
        return new RedPotionItem();
    }

    protected override void Effect()
    {
        SubAmount(1);
        FieldSceneFlowController.Player.Heal(healAmount);
    }
}