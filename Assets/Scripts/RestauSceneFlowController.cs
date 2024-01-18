using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestauSceneFlowController : MonoBehaviour
{
    [SerializeField] private Vector2Int leftBottom;
    [SerializeField] private Vector2Int rightTop;
    [SerializeField] private Vector2Int entrance;
    [SerializeField] private Vector2Int[] tables;
    [SerializeField] private bool[] tableVisit;
    [SerializeField] private Chief chief;
    [SerializeField] private Customer customerPrefab;

    private List<CuisineItem> allCuisineList;
    private List<CuisineItem> selectableCuisineList;

    private static RestauSceneFlowController instance;
    private static RestaurantPlayer player;

    private int maxCustomer;
    private int curCustomer;
    private int leftCustomer;

    private int happyCustomer;
    private int wrongMenuCustomer;
    private int angryCustomer;
    private int startMoney;

    public Vector2Int Entrance { get { return entrance; } }

    private int[,] map;

    public int[,] Map { get { return map; } }

    public static RestaurantPlayer Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<RestaurantPlayer>();
            }
            return player;
        }
    }

    public static RestauSceneFlowController Instance
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

        maxCustomer = Random.Range(7, 12);
        curCustomer = 0;
        leftCustomer = maxCustomer;

        happyCustomer = 0;
        wrongMenuCustomer = 0;
        angryCustomer = 0;
        startMoney = GameManager.Inven.Money;

        map = new int[rightTop.x - leftBottom.x + 1, rightTop.y - leftBottom.y + 1];

        LayerMask platformLayer = LayerMask.GetMask("Platform");
        for (int x = leftBottom.x; x <= rightTop.x; x++)
        {
            for (int y = leftBottom.y; y <= rightTop.y; y++)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Vector2.up, 0.1f, platformLayer);
                map[x, y] = (hit.collider == null) ? 0 : 1;
            }
        }

        tableVisit = new bool[tables.Length];

        allCuisineList = new List<CuisineItem>();
        selectableCuisineList = new List<CuisineItem>();

        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.BasicSoup));
        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.FriedEgg));
        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.GrilledSkewers));
        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.GrilledWholeChicken));
        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.ChickenSkewersAndBoiledEggs));
        allCuisineList.Add((CuisineItem) GameManager.Data.GetItem(ItemID.BuffaloSteak));

        foreach(var item in allCuisineList)
        {
            selectableCuisineList.Add(item);
        }

        _ = StartCoroutine(SaleStart());
    }

    private IEnumerator SaleStart()
    {
        while(curCustomer < maxCustomer)
        {
            yield return new WaitUntil(() =>
            {
                foreach (bool val in tableVisit)
                {
                    if (false == val)
                        return true;
                }
                return false;
            });
            Instantiate(customerPrefab);
            curCustomer++;
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }

    public int AllocateTable()
    {
        List<int> emptyTableIdxList = new List<int>();

        for(int i = 0; i < tableVisit.Length; i++)
        {
            if (false == tableVisit[i])
            {
                emptyTableIdxList.Add(i);
            }
        }

        int selectIdx = emptyTableIdxList[Random.Range(0, emptyTableIdxList.Count)];
        tableVisit[selectIdx] = true;

        return selectIdx;
    }

    public Vector2Int GetTablePos(int idx)
    {
        return tables[idx];
    }

    public void ReturnTable(int idx)
    {
        tableVisit[idx] = false;
    }

    public CuisineItem AllocateMenu()
    {
        int idx = Random.Range(0, selectableCuisineList.Count);
        CuisineItem item = (CuisineItem) selectableCuisineList[idx].Clone();
        //selectableCuisineList.RemoveAt(idx);
        chief.OrderEnqueue((CuisineItem) item.Clone());
        return item;
    }

    public void CustomerExit()
    {
        leftCustomer--;
        print($"남은 손님: {leftCustomer}");
        if (0 == leftCustomer)
        {
            print("영업 종료");
        }
    }

    public void HappyCntUp()
    {
        happyCustomer++;
    }

    public void WrongCntUp()
    {
        wrongMenuCustomer++;
    }

    public void AngryCntUp()
    {
        angryCustomer++;
    }
}