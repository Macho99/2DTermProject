using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image mask;

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
        background.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        owner.onHpChanged.RemoveListener(UIUpdate);
    }

    public void UIUpdate()
    {
        background.gameObject.SetActive(true);
        mask.gameObject.SetActive(true);

        mask.fillAmount = owner.CurHp / owner.MaxHp;

        lastOnTime = Time.time;
        if(null == offCoroutine)
        {
            StartCoroutine(CoOff());
        }
    }

    private IEnumerator CoOff()
    {
        while(lastOnTime + turnOnDuration > Time.time)
        {
            yield return null;
        }
        background.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
    }
}
