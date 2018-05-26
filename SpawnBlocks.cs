using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyController;

    [Header("System")]
    public GameObject player;
    private int beforeLane = 0;
    private int nowLane = 0;
    private int iterator = 0;
    private int nodeIterator = 0;
    private int delNodeIterator = 0;
    private bool endSpawn = true;

    [Header("Block")]
    public GameObject block;
    public static float blockXSize = 1.0f;
    private static int maxBlocks = 250;
    private static int maxNodes = 20;
    private int existNode = 0;
    private GameObject[] lane = new GameObject[maxBlocks];
    private GameObject[] node = new GameObject[maxNodes];

    [Header("Difficulty")]
    static public int minSpace = 4;
    static public int maxSpace = 10;
    public int space;
    private int difficulty;
    static public int maxDifficulty = 15;
    static public int minDifficulty = 2;
    private int beforeDifficulty;

    void Awake()
    {
        InitBlock();
        endSpawn = true;
    }

    void Start()
    {
        for (int i = 0; i < 10; i++) SpawnBlock();
    }

    void Update()
    {
        DeleteBlock();
        //if (node[delNodeIterator].transform.position.x < player.transform.position.x)
        //{
        //    if (existNode > 0)
        //    {
        //        existNode--;
        //        delNodeIterator++;
        //        delNodeIterator %= maxNodes;
        //    }
        //}
        //Debug.Log(existNode);
        if (endSpawn)
        {
            if (lane[iterator].transform.position.x + 25.0f < player.transform.position.x)
            {
                //if (lane[iterator] == node[nodeIterator])
                //{
                //    existNode--;
                //}
                if (existNode < maxNodes)
                {
                    SpawnBlock();
                }
            }
        }

    }

    //속도비례
    public void SetSpace()
    {
        space = (int)(player.GetComponent<PlayerController>().speed / 100.0f) + minSpace;
    }

    //거리비례
    public void SetDifficulty()
    {
        if (player.transform.position.x <= 200.0f)
        {
            difficulty = Random.Range(10, maxDifficulty);
        }
        else if (200.0f < player.transform.position.x && player.transform.position.x <= 500.0f)
        {
            difficulty = Random.Range(8, 13);
        }
        else if (500.0f < player.transform.position.x && player.transform.position.x <= 1000.0f)
        {
            difficulty = Random.Range(6, 10);
        }
        else if (1000.0f < player.transform.position.x && player.transform.position.x <= 1500.0f)
        {
            difficulty = Random.Range(4, 8);
        }
        else
        {
            difficulty = Random.Range(minDifficulty, 7);
        }
    }

    public void SpawnBlock()
    {
        endSpawn = false;
        ComputeLane();
        beforeDifficulty += difficulty + space;
        SetDifficulty();
        SetSpace();

        //node[nodeIterator] = lane[iterator];
        for (int i = 0; i < difficulty; i++)
        {
            lane[iterator].transform.position = new Vector3(beforeDifficulty, 0, 0);
            lane[iterator].transform.position += new Vector3(blockXSize * i, 0, nowLane);
            lane[iterator].SetActive(true);
            lane[iterator].GetComponent<Renderer>().enabled = true;
            lane[iterator].GetComponent<Collider>().enabled = true;
            if (i == difficulty - 1)
            {
                node[nodeIterator] = lane[iterator];
            }
            iterator++;
            iterator %= maxBlocks;
        }
        //------------------- Attack --------------------------
        enemyController.GetComponent<EnemyController>().MineRequest(beforeDifficulty, difficulty, nowLane);
        //-----------------------------------------------------
        existNode++;
        nodeIterator++;
        nodeIterator %= maxNodes;
        endSpawn = true;
    }

    public void DeleteBlock()
    {
        if (node[delNodeIterator].transform.position.x + 5.0f < player.transform.position.x)
        {
            if (existNode > 0)
            {
                existNode--;
                delNodeIterator++;
                delNodeIterator %= maxNodes;
            }
        }
    }

    public void ComputeLane()
    {
        nowLane = Random.Range(0, 2);
        if (beforeLane == -1)
        {
            if (nowLane == 0)
            {
                nowLane = 0;
            }
            else
            {
                nowLane = 1;
            }
        }
        else if (beforeLane == 0)
        {
            if (nowLane == 0)
            {
                nowLane = -1;
            }
            else
            {
                nowLane = 1;
            }
        }
        else
        {
            if (nowLane == 0)
            {
                nowLane = -1;
            }
            else
            {
                nowLane = 0;
            }
        }
        beforeLane = nowLane;
    }

    public void InitBlock()
    {
        for (int i = 0; i < maxBlocks; i++)
        {
            lane[i] = Instantiate(block, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }
}
