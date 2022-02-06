using System.Collections.Generic;
using UnityEngine;

public class Bacteries : MenuGame
{
    [Range(10, 200)]
    [SerializeField] private int sizeOfField;
    [Range(0.3f, 40f)]
    [SerializeField] private int Destinity;
    [Space]
    [Range(1, 30)]
    [SerializeField] private uint ourDeleteDistance;
    [Range(1, 10)]
    [SerializeField] private uint ourSpeed;
    [Range(0f, 0.5f)]
    [SerializeField] private float ourSpawnChance;
    [Range(1, 10)]
    [SerializeField] private uint ourGrow;
    [Space]
    [Range(1, 30)]
    [SerializeField] private uint enemyDeleteDistance;
    [Range(1, 10)]
    [SerializeField] private uint enemySpeed;
    [Range(0f, 0.5f)]
    [SerializeField] private float enemySpawnChance;
    [Range(1, 10)]
    [SerializeField] private uint enemyGrow;
    private float time;
    private LinkedList<Bacterie> ourBacteries;
    private LinkedList<Bacterie> enemyBacteries;
    private uint[] foodPlaces;
    private Texture2D texture;

    public new void Start()
    {
        texture = new Texture2D(sizeOfField, sizeOfField);
        for (int y = 0; y < sizeOfField; y++)
            for (int x = 0; x < sizeOfField; x++)
                texture.SetPixel(x, y, new Color(0, 0, 0, 1f));

        time = 0;

        ourBacteries = new LinkedList<Bacterie>();
        ourBacteries.AddLast(new Bacterie(new Vector2Int((int)(sizeOfField * 0.25f), (int)(sizeOfField * 0.5f)),
            1, (uint)sizeOfField));

        enemyBacteries = new LinkedList<Bacterie>();
        enemyBacteries.AddLast(new Bacterie(new Vector2Int((int)(sizeOfField * 0.75f), (int)(sizeOfField * 0.5f)),
            2, (uint)sizeOfField));

        foodPlaces = new uint[(int)(sizeOfField * sizeOfField * 0.1f)];

        int randomValue;
        for (int index = 0; index < (int)(sizeOfField * sizeOfField * 0.1f); index++)
        {
            randomValue = Random.Range(1, 11);
            foodPlaces[index] = (uint)(randomValue);
        }

        UpdateImage();

        base.Start();
    }
    private void Update()
    {
        time += Time.deltaTime;
        while (time * Destinity >= 1f)
        {
            time -= 1f / Destinity;

            //create new bacterie
            //int randomValue = Random.Range(0, 10000);
            //if (randomValue < 10000 * ourSpawnChance)
            //    enemyBacteries.AddLast(new Bacterie(new Vector2Int(randomValue % 100, randomValue / 100),
            //    2,(uint)sizeOfField));
            //else
            //if (randomValue < 10000 * (enemySpawnChance + ourSpawnChance))
            //    ourBacteries.AddLast(new Bacterie(new Vector2Int(randomValue % 100, (int)(randomValue/
            //        (enemySpawnChance + ourSpawnChance) *0.01f)),
            //    (uint)sizeOfField));

            //enemies go
            LinkedListNode<Bacterie> listNode = enemyBacteries.First;
            if (listNode == null)
            {
                Finish(true);
                return;
            }
            do
            {
                texture.SetPixel(listNode.Value.Position.x, listNode.Value.Position.y, new Color(0, 0, 0, 1));
                listNode.Value.go(ourBacteries, enemySpeed);

                listNode = listNode.Next;
            }
            while (listNode != null);

            //enemies delete not needed bacteries
            listNode = enemyBacteries.First;
            do
            {
                //delete enemies
                LinkedListNode<Bacterie> nextBacteries = listNode.Next;
                while (nextBacteries != null)
                {
                    if (CyclicRange.Distance(nextBacteries.Value.Position, listNode.Value.Position, sizeOfField)
                        <= enemyDeleteDistance)
                    {
                        listNode.Value.addSize(nextBacteries.Value.Size);
                        enemyBacteries.Remove(nextBacteries.Value);
                    }
                    nextBacteries = nextBacteries.Next;
                }

                //delete our bacteries
                LinkedListNode<Bacterie> ourBacteriesNode = ourBacteries.First;
                while (ourBacteriesNode != null)
                {
                    //Debug.Log(CyclicRange.Distance(ourBacteriesNode.Value.Position, listNode.Value.Position,sizeOfField));
                    if (CyclicRange.Distance(ourBacteriesNode.Value.Position, listNode.Value.Position, sizeOfField)
                        <= enemyDeleteDistance)
                    {
                        listNode.Value.addSize(ourBacteriesNode.Value.Size);
                        ourBacteries.Remove(ourBacteriesNode.Value);
                    }
                    ourBacteriesNode = ourBacteriesNode.Next;
                }

                //if on foodPlace
                if (foodPlaces[listNode.Value.Position.y * 10 + listNode.Value.Position.x / 10] == listNode.Value.Position.x % 10 + 1)
                {
                    listNode.Value.addSize(enemyGrow);
                    foodPlaces[listNode.Value.Position.y * 10 + listNode.Value.Position.x / 10] = 0;
                }

                listNode = listNode.Next;
            }
            while (listNode != null);

            //our go
            listNode = ourBacteries.First;
            if (listNode == null)
            {
                Finish();
                return;
            }
            do
            {
                texture.SetPixel(listNode.Value.Position.x, listNode.Value.Position.y, new Color(0, 0, 0, 1));
                listNode.Value.go(enemyBacteries, ourSpeed);

                listNode = listNode.Next;
            }
            while (listNode != null);

            //our delete not needed bacteries
            listNode = ourBacteries.First;
            do
            {
                //delete our
                LinkedListNode<Bacterie> nextBacteries = listNode.Next;
                while (nextBacteries != null)
                {
                    if (CyclicRange.Distance(nextBacteries.Value.Position, listNode.Value.Position, sizeOfField)
                        <= ourDeleteDistance)
                    {
                        listNode.Value.addSize(nextBacteries.Value.Size);
                        ourBacteries.Remove(nextBacteries.Value);
                    }
                    nextBacteries = nextBacteries.Next;
                }

                //delete enemies
                LinkedListNode<Bacterie> enemiesBacteriesNode = enemyBacteries.First;
                while (enemiesBacteriesNode != null)
                {
                    if (CyclicRange.Distance(enemiesBacteriesNode.Value.Position, listNode.Value.Position, sizeOfField)
                        <= ourDeleteDistance)
                    {
                        listNode.Value.addSize(enemiesBacteriesNode.Value.Size);
                        enemyBacteries.Remove(enemiesBacteriesNode.Value);
                    }
                    enemiesBacteriesNode = enemiesBacteriesNode.Next;
                }

                //if on foodPlace
                if (foodPlaces[listNode.Value.Position.y * 10 + listNode.Value.Position.x / 10] == listNode.Value.Position.x % 10 + 1)
                {
                    listNode.Value.addSize(ourGrow);
                    foodPlaces[listNode.Value.Position.y * 10 + listNode.Value.Position.x / 10] = 0;
                }


                listNode = listNode.Next;
            }
            while (listNode != null);
            UpdateImage();
        }
    }
    public new void Finish(bool isWin = false)
    {
        base.Finish(isWin);
        this.enabled = false;
    }
    public void UpdateImage()
    {
        foreach (Bacterie bacterie in ourBacteries)
            texture.SetPixel(bacterie.Position.x, bacterie.Position.y, new Color(0, 0.04f * bacterie.Size, 1f));
        foreach (Bacterie bacterie in enemyBacteries)
            texture.SetPixel(bacterie.Position.x, bacterie.Position.y, new Color(1f, 0.04f * bacterie.Size, 0));
        texture.Apply();
        base.UpdateImage(texture);
    }
}
class Bacterie
{
    private Vector2Int position;
    private uint size;
    private uint sizeOfField;

