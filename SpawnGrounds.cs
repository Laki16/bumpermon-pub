using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpawnGrounds : MonoBehaviour
{
    [Header("Ground Info")]
    public GameObject ground;
    private int maxGrounds = 3;
    public static float groundXSize = 5.0f;
    public GameObject[] groundArray = new GameObject[3];
    private int groundIterator = 0;

    [Header("Position")]
    private Vector3 localPostion;
    private Vector3 spawnPosition;

    void Start()
    {
        localPostion = new Vector3(0,0,0);
        spawnPosition = localPostion + new Vector3(-50, 0, 0);
        InitGround();
    }
    void Update()
    {
        
    }

    //in Loading
    private void InitGround()
    {
        for (int i = 0; i < 3; i++)
        {
            groundArray[i] = Instantiate(ground, spawnPosition, new Quaternion(0, 0, 0, 0));
            groundArray[i].SetActive(true);
            spawnPosition += new Vector3(groundXSize*10, 0, 0);
        }
    }

    public void SpawnGround()
    {
        groundArray[groundIterator].transform.position += new Vector3(groundXSize *3* 10, 0, 0);
        groundIterator++;
        groundIterator %= 3;
    }
}
