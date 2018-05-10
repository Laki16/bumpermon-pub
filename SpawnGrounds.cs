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
    static private float groundXSize;

    [Header("Position")]
    private Vector3 localPostion;
    private Vector3 spawnPosition;

    void Start()
    {
        localPostion = new Vector3(0, 0, 0);
        groundXSize = ground.transform.localScale.x * 10;
        spawnPosition = localPostion + new Vector3(3 * groundXSize, 0, 0);
        InvokeRepeating("SpawnGround", 0.0f, 3.0f);
    }
    void Update()
    {
        
    }

    public void SpawnGround()
    {
        Instantiate(ground, spawnPosition, new Quaternion(0, 0, 0, 0));
        spawnPosition += new Vector3(groundXSize, 0, 0);
    }

    public void DeleteGround()
    {

    }

}
