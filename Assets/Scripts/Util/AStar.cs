using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ASNode
{
    public Point pos;
    public int f;
    public int g;
    public int h;
    public Point? parent;
    public ASNode(Point point, int g, int h)
    {
        this.g = g;
        this.h = h;
        this.f = g + h;
        pos = point;
        parent = null;
    }
    public void SetParent(Point p)
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
    private static bool isValid(bool[,] tileMap, int y, int x)
    {
        if (y < 0 || x < 0) return false;
        if (y >= tileMap.GetLength(0)) return false;
        if (x >= tileMap.GetLength(1)) return false;
        if (tileMap[y, x] == false) return false;

        return true;
    }
    private static List<Point> GetPath(ASNode[,] map, Point start, Point end)
    {
        List<Point> path = new List<Point>();
        Point? parent = end;
        while (parent != null)
        {
            path.Add((Point)parent);
            int y = (int)(parent?.y);
            int x = (int)(parent?.x);
            parent = map[y, x].parent;
        }
        path.Reverse();
        return path;
    }
    private static int getDistance(Point start, Point end)
    {
        int diagonalDist = Math.Min(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y));
        int straightDist = Math.Max(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y))
            - diagonalDist;

        return diagonalDist * diagWeight + straightDist * straightWeight;
    }
    public static void PathFinding(int[,] tileMap, Point start,
        Point end, out List<Point> path)
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
        Point start, Point end, out List<Point> path)
    {
        int ySize = tileMap.GetLength(0);
        int xSize = tileMap.GetLength(1);
        bool[,] visit = new bool[ySize, xSize];
        ASNode[,] asMap = new ASNode[ySize, xSize];

        ASNode startNode = new ASNode(start, 0, getDistance(start, end));
        Point? before = null;
        PriorityQueue<ASNode, int> pq = new PriorityQueue<ASNode, int>();
        pq.Enqueue(startNode, startNode.f);

        while (pq.Count > 0)
        {
            ASNode node = pq.Dequeue();
            int y = node.pos.y;
            int x = node.pos.x;

            if (visit[y, x]) continue;

            before = node.pos;
            visit[y, x] = true;
            asMap[y, x] = node;

            if (y == end.y && x == end.x)
            {
                path = GetPath(asMap, start, end);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int ny = y + dy[i];
                int nx = x + dx[i];

                if (!isValid(tileMap, ny, nx)) continue;
                if (visit[ny, nx]) continue;

                Point p = new Point(ny, nx);
                int dist = getDistance(p, end);

                if (asMap[ny, nx] != null && asMap[ny, nx].f <= dist) continue;

                //Console.WriteLine($"{ny}, {nx}, before: {before?.y}, {before?.x}");
                ASNode newNode = new ASNode(p, node.g + 1, dist);
                newNode.parent = before;
                asMap[ny, nx] = newNode;
                pq.Enqueue(newNode, newNode.f);
            }
        }
        path = null;
        return;
    }
}
public struct Point
{
    public int x, y;
    public Point(int y, int x)
    {
        this.x = x; this.y = y;
    }
    public float GetLength()
    {
        return (float)Math.Sqrt(x * x + y * y);
    }
    public static bool operator !=(Point a, Point b)
    {
        if (a.y != b.y) return true;
        if (a.x != b.x) return true;
        return false;
    }
    public static bool operator ==(Point a, Point b)
    {
        if (a.y == b.y)
        {
            if (a.x == b.x)
                return true;
        }

        return false;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.y + b.y, a.x + b.x);
    }
    public static Point operator -(Point a, Point b)
    {
        return new Point(a.y - b.y, a.x - b.x);
    }
}