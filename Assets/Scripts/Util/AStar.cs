using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ASNode
{
    public Vector2Int pos;
    public int f;
    public int g;
    public int h;
    public Vector2Int? parent;
    public ASNode(Vector2Int point, int g, int h)
    {
        this.g = g;
        this.h = h;
        this.f = g + h;
        pos = point;
        parent = null;
    }
    public void SetParent(Vector2Int p)
    {
        this.parent = p;
    }
}

public class AStar
{
    const int diagWeight = 14;
    const int straightWeight = 10;
    //상, 우, 하, 좌
    static int[] dy = { -1, 0, 1, 0 };
    static int[] dx = { 0, 1, 0, -1 };

    private static bool isValid(bool[,] tileMap, int x, int y)
    {
        if (y < 0 || x < 0) return false;
        if (x >= tileMap.GetLength(0)) return false;
        if (y >= tileMap.GetLength(1)) return false;
        if (tileMap[x, y] == false) return false;

        return true;
    }

    private static List<Vector2Int> GetPath(ASNode[,] map, Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? parent = end;
        while (parent != null)
        {
            path.Add((Vector2Int)parent);
            int x = (int)(parent?.x);
            int y = (int)(parent?.y);
            parent = map[x, y].parent;
        }
        path.Reverse();
        return path;
    }

    private static int getDistance(Vector2Int start, Vector2Int end)
    {
        int diagonalDist = Math.Min(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y));
        int straightDist = Math.Max(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y))
            - diagonalDist;

        return diagonalDist * diagWeight + straightDist * straightWeight;
    }

    public static void PathFinding(int[,] tileMap, Vector2Int start,
        Vector2Int end, out List<Vector2Int> path)
    {
        bool[,] boolMap = new bool[tileMap.GetLength(0), tileMap.GetLength(1)];
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                if (tileMap[i, j] == 1) // 벽이면
                {
                    boolMap[i, j] = false;
                }
                else
                {
                    boolMap[i, j] = true;
                }
            }
        }
        PathFinding(boolMap, start, end, out path);
    }

    public static void PathFinding(bool[,] tileMap,
        Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        int xSize = tileMap.GetLength(0);
        int ySize = tileMap.GetLength(1);

        bool[,] visit = new bool[xSize, ySize];
        ASNode[,] asMap = new ASNode[xSize, ySize];

        ASNode startNode = new ASNode(start, 0, getDistance(start, end));
        Vector2Int? before = null;
        PriorityQueue<ASNode> pq = new PriorityQueue<ASNode>((a, b) => (a.f < b.f) ? -1 : 1);
        pq.Enqueue(startNode);

        while (pq.Count > 0)
        {
            ASNode node = pq.Dequeue();
            int x = node.pos.x;
            int y = node.pos.y;

            if (visit[x, y]) continue;

            before = node.pos;
            visit[x, y] = true;
            asMap[x, y] = node;

            if (x == end.x && y == end.y)
            {
                path = GetPath(asMap, start, end);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (!isValid(tileMap, nx, ny)) continue;
                if (visit[nx, ny]) continue;

                Vector2Int p = new Vector2Int(nx, ny);
                int dist = getDistance(p, end);

                if (asMap[nx, ny] != null && asMap[nx, ny].f <= dist) continue;

                //Console.WriteLine($"{ny}, {nx}, before: {before?.y}, {before?.x}");
                ASNode newNode = new ASNode(p, node.g + 1, dist);
                newNode.parent = before;
                asMap[nx, ny] = newNode;
                pq.Enqueue(newNode);
            }
        }
        path = null;
        return;
    }
}