using System;
using System.Collections.Generic;

[Serializable]
public class IngredientItem : MultipleItem
{
    public IngredientItem(ItemType type, int amount = 1) : base(type, amount)
    {
    }
}