using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI upperText;
    [SerializeField] TextMeshProUGUI lowerText;

    Animator anim;
    RectTransform rect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        GameManager.UI.Alarm = this;
    }

    private void OnDestroy()
    {
        GameManager.UI.Alarm = null;
    }

    public void Set(Item item)
    {
        string name;
        if(item is MultipleItem multipleItem)
        {
            name = $"{item.Name} X {multipleItem.Amount}";
        }
        else
        {
            name = item.Name;
        }
        Set(item.Sprite, "æ∆¿Ã≈€ »πµÊ!!", name);
    }

    public void Set(Sprite sprite, string infoStr, string textStr)
    {
        image.sprite = sprite;
        upperText.text = infoStr;
        lowerText.text = textStr;

        anim.Play("Start");
    }
}
