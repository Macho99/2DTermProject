using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumptionItem : MultipleItem, ICloneable
{
    private static ItemType type = ItemType.Consump;
    public ConsumptionItem(ItemID id, int amount) : base(id, type, amount)
    {
    }

    public abstract object Clone();

    public override void Use()
    {
        Effect();
    }
    protected abstract void Effect();
}