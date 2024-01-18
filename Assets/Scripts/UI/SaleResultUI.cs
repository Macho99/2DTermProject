using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleResultUI : MonoBehaviour
{
    [SerializeField] Text maxCustomerText;
    [SerializeField] Text happyCustomerText;
    [SerializeField] Text wrongCustomerText;
    [SerializeField] Text angryCustomerText;
    [SerializeField] Text earnMoneyText;
    [SerializeField] Toggle[] starToggles;

    public void Init()
    {
        gameObject.SetActive(true);
        RestauSceneFlowController.Instance.GetResult(out int maxCus, out int happyCus, out int wrongCus,
            out int angryCus, out int earnMoney, out int stars);

        maxCustomerText.text = $"{maxCus.ToString()}명";
        happyCustomerText.text = $"{happyCus.ToString()}명";
        wrongCustomerText.text = $"{wrongCus.ToString()}명";
        angryCustomerText.text = $"{angryCus.ToString()}명";
        earnMoneyText.text = earnMoney.ToString();

        int idx;
        for(idx = 0; idx < stars; idx++)
        {
            starToggles[idx].isOn = true;
        }
        for(;idx < starToggles.Length; idx++)
        {
            starToggles[idx].isOn = false;
        }
    }
}