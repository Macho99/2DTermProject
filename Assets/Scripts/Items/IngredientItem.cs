using System;
using System.Collections.Generic;

[Serializable]
public class IngredientItem : MultipleItem
{
    private static ItemType type = ItemType.Ingredient;
    public IngredientItem(ItemID id, int amount = 1) : base(id, type, amount)
    {

    }
}