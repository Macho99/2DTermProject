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
    Vector3 initPos;

    Coroutine turnOffCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        initPos = rect.position;
        GameManager.UI.Alarm = this;
    }

    public void Init(Sprite sprite, string infoStr, string textStr, int amount = 1)
    {
        gameObject.SetActive(true);

        image.sprite = sprite;
        upperText.text = infoStr;
        lowerText.text = textStr;

        if (turnOffCoroutine != null)
        {
            StopCoroutine(turnOffCoroutine);
        }

        anim.Play("Enable");
        rect.position = initPos;
        turnOffCoroutine = StartCoroutine(CoTurnOff());
    }

    private IEnumerator CoTurnOff()
    {
        yield return new WaitForSeconds(3f);
        anim.Play("Disable");
        yield return new WaitUntil(() => { return anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"); });
        gameObject.SetActive(false);
    }
}
