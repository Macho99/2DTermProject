using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FieldSceneFlowController : MonoBehaviour
{
    private static FieldSceneFlowController instance;
    private static FieldPlayer player;

    [SerializeField] Vector2 originPoint;
    [SerializeField] GameObject monsterFolder;
    [SerializeField] float spawnStartX;
    [SerializeField] float snowX;
    [SerializeField] float spawnEndX;
    [SerializeField] float topY;

    [Header("약한 순서대로 둘 것")]
    [SerializeField] Monster[] monsterPrefabs;
    [SerializeField] int[] spawnAmounts;

    private LayerMask platformLayer;

    private bool invenOpened;
    public UnityEvent onInvenOpen;
    public UnityEvent onInvenClose;
    public UnityEvent onPlayerDie;
    public UnityEvent onGameOver;
    public UnityEvent<int> onNumPressed;
    public UnityEvent onTitleStart;
    public UnityEvent onTitleEnd;

    private int killCnt;

    public int KillCnt { get { return killCnt; } }

    public static FieldPlayer Player { get {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<FieldPlayer>();
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
        onNumPressed = new UnityEvent<int>();
        platformLayer = LayerMask.GetMask("Platform");
        killCnt = 0;

        if(GameManager.Data.TitlePlayed == false)
        {
            GameManager.Data.TitlePlayed = true;
            onTitleStart?.Invoke();
            _ = StartCoroutine(CoTitle());
        }
        else
        {
            SpawnMonster();
            player.PlayerStart();
        }
    }

    IEnumerator CoTitle()
    {
        while (true)
        {
            if(true == Input.anyKeyDown)
            {
                break;
            }
            yield return null;
        }
        SpawnMonster();
        player.PlayerStart();
        onTitleEnd?.Invoke();
    }

    private void SpawnMonster()
    {
        float offsetX = 40;
        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            float middleX = spawnStartX + ((spawnEndX - spawnStartX) / (monsterPrefabs.Length)) * i;
            for (int j= 0; j < spawnAmounts[i]; j++)
            {
                float xPos = Random.Range(middleX - offsetX, middleX + offsetX);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPos, topY), Vector2.down, topY, platformLayer);

                if (hit.collider == null) 
                {
                    Debug.LogError("topY에서 지면까지 닿지 않음");
                    return;
                }
                float yPos = topY - hit.distance;
                Monster monster = Instantiate(monsterPrefabs[i], monsterFolder.transform);
                monster.transform.position = new Vector2(xPos, yPos);
            }
        }
    }

    private void Start()
    {
        GameManager.Inven.PlayerAddWeapons();
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
            player = null;
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
        InvenToggle();
    }

    private void OnNum1(InputValue value)
    {
        onNumPressed?.Invoke(1);
    }
    private void OnNum2(InputValue value)
    {
        onNumPressed?.Invoke(2);
    }
    private void OnNum3(InputValue value)
    {
        onNumPressed?.Invoke(3);
    }
    private void OnNum4(InputValue value)
    {
        onNumPressed?.Invoke(4);
    }

    public void PlayerDie()
    {
        StartCoroutine(CoPlayerDie());
    }

    IEnumerator CoPlayerDie()
    {
        onPlayerDie?.Invoke();
        yield return new WaitForSeconds(3f);
        GameOver();
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public void AddKillCnt()
    {
        killCnt++;
    }

    public void GoToRestaurant()
    {
        SceneManager.LoadScene("RestaurantScene");
    }

    public void PlayerRespawn()
    {
        player.transform.position = originPoint;
    }

    
}
