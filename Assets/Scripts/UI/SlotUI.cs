using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [Serializable]
    public enum HoldSlotType { Slot1 = 1, Slot2 = 2 }

    [SerializeField] private HoldSlotType curHold;
    public HoldSlotType CurHold { get { return curHold; }
        private set { curHold = value; }
    }
    [SerializeField] Color selectSlotColor;
    [SerializeField] Color unselectSlotColor;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Image EquipBackground;
    [SerializeField] Image ConsumpBackground;
    [SerializeField] Color EquipColor;
    [SerializeField] Color ConsumpColor;
    [SerializeField] Color DisableColor;

    [SerializeField] SlotElement[] invSlots;
    [SerializeField] SlotElement[] quickSlots;

    private Item[] slotItems;

    private void Start()
    {
        CurHold = HoldSlotType.Slot1;
        inventoryUI.onEquipMenuSelected.AddListener(OnEquipInvSelect);
        inventoryUI.onConsumpMenuSelected.AddListener(OnConsumpInvSelect);
        inventoryUI.onIngredientMenuSelected.AddListener(OnIngredientInvSelect);
        slotItems = new Item[4];

        for(int i = 0; i < slotItems.Length; i++)
        {
            invSlots[i].Set();
            quickSlots[i].Set();
        }

        FieldSceneFlowController.Instance.onNumPressed.AddListener(OnNumPressed);
    }

    private void OnDestroy()
    {
        FieldSceneFlowController.Instance?.onNumPressed.RemoveListener(OnNumPressed);
    }

    private void OnNumPressed(int num)
    {
        if (num < 1 || num > slotItems.Length)
        {
            Debug.LogError($"{num}번이 눌림");
            return;
        }

        if(num == 1 || num == 2)
        {
            CurHold = (HoldSlotType) (num);
            if (null == slotItems[num - 1])
            {
                FieldSceneFlowController.Player.SetCurWeapon(null);
            }
            else
            {
                FieldSceneFlowController.Player.SetCurWeapon(((WeaponItem)slotItems[num - 1]).weaponObj);
            }
        }

        else
        {
            slotItems[num - 1]?.Use();
        }
    }

    private void OnEquipInvSelect()
    {
        EquipBackground.color = EquipColor;
        ConsumpBackground.color = DisableColor;
    }

    private void OnConsumpInvSelect()
    {
        EquipBackground.color = DisableColor;
        ConsumpBackground.color = ConsumpColor;
    }

    private void OnIngredientInvSelect()
    {
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
        if(item is EquipItem && !(item is WeaponItem))
        {
            return;
        }

        for(int i = 0; i < slotItems.Length; i++)
        {
            if (slotItems[i] == null) continue;
            if (slotItems[i].ID == item.ID)
            {
                invSlots[i].Set();
                quickSlots[i].Set();
                slotItems[i] = null;
                break;
            }
        }

        CheckCurHold(idx, item);
        invSlots[idx].Set(item);
        quickSlots[idx].Set(item);
        slotItems[idx] = item;
    }

    public void OnSlotClick(int idx)
    {
        CheckCurHold(idx, null);
        invSlots[idx].Set();
        quickSlots[idx].Set();
        slotItems[idx] = null;
    }

    public void CheckCurHold(int idx, Item item)
    {
        if (idx + 1 == (int)CurHold)
        {
            if(item == null)
                FieldSceneFlowController.Player.WeaponSlotChanged(null);
            else
                FieldSceneFlowController.Player.WeaponSlotChanged(((WeaponItem)item).weaponObj);
        }
    }
}