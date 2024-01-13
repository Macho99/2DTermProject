using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler,
    IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Item curItem;
    private Image image;
    private TextMeshProUGUI text;
    private Color transparencyColor;
    private Color normalColor;

    public InventoryUI InventoryUI { get; set; }
    public int Idx { get; set; }
    public Item CurItem { get { return curItem; } }

    private void Awake()
    {
        transparencyColor = Color.white;
        transparencyColor.a = 0f;
        normalColor = Color.white;
        image = transform.GetChild(0).GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Set(Item item = null)
    {
        gameObject.SetActive(true);
        curItem = item;
        if(curItem == null)
        {
            image.sprite = null;
            image.color = transparencyColor;
            text.text = "";
        }
        else
        {
            image.sprite = curItem.Sprite;
            image.color = normalColor;
            if(curItem is MultipleItem multipleItem)
            {
                text.text = multipleItem.Amount.ToString();
            }
            else
            {
                text.text = "";
            }
        }
    }

    public void Invisible()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (curItem == null) return;
        GameManager.UI.ItemInfo.Set(curItem);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (curItem == null) return;
        Vector2 pos = eventData.position;
        pos.x += 10f;
        GameManager.UI.ItemInfo.Move(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (curItem == null) return;
        GameManager.UI.ItemInfo.Set(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (curItem == null) return;
        GameManager.UI.DragInfo.Set(curItem);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.UI.DragInfo.InActive();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (curItem == null) return;
        GameManager.UI.DragInfo.Move(eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent<InvenElement>(out InvenElement otherElem))
        {
            if (null == otherElem.curItem) return;
            if(otherElem == this) return;

            ItemType type = otherElem.curItem.Type;

            Item temp = otherElem.curItem;
            otherElem.curItem = this.curItem;
            this.curItem = temp;
            GameManager.Inven.Refresh(type, otherElem.Idx, otherElem.curItem);
            GameManager.Inven.Refresh(type, this.Idx, this.curItem);
            InventoryUI.Refresh();
            OnPointerEnter(null);
        }
    }
}