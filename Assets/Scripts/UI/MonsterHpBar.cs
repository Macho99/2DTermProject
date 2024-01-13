using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image greenMask;
    [SerializeField] Image redMask;
    [SerializeField] float lerpSpeed = 5f;

    private Monster owner;
    private Coroutine offCoroutine;
    private float lastOnTime;
    private float turnOnDuration;

    private void Awake()
    {
        owner = transform.parent.parent.GetComponent<Monster>();
        turnOnDuration = Constants.HpBarDuration;
    }

    private void OnEnable()
    {
        owner.onHpChanged.AddListener(UIUpdate);
        SetVisible(false);
    }

    private void OnDisable()
    {
        owner.onHpChanged.RemoveListener(UIUpdate);
    }

    public void UIUpdate()
    {
        SetVisible(true);

        greenMask.fillAmount = owner.CurHp / owner.MaxHp;
        lastOnTime = Time.time;
        StopAllCoroutines();

        if (owner.CurHp <= 0)
        {
            SetVisible(false);
            return;
        }
        
        StartCoroutine(CoOff());
    }

    private IEnumerator CoOff()
    {
        yield return new WaitForSeconds(1f);

        while(Mathf.Abs(greenMask.fillAmount - redMask.fillAmount) > 0.0001f)
        {
            redMask.fillAmount = Mathf.Lerp(greenMask.fillAmount, redMask.fillAmount, 1 - Time.deltaTime * lerpSpeed);
            yield return null;
        }

        while(Time.time < lastOnTime + turnOnDuration)
        {
            yield return null;
        }

        SetVisible(false);
    }

    private void SetVisible(bool val)
    {
        background.gameObject.SetActive(val);
        greenMask.gameObject.SetActive(val);
        redMask.gameObject.SetActive(val);
    }
}