    public Vector2Int Position
    {
        get => position;
    }
    public uint Size
    {
        get => size;
    }

    public Bacterie(Vector2Int position = new Vector2Int(), uint size = 1, uint sizeOfField = 100)
    {
        this.size = (uint)CyclicRange.Range(size, 1000);
        this.position = CyclicRange.Range(position);
        this.sizeOfField = (uint)CyclicRange.Range(sizeOfField, 200, 10);
    }

    public void addSize(uint size)
    {
        if (this.size + size <= 200)
            this.size += size;
    }
    public void go(LinkedList<Bacterie> enemies, uint step = 1)
    {
        Bacterie nearest = enemies.First.Value;
        float minimumDistance = sizeOfField * sizeOfField * 0.25f + 1, distance;

        foreach (Bacterie enemy in enemies)
        {
            if (enemy.Size != size)
            {
                distance = CyclicRange.Distance(enemy.position, position, sizeOfField);
                if (minimumDistance > distance)
                {
                    minimumDistance = distance;
                    nearest = enemy;
                }
            }
        }
        if (minimumDistance < sizeOfField * sizeOfField * 0.25f + 1)
        {
            Vector2Int normal = position - nearest.position;
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) && Mathf.Abs(normal.y) != 0)
                position.y = CyclicRange.Range(normal.y > 0 ^ Mathf.Abs(normal.y) * 2 > sizeOfField ^ nearest.Size < size
                    ? position.y + (int)step : position.y - (int)step, (int)sizeOfField);
            else
                position.x = CyclicRange.Range(normal.x > 0 ^ Mathf.Abs(normal.x) * 2 > sizeOfField ^ nearest.Size < size
              ? position.x + (int)step : position.x - (int)step, (int)sizeOfField);
        }
        else
            if (Random.Range(0, 2) == 0)
            position.x = (int)CyclicRange.Range(Random.Range(0, 2) == 0 ? position.x + step : position.x - step, sizeOfField);
        else
            position.y = (int)CyclicRange.Range(Random.Range(0, 2) == 0 ? position.y + step : position.y - step, sizeOfField);
        //foreach (Bacterie enemy in enemies)
        //{
        //    if (enemy.Size > size)
        //    {
        //        distance = CyclicRange.Distance(enemy.position, position);
        //        if (minimumDistance > distance)
        //        {
        //            minimumDistance = distance;
        //            minimumDistanceVector = enemy.position;
        //            isOur = false;
        //        }
        //    }
        //    else
        //    if (enemy.Size < size)
        //    {
        //        distance = CyclicRange.Distance(enemy.position, position);
        //        if (minimumDistance > distance)
        //        {
        //            minimumDistance = distance;
        //            minimumDistanceVector = enemy.position;
        //            isOur = false;
        //        }
        //    }
        //}
        //if (minimumDistance <= 2500f)
        //{
        //    Vector2Int normal = position - minimumDistanceVector;
        //    if (Vector2Int.Distance(new Vector2Int(), normal) > 1.41f * size)
        //        normal *= -1;
        //    if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        //        position.x = CyclicRange.Range(normal.x > 0 ? position.x+step : position.x - step);
        //    else
        //        position.y = CyclicRange.Range(normal.y > 0 ? position.y + step : position.y - step);
        //}
        //else
        //{
        //    foreach (Bacterie enemy in enemies)
        //    {
        //        if (enemy.Size < size)
        //        {
        //            distance = CyclicRange.Distance(enemy.position, position);
        //            if (minimumDistance > distance)
        //            {
        //                minimumDistance = distance;
        //                minimumDistanceVector = enemy.position;
        //            }
        //        }
        //    }
        //    if (minimumDistance <= 2500f)
        //    {
        //        Vector2Int normal = minimumDistanceVector - position;
        //        if (Vector2Int.Distance(new Vector2Int(), normal) > 1.41f * size)
        //            normal *= -1;
        //        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        //            position.x = CyclicRange.Range(normal.x > 0 ? position.x + step : position.x - step);
        //        else
        //            position.y = CyclicRange.Range(normal.y > 0 ? position.y + step : position.y - step);
        //    }
        //    else
        //    if (Random.Range(0, 2) == 0)
        //        position.x = CyclicRange.Range(Random.Range(0,2) == 0 ? position.x + step : position.x - step);
        //    else
        //        position.y = CyclicRange.Range(Random.Range(0, 2) == 0 ? position.y + step : position.y - step);
        //}
    }
}
