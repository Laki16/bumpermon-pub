using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpawnGrounds : MonoBehaviour
{
    [Header("Ground Info")]
    public GameObject ground;
    public static int mapLength = 5;
    private int maxGrounds = 3;
    public static float groundXSize = 5.0f;
    [HideInInspector]
    public GameObject[] groundArray = new GameObject[3];
    public GameObject[] backgroundPrefab = new GameObject[4];
    private GameObject[] backgroundArray = new GameObject[mapLength];
    public GameObject tunnel;
    private bool isTunnel;
    [HideInInspector]
    public PlayerController playerController;

    //Iterator
    private int groundIterator = 0;
    private int backgroundIterator = 0;
    private int backgroundIndex = -1;

    [Header("Position")]
    private Vector3 localPostion;
    private Vector3 spawnPosition;

    void Start()
    {
        localPostion = new Vector3(0, 0, 0);
        spawnPosition = localPostion + new Vector3(-50, 0, 0);
        InitGround();
    }

    //in Loading
    private void InitGround()
    {
        for (int i = 0; i < maxGrounds; i++)
        {
            groundArray[i] = Instantiate(ground, spawnPosition, new Quaternion(0, 0, 0, 0));
            groundArray[i].SetActive(true);

            spawnPosition += new Vector3(groundXSize * 10, 0, 0);
        }

        for (int i = 0; i < mapLength; i++)
        {
            backgroundArray[i] = Instantiate(backgroundPrefab[0], localPostion + new Vector3(150 * i + 75, 0, 0), new Quaternion(0, 0, 0, 0));
            backgroundArray[i].SetActive(true);
        }
    }

    public void SpawnGround()
    {
        groundArray[groundIterator % maxGrounds].transform.position += new Vector3(groundXSize * 3 * 10, 0, 0);
        groundIterator++;
        backgroundIndex++;

        if (backgroundIndex % maxGrounds == 0 && backgroundIndex != 0)
        {
            SpawnMap();
        }
    }

    public void SpawnMap()
    {
        //다음 background를 생성해야 할 때 다음 background를 instantiate해서 위치를 바꿔야 한다.
        //위치를 저장해 두고, 그 맵을 삭제한 뒤에 그 위치에 instantiate한다.
        Transform _transform = backgroundArray[backgroundIterator % mapLength].transform;
        Destroy(backgroundArray[backgroundIterator % mapLength]);

        if ((backgroundIterator % (mapLength+1)) == 0 && backgroundIterator <= (mapLength + 1) * 2)
        {
            isTunnel = true;
        }

        if (isTunnel)
        {
            backgroundArray[backgroundIterator % mapLength]
                = Instantiate(tunnel, _transform.position, _transform.rotation);
            isTunnel = false;
        }
        else
        {
            if (backgroundIterator >= (mapLength + 1) * 3)
            {
                backgroundArray[backgroundIterator % mapLength]
                          = Instantiate(backgroundPrefab[mapLength - 1], _transform.position, _transform.rotation);
            }
            else
            {
                backgroundArray[backgroundIterator % mapLength]
                       = Instantiate(backgroundPrefab[(backgroundIterator / (mapLength + 1)) + 1], _transform.position, _transform.rotation);
            }
        }
        
        backgroundArray[backgroundIterator % mapLength].transform.position += new Vector3(150 * mapLength, 0, 0);
        backgroundIterator++;
    }

}
