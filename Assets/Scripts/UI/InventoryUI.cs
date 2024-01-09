using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject contentsFolder;
    [SerializeField] InvenElement slotPrefab;
    [SerializeField] InvenElement[] slots;

    private enum OpenInvenType { Equip, Consump, Ingredient };
    private OpenInvenType openInvenType;

    private void Awake()
    {
        int count = GameManager.Inven.MaxInvenSize;
        slots = new InvenElement[count];
        for(int i=0 ; i < count; i++)
        {
            InvenElement elem = Instantiate(slotPrefab, contentsFolder.transform);
            elem.name = $"slot{i.ToString()}";
            slots[i] = elem;
        }
        openInvenType = OpenInvenType.Ingredient;
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

    private void Refresh(OpenInvenType type)
    {
        Item[] inv;

        openInvenType = type;

        switch (openInvenType)
        {
            case OpenInvenType.Equip:
                inv = GameManager.Inven.GetEquipInv();
                break;
            case OpenInvenType.Consump:
                inv = GameManager.Inven.GetConsumpInv();
                break;
            case OpenInvenType.Ingredient:
                inv = GameManager.Inven.GetIngredientInv();
                break;
            default:
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
        Refresh(OpenInvenType.Equip);
    }

    public void ConsumpMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(OpenInvenType.Consump);
    }

    public void IngredientMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(OpenInvenType.Ingredient);
    }
}