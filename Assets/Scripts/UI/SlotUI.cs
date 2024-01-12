using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Image EquipBackground;
    [SerializeField] Image ConsumpBackground;
    [SerializeField] Color EquipColor;
    [SerializeField] Color ConsumpColor;
    [SerializeField] Color DisableColor;

    [SerializeField] SlotElement[] invSlots;
    [SerializeField] SlotElement[] quickSlots;

    private Item[] slotItems;

    private ItemType curSelectedInv;

    private void Start()
    {
        inventoryUI.onEquipMenuSelected.AddListener(OnEquipInvSelect);
        inventoryUI.onConsumpMenuSelected.AddListener(OnConsumpInvSelect);
        inventoryUI.onIngredientMenuSelected.AddListener(OnIngredientInvSelect);
        slotItems = new Item[4];

        for(int i = 0; i < slotItems.Length; i++)
        {
            invSlots[i].Set();
            quickSlots[i].Set();
        }
    }

    private void OnEquipInvSelect()
    {
        curSelectedInv = ItemType.Equip;
        EquipBackground.color = EquipColor;
        ConsumpBackground.color = DisableColor;
    }

    private void OnConsumpInvSelect()
    {
        curSelectedInv = ItemType.Consump;
        EquipBackground.color = DisableColor;
        ConsumpBackground.color = ConsumpColor;
    }

    private void OnIngredientInvSelect()
    {
        curSelectedInv = ItemType.Ingredient;
        EquipBackground.color = DisableColor;
        ConsumpBackground.color = DisableColor;
    }

    public void OnInvClose()
    {
        EquipBackground.gameObject.SetActive(false);
        ConsumpBackground.gameObject.SetActive(false);
    }

    public void OnInvOpen()
    {
        EquipBackground.gameObject.SetActive(true);
        ConsumpBackground.gameObject.SetActive(true);
    }

    public void OnItemDrop(int idx, Item item)
    {
        int i;
        for(i = 0; i < slotItems.Length; i++)
        {
            if (slotItems[i] == item)
            {
                invSlots[i].Set();
                quickSlots[i].Set();
                break;
            }
        }


        invSlots[idx].Set(item);
        quickSlots[idx].Set(item);
    }
}
