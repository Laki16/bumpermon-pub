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
    //private int deleteIterator = 0;
    //int passedCount = 0;

    [Header("Block")]
    public GameObject block;
    public static float blockXSize = 1.0f;
    private static int maxBlocks = 150;
    private GameObject[] lane = new GameObject[maxBlocks];
    private float genSpeed;

    [Header("Difficulty")]
    public int space = 3;
    private int difficulty;
    private int beforeDifficulty;

    private void Start()
    {
        genSpeed = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().speed;
        InitBlock();
        endSpawn = true;
    }

    private void FixedUpdate()
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
            //passedCount++;
            lane[iterator].transform.position = new Vector3(beforeDifficulty, 0, 0);
            lane[iterator].transform.position += new Vector3(blockXSize * i, 0, nowLane);
            lane[iterator].SetActive(true);
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
            if (nowLane == -1)
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
            if (nowLane == 1)
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
    
    //void DeleteBlock()
    //{
    //    while (true)
    //    {
    //        //Debug.Log(endSpawn);
    //        if (endSpawn)
    //        {
    //            endSpawn = false;
    //            SpawnBlock();
    //            //Debug.Log("Spawn");
    //        }
    //        if (passedCount > 100)
    //        {
    //            passedCount--;
    //            lane[deleteIterator].SetActive(false);
    //            deleteIterator++;
    //            deleteIterator %= maxBlocks;
    //        }
    //    }
    //}
}
