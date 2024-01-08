using System;
using System.Collections.Generic;

[Serializable]
public class OtherItem : MultipleItem
{
    public OtherItem(ItemType type, int amount = 1) : base(type, amount)
    {
    }
}