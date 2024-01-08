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

    [SerializeField] Animator anim;
    [SerializeField] RectTransform rect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        GameManager.UI.Alarm = this;
    }

    public void Init(Item item)
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
        Init(item.Sprite, "æ∆¿Ã≈€ »πµÊ!!", name);
    }

    public void Init(Sprite sprite, string infoStr, string textStr)
    {
        image.sprite = sprite;
        upperText.text = infoStr;
        lowerText.text = textStr;

        anim.Play("Start");
    }
}
