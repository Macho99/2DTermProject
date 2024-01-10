using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MonsterStateViewer : MonoBehaviour
{
    [SerializeField] Sprite detectSprite;
    [SerializeField] Sprite missSprite;
    [SerializeField] Image background;
    [SerializeField] Image foreground;

    Monster owner;
    float lastOnTime;
    float turnOnDuration;

    private void Awake()
    {
        owner = transform.parent.parent.GetComponent<Monster>();
        turnOnDuration = Constants.StateViewerDuration;
    }

    private void OnEnable()
    {
        owner.onUIStateChanged.AddListener(UIUpdate);
        background.gameObject.SetActive(false);
        foreground.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        owner.onUIStateChanged.RemoveListener(UIUpdate);
    }

    public void UIUpdate()
    {
        MonsterUIState type = owner.curUIState;

        Sprite sprite;
        switch(type)
        {
            case MonsterUIState.Detect:
                sprite = detectSprite;
                break;
            case MonsterUIState.Miss:
                sprite = missSprite;
                break;
            default:
                sprite = detectSprite;
                Debug.LogError("MonsterUIState에 해당하는 sprite가 없음");
                break;
        }

        lastOnTime = Time.time;

        foreground.sprite = sprite;
        foreground.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        StartCoroutine(CoOff());
    }

    private IEnumerator CoOff()
    {
        while (lastOnTime + turnOnDuration > Time.time)
        {
            yield return null;
        }
        background.gameObject.SetActive(false);
        foreground.gameObject.SetActive(false);
    }
}
