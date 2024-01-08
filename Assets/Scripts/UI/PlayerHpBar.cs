using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] float changeSpeed = 5f;
    [SerializeField] TextMeshProUGUI curHpText;
    [SerializeField] TextMeshProUGUI maxHpText;
    [SerializeField] Image fillImage;
    [SerializeField] Color maxColor;
    [SerializeField] Color minColor;
    Slider slider;

    Player player;

    int curHp;
    int maxHp;
    float ratio;
    Coroutine hpChangeCoroutine;
    private void Awake()
    {
        player = FieldSceneFlowController.Player;
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        player.onHpChanged.AddListener(UIUpdate);
    }

    private void OnDisable()
    {
        player.onHpChanged.RemoveListener(UIUpdate);
    }

    public void UIUpdate()
    {
        curHp = player.CurHp;
        maxHp = player.MaxHp;

        ratio = ((float)curHp) / maxHp;
        fillImage.color = Color.Lerp(minColor, maxColor, ratio);

        curHpText.text = curHp.ToString();
        maxHpText.text = maxHp.ToString();



        if(null == hpChangeCoroutine)
        {
            StartCoroutine(CoHpChange());
        }
    }

    private IEnumerator CoHpChange()
    {
        while (Mathf.Abs(ratio - slider.value) > 0.01f)
        {
            float diff = Mathf.Abs(ratio - slider.value);
            float amount = Mathf.Lerp(0, diff, Time.unscaledDeltaTime * changeSpeed);
            //hp가 줄었을 때
            if (ratio < slider.value)
            {
                slider.value -= amount;
            }
            //hp를 회복했을 때
            else
            {
                slider.value += amount;
            }
            yield return null;
        }
    }
}
