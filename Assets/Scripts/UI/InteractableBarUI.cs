using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBarUI : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] Color unSelectedColor;
    [SerializeField] Image background;
    [SerializeField] Image mask;

    Interactable owner;

    private void Awake()
    {
        owner= GetComponentInParent<Interactable>();
    }

    private void OnEnable()
    {
        owner.onInteractorEnter.AddListener(SetActiveTrue);
        owner.onInteractorExit.AddListener(SetActiveFalse);
        owner.onSelected.AddListener(Selected);
        owner.onUnSelected.AddListener(UnSelected);
        owner.onInteractProgress.AddListener(ProgressUpdate);

        SetActiveFalse();
    }

    public void SetActiveTrue()
    {
        background.gameObject.SetActive(true);
        mask.gameObject.SetActive(true);
    }

    public void SetActiveFalse()
    {
        background.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
    }

    public void Selected()
    {
        background.color = selectedColor;
    }

    public void UnSelected()
    {
        background.color = unSelectedColor;
    }

    public void ProgressUpdate()
    {
        float progress = owner.progressRatio;
        mask.fillAmount = progress;
    }
}
