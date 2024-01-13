using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteItemUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color activeColor;
    [SerializeField] Image coloringImage1;
    [SerializeField] Image coloringImage2;

    Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent<InvenElement>(out InvenElement elem))
        {
            if (elem.CurItem == null) return;
            GameManager.Inven.DeleteItem(elem.CurItem.Type, elem.Idx);
            elem.InventoryUI.Refresh();

            coloringImage1.color = Color.white;
            coloringImage2.color = Color.white;
            text.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<InvenElement>(out InvenElement elem))
        {
            if (elem.CurItem == null) return;

            coloringImage1.color = activeColor;
            coloringImage2.color = activeColor;
            text.color = activeColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<InvenElement>(out InvenElement elem))
        {
            if (elem.CurItem == null) return;


            coloringImage1.color = Color.white;
            coloringImage2.color = Color.white;
            text.color = Color.white;
        }
    }
}
