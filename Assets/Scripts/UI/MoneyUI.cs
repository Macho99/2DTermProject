using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text text;

    private void Start()
    {
        GameManager.Inven.onMoneyChanged.AddListener(UIUpdate);
        UIUpdate();
    }

    private void OnDestroy()
    {
        GameManager.Inven.onMoneyChanged.RemoveListener(UIUpdate);
    }

    private void UIUpdate()
    {
        text.text = GameManager.Inven.Money.ToString();
    }
}
