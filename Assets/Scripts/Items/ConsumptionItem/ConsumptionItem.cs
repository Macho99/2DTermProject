using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumptionItem : MultipleItem
{
    private static ItemType type = ItemType.Consump;
    public ConsumptionItem(ItemID id, int amount) : base(id, type, amount)
    {
    }
    public void Use()
    {
        Effect();
    }
    protected abstract void Effect();
}