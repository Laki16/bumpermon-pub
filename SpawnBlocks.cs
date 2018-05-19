using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [Header("System")]
    public GameObject player;
    private int beforeLane = 0;
    private int nowLane = 0;
    private int iterator = 0;
    private bool endSpawn = true;

    [Header("Block")]
    public GameObject block;
    public static float blockXSize = 1.0f;
    private static int maxBlocks = 150;
    private GameObject[] lane = new GameObject[maxBlocks];

    [Header("Difficulty")]
    static public int space = 3;
    private int difficulty;
    static public int maxDifficulty = 15;
    static public int minDifficulty = 5;
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
        if (endSpawn)
        {
            if(lane[iterator].transform.position.x + 25.0f < player.transform.position.x)
            {
                SpawnBlock();
            }
        }
    }

    public void SpawnBlock()
    {
        ComputeLane();
        beforeDifficulty += difficulty + space;
        difficulty = Random.Range(5, 15);

        for (int i = 0; i < difficulty; i++)
        {
            lane[iterator].transform.position = new Vector3(beforeDifficulty, 0, 0);
            lane[iterator].transform.position += new Vector3(blockXSize * i, 0, nowLane);
            lane[iterator].SetActive(true);
            lane[iterator].GetComponent<Renderer>().enabled = true;
            lane[iterator].GetComponent<Collider>().enabled = true;
            iterator++;
            iterator %= maxBlocks;
        }
        endSpawn = true;
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
