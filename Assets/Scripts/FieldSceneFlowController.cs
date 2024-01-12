using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FieldSceneFlowController : MonoBehaviour
{
    private static FieldSceneFlowController instance;
    private static Player player;

    private bool invenOpened;
    public UnityEvent onInvenOpen;
    public UnityEvent onInvenClose;

    public static Player Player { get {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            return player;
        }}
    public static FieldSceneFlowController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        invenOpened = false;
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    private void OnInventory(InputValue value)
    {
        InvenToggle();
    }

    public void InvenToggle()
    {
        if (false == invenOpened)
        {
            onInvenOpen?.Invoke();
            invenOpened = true;
            Time.timeScale = 0f;
        }
        else
        {
            onInvenClose?.Invoke();
            invenOpened = false;
            Time.timeScale = 1f;
        }
    }
    
    private void OnEscape(InputValue value)
    {

    }
}
