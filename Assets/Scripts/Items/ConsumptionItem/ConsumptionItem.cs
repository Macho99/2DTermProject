using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumptionItem : MultipleItem
{
    public ConsumptionItem(ItemType type, int amount) : base(type, amount)
    {
    }
    public void Use()
    {
        Effect();
    }
    protected abstract void Effect();
}