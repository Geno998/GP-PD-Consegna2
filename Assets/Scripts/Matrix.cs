using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public const int sizeX = 35;
    const int sizeY = 9;

    [SerializeField] GameObject wall;
    [SerializeField] GameObject brick;
    [SerializeField] GameObject empty;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] Transform wallsFolder;

    int enemy1Num;
    int enemy2Num;
    int enemy3Num;

    [SerializeField] StageValuesScriptable stageValues;

    List<GameObject> emptys = new List<GameObject>();
    void Awake()
    {
        InizializeMatrix();
    }


    private void Update()
    {
        spawnEnemies();

        
    }


    void InizializeMatrix()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                SpawnMap(x, y);

            }
        }
    }


    void SpawnMap(int x, int y)
    {
        if (x == 0 || y == 0 || x == sizeX - 1 || y == sizeY - 1)
        {
            GameObject tmp = Instantiate(wall);
            tmp.transform.SetParent(wallsFolder);
            tmp.transform.localPosition = new Vector2(x, y);
            tmp.name = "Wall - " + x + " : " + y;

        }
        else
        {
            if (x % 2 == 0 && y % 2 == 0)
            {
                GameObject tmp = Instantiate(wall);
                tmp.transform.SetParent(wallsFolder);
                tmp.transform.localPosition = new Vector2(x, y);
                tmp.name = "Wall - " + x + " : " + y;
            }
            else
            {


                if ((x == 1 && y == sizeY - 2) || (x == 1 && y == sizeY - 3) || (x == 2 && y == sizeY - 2))
                {
                    GameObject tmp = Instantiate(empty);
                    tmp.transform.SetParent(wallsFolder);
                    tmp.transform.localPosition = new Vector2(x, y);
                    tmp.name = "Spawn - " + x + " : " + y;
                }
                else
                {
                    if (x <= 5)
                    {
                        int rng = Random.Range(1, 101);

                        if (rng >= 70)
                        {
                            GameObject tmp = Instantiate(brick);
                            tmp.transform.SetParent(wallsFolder);
                            tmp.transform.localPosition = new Vector2(x, y);
                            tmp.name = "Brick - " + x + " : " + y;
                        }
                        else
                        {
                            GameObject tmp = Instantiate(empty);
                            tmp.transform.SetParent(wallsFolder);
                            tmp.transform.localPosition = new Vector2(x, y);
                            tmp.name = "Empty - " + x + " : " + y;
                        }
                    }
                    else
                    {
                        int rng = Random.Range(1, 101);

                        if (rng >= 70)
                        {
                            GameObject tmp = Instantiate(brick);
                            tmp.transform.SetParent(wallsFolder);
                            tmp.transform.localPosition = new Vector2(x, y);
                            tmp.name = "Brick - " + x + " : " + y;
                        }
                        else
                        {
                            GameObject tmp = Instantiate(empty);
                            emptys.Add(tmp);
                            tmp.transform.SetParent(wallsFolder);
                            tmp.transform.localPosition = new Vector2(x, y);
                            tmp.name = "Empty - " + x + " : " + y;
                        }
                    }

                }

            }
        }
    }


    void spawnEnemies()
    {
        while (enemy1Num < stageValues.enemy1ToSpawn)
        {

            int tmpEmpty = Random.Range(0, emptys.Count);
            Instantiate(enemy1, emptys[tmpEmpty].transform.position, Quaternion.identity);
            emptys.Remove(emptys[tmpEmpty]);
            enemy1Num++;
        }


        if (enemy1Num >= stageValues.enemy1ToSpawn)
        {
            while (enemy2Num < stageValues.enemy2ToSpawn)
            {

                int tmpEmpty = Random.Range(0, emptys.Count);
                Instantiate(enemy2, emptys[tmpEmpty].transform.position, Quaternion.identity);
                emptys.Remove(emptys[tmpEmpty]);
                enemy2Num++;
            }
        }

        if (enemy2Num >= stageValues.enemy2ToSpawn)
        {
            while (enemy3Num < stageValues.enemy3ToSpawn)
            {

                int tmpEmpty = Random.Range(0, emptys.Count);
                Instantiate(enemy3, emptys[tmpEmpty].transform.position, Quaternion.identity);
                emptys.Remove(emptys[tmpEmpty]);
                enemy3Num++;
            }
        }
    }
}
