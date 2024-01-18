using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldResultUI : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] Text huntResultText;
    [SerializeField] HorizontalLayoutGroup panelPrefab;

    public void Init()
    {
        gameObject.SetActive(true);
        huntResultText.text = $"{FieldSceneFlowController.Instance.KillCnt.ToString()}마리 사냥";

        IngredientItem[] items = GameManager.Inven.GetIngredientInv();
        for (int i = 0; i < items.Length; i++)
        {
            GameObject panel = Instantiate(panelPrefab, content.transform).gameObject;
            Image image = panel.GetComponentInChildren<Image>();
            Text text = panel.GetComponentInChildren<Text>();
            image.sprite = items[i].Sprite;
            text.text = $"{items[i].Name} X {items[i].Amount}";
        }
    }
}
