using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image mask;

    private FieldPlayer owner;

    private void Awake()
    {
        owner = transform.parent.parent.GetComponent<FieldPlayer>();
    }

    private void OnEnable()
    {
        owner.onMultiPurposeBarChanged.AddListener(UIUpdate);
        background.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        owner.onMultiPurposeBarChanged.RemoveListener(UIUpdate);
    }

    public void UIUpdate(float ratio)
    {
        background.gameObject.SetActive(true);
        mask.gameObject.SetActive(true);

        mask.fillAmount = ratio;

        if (ratio < 0.01f)
        {
            background.gameObject.SetActive(false);
            mask.gameObject.SetActive(false);
        }
    }
}