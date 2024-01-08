﻿using System;
using System.Collections.Generic;

[Serializable]
public abstract class MultipleItem : Item
{
    int amount;
    public int Amount { get { return amount; } }
    protected MultipleItem(ItemType type, int amount) : base(type)
    {
        this.amount = amount;
    }

    public void AddAmount(int num)
    {
        amount += num;
    }

    public bool SubAmount(int num)
    {
        if (amount >= num)
        {
            amount -= num;
            return true;
        }
        return false;
    }
}
