using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestauSceneFlowController : MonoBehaviour
{
    [SerializeField] private Vector2Int leftBottom;
    [SerializeField] private Vector2Int rightTop;

    private int[,] map;

    private void Awake()
    {
        map = new int[rightTop.x - leftBottom.x + 1,rightTop.y - leftBottom.y + 1];

        LayerMask platformLayer = LayerMask.GetMask("Platform");
        for(int x = leftBottom.x; x <= rightTop.x; x++)
        {
            for(int y = leftBottom.y; y <= rightTop.y; y++)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Vector2.up, 0.1f, platformLayer);
                map[x, y] = (hit.collider == null) ? 0 : 1;
            }
        }

        //for(int i=0;i<map.GetLength(0); i++)
        //{
        //    for(int j=0;j<map.GetLength(1); j++)
        //    {
        //        print($"{i}, {j} : {map[i, j]}");
        //    }
        //}
    }
}
