using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject contentsFolder;
    [SerializeField] Text spaceTakenText;
    [SerializeField] Text spaceEmptyText;
    [SerializeField] InvenElement slotPrefab;
    [SerializeField] InvenElement[] slots;
    [SerializeField] Image title;
    [SerializeField] Color equipColor;
    [SerializeField] Color consumpColor;
    [SerializeField] Color ingredientColor;

    private ItemType openInvenType;

    [HideInInspector] public UnityEvent onEquipMenuSelected;
    [HideInInspector] public UnityEvent onConsumpMenuSelected;
    [HideInInspector] public UnityEvent onIngredientMenuSelected;

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
        openInvenType = ItemType.Equip;
        GameManager.Inven.onItemAmountChanged.AddListener(OnItemAmountChange);
        GameManager.Inven.onItemDelete.AddListener(OnItemAmountChange);
    }

    private void OnDestroy()
    {
        GameManager.Inven.onItemAmountChanged.RemoveListener(OnItemAmountChange);
        GameManager.Inven.onItemDelete.RemoveListener(OnItemAmountChange);
    }

    private void OnItemAmountChange(Item item)
    {
        if (item.Type == openInvenType)
        {
            Refresh();
        }
    }

    private void Start()
    {
        EquipMenuSelected(true);
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

        int takenCnt = 0;
        for (int i = 0; i < inv.Length; i++)
        {
            if (inv[i] != null) takenCnt++;
            slots[i].Set(inv[i]);
        }
        for(int i=inv.Length; i< slots.Length; i++)
        {
            slots[i].Invisible();
        }

        spaceTakenText.text = takenCnt.ToString();
        spaceEmptyText.text = inv.Length.ToString();
    }

    private void OnEnable()
    {
        //IngredientMenuSelected(true);
    }

    public void EquipMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Equip);
        onEquipMenuSelected?.Invoke();
    }

    public void ConsumpMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Consump);
        onConsumpMenuSelected?.Invoke();
    }

    public void IngredientMenuSelected(bool val)
    {
        if (val == false) return;
        Refresh(ItemType.Ingredient);
        onIngredientMenuSelected?.Invoke();
    }

    public void SortInv()
    {
        GameManager.Inven.SortInv(openInvenType);
        Refresh();
    }
}