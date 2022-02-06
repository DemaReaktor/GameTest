using UnityEngine;

public static class CyclicRange
{
    public static int Distance(int a, int b, int size = 100) =>
        Mathf.Abs(a - b) * 2 <= size ? Mathf.Abs(a - b) : size - Mathf.Abs(a - b);
    public static float Distance(float a, float b, float size = 100) =>
        Mathf.Abs(a - b) * 2 <= size ? Mathf.Abs(a - b) : size - Mathf.Abs(a - b);
    public static float Distance(Vector2 a, Vector2 b, float size = 100)
    {
        float x = Mathf.Abs(a.x - b.x), y = Mathf.Abs(a.y - b.y);
        if (x*2 > size) x = size - x;
        if (y*2 > size) y = size - y;
        return Mathf.Sqrt(x * x + y * y);
    }

    public static int Range(int value, int max=100,int min=0) {
        if (max <= min)
            throw new System.Exception("min should be less than max");
        while (value >= max)
            value += min - max;
        while (value < min)
            value += max-min;
        return value;
    }
    public static float Range(float value, float max = 100, float min = 0)
    {
        if (max <= min)
            throw new System.Exception("min should be less than max");
        while (value >= max)
            value += min - max;
        while (value < min)
            value += max - min;
        return value;
    }
    public static Vector2 Range(Vector2 value)
    {
        Vector2 max=new Vector2(100,100),min=new Vector2();
        while (value.x >= max.x)
            value.x += min.x - max.x;
        while (value.x < min.x)
            value.x += max.x - min.x;

        while (value.y >= max.y)
            value.y += min.y - max.y;
        while (value.y < min.y)
            value.y += max.y - min.y;
        return value;
    }
    public static Vector2 Range(Vector2 value, Vector2 max, Vector2 min)
    {
        if (max.x <= min.x || max.x<=max.y)
            throw new System.Exception("min coordinates should be less than max coordinates");
        while (value.x >= max.x)
            value.x += min.x - max.x;
        while (value.x < min.x)
            value.x += max.x - min.x;

        while (value.y >= max.y)
            value.y += min.y - max.y;
        while (value.y < min.y)
            value.y += max.y - min.y;
        return value;
    }
    public static Vector2Int Range(Vector2Int value)
    {
        Vector2Int max = new Vector2Int(100, 100), min = new Vector2Int();
        while (value.x >= max.x)
            value.x += min.x - max.x;
        while (value.x < min.x)
            value.x += max.x - min.x;

        while (value.y >= max.y)
            value.y += min.y - max.y;
        while (value.y < min.y)
            value.y += max.y - min.y;
        return value;
    }
    public static Vector2Int Range(Vector2Int value, Vector2Int max, Vector2Int min)
    {
        if (max.x <= min.x || max.x <= max.y)
            throw new System.Exception("min coordinates should be less than max coordinates");
        while (value.x >= max.x)
            value.x += min.x - max.x;
        while (value.x < min.x)
            value.x += max.x - min.x;

        while (value.y >= max.y)
            value.y += min.y - max.y;
        while (value.y < min.y)
            value.y += max.y - min.y;
        return value;
    }
}
