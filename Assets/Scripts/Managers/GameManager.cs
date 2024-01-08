using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;
    private static ResourceManager resourceManager;
    private static InventoryManager inventoryManager;
    private static UIManager uiManager;
    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return dataManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static InventoryManager Inven { get { return inventoryManager; } }
    public static UIManager UI {  get { return uiManager; } }

    public UnityEvent onFpsChange;
    public int fps;
    private int frameCnt;
    private float timeCnt;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitManagers();

        frameCnt = 0;
        timeCnt = 0f;
    }

    private void Update()
    {
        frameCnt++;
        timeCnt += Time.deltaTime;
        if(timeCnt > 1f)
        {
            timeCnt -= 1f;
            fps = frameCnt;
            frameCnt = 0;
            onFpsChange?.Invoke();
        }
    }

    private void InitManagers()
    {
        dataManager = new GameObject("DataManager").AddComponent<DataManager>();
        dataManager.transform.parent = transform;
        resourceManager = new GameObject("ResourceManager").AddComponent<ResourceManager>();
        resourceManager.transform.parent = transform;
        inventoryManager = new GameObject("InventoryManager").AddComponent<InventoryManager>();
        inventoryManager.transform.parent = transform;
        uiManager = new GameObject("UIManager").AddComponent<UIManager>();
        uiManager.transform.parent = transform;
    }
}