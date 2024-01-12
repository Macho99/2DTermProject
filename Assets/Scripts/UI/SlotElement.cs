using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotElement : MonoBehaviour, IDropHandler
{
    [SerializeField] ItemType slotType;
    [SerializeField] int slotNum;
    [SerializeField] Image image;
    [SerializeField] Text amountText;
    
    private SlotUI slotUI;
    private Item curItem;
    private int amount;


    private void Awake()
    {
        if(slotNum == 0)
        {
            print("슬롯 번호는 0일 수 없습니다!");
        }
        slotUI = GetComponentInParent<SlotUI>();
    }

    public void Set(Item item = null)
    {
        if (item == null)
        {
            amount = 0;
            image.sprite = null;
            return;
        }

        curItem = item;
        
        if(item is MultipleItem multi)
        {
            amount = multi.Amount;
            amountText.text = amount.ToString();
        }
        else
        {
            amountText.text = "";
        }
        image.sprite = item.Sprite;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<InvenElement>().CurItem;
        if (item == null || slotType != item.Type)
        {
            return;
        }

        slotUI.OnItemDrop(slotNum - 1, item);
    }
}
