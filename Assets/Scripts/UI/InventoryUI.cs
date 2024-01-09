using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject contentsFolder;
    [SerializeField] InvenElement slotPrefab;
    [SerializeField] InvenElement[] slots;
    [SerializeField] Image title;
    [SerializeField] Color equipColor;
    [SerializeField] Color consumpColor;
    [SerializeField] Color ingredientColor;

    private ItemType openInvenType;

    private void Awake()
    {
        int count = GameManager.Inven.MaxInvenSize;
        slots = new InvenElement[count];
        for(int i=0 ; i < count; i++)
        {
            InvenElement elem = Instantiate(slotPrefab, contentsFolder.transform);
            elem.name = $"slot{i.ToString()}";
            elem.Idx = i;
            elem.InventoryUI = this;
            slots[i] = elem;
        }
        openInvenType = ItemType.Ingredient;
    }

    private void Start()
    {
        IngredientMenuSelected(true);
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        Refresh(openInvenType);
    }

    private void Refresh(ItemType type)
    {
        Item[] inv;

        openInvenType = type;

        switch (openInvenType)
        {
            case ItemType.Equip:
                title.color = equipColor;
                inv = GameManager.Inven.GetEquipInv();
                break;
            case ItemType.Consump:
                title.color = consumpColor;
                inv = GameManager.Inven.GetConsumpInv();
                break;
            case ItemType.Ingredient:
            default:
                title.color = ingredientColor;
                inv = GameManager.Inven.GetIngredientInv();
                break;
        }

        for (int i = 0; i < inv.Length; i++)
        {
            slots[i].Set(inv[i]);
        }
        for(int i=inv.Length; i< slots.Length; i++)
        {
            slots[i].Invisible();
        }
    }

    private void OnEnable()
    {
        //IngredientMenuSelected(true);
    }

    public void EquipMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Equip);
    }

    public void ConsumpMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Consump);
    }

    public void IngredientMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Ingredient);
    }
}