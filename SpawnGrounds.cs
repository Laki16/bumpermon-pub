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
    private int backGroundIterator = 0;
    public GameObject[] backGround = new GameObject[4];
    public GameObject[] backGroundArray = new GameObject[3];

    [Header("Position")]
    private Vector3 localPostion;
    private Vector3 spawnPosition;

    [Header("Stage")]
    //스테이지 바뀌는 중
    public bool betweenStage = false;
    public int nowStage = 0;
    //맵이 바뀌는 속도(터널길이)
    public int mapLoadingCount = 0;
    public GameObject tunnel;
    public GameObject stageController;
    //진행상황 (스테이지 길이를 늘리려면 +)
    public int progressCount = 0;

    [HideInInspector]
    public PlayerController playerController;

    void Start()
    {
        localPostion = new Vector3(0, 0, 0);
        spawnPosition = localPostion + new Vector3(-50, 0, 0);
        InitGround();
    }

    private void Update()
    {
        if(progressCount > 3)
        {
            stageController.GetComponent<StageController>().EndStage();
        }
    }

    public void StageStart()
    {
        progressCount = 0;
        betweenStage = true;
        nowStage++;
    }

    //in Loading
    private void InitGround()
    {
        for (int i = 0; i < 3; i++)
        {
            groundArray[i] = Instantiate(ground, spawnPosition, new Quaternion(0, 0, 0, 0));
            groundArray[i].SetActive(true);

                backGroundArray[i] = Instantiate(backGround[nowStage], localPostion + new Vector3(150 * i +75, 0, 0), new Quaternion(0, 0, 0, 0));
                backGroundArray[i].SetActive(true);

            spawnPosition += new Vector3(groundXSize * 10, 0, 0);
        }
    }

    public void DecideGround()
    {
        if(betweenStage)
        {
            ChangeGround();
        }
        else
        {
            SpawnGround();
        }
        if(!betweenStage && nowStage < 3)
        progressCount++;
    }

    public void SpawnGround()
    {
        groundArray[groundIterator].transform.position += new Vector3(groundXSize * 3 * 10, 0, 0);
        groundIterator++;
        groundIterator %= 3;
        if (groundIterator == 0)
        {
            backGroundArray[backGroundIterator].transform.position += new Vector3(450f, 0, 0);
            backGroundIterator++;
            backGroundIterator %= 3;
        }
    }

    public void ChangeGround()
    {
        groundArray[groundIterator].transform.position += new Vector3(groundXSize * 3 * 10, 0, 0);
        groundIterator++;
        groundIterator %= 3;
        if (groundIterator == 0)
        {
            Vector3 backGroundPos = backGroundArray[backGroundIterator].transform.position;
            Quaternion backGroundRotation = backGroundArray[backGroundIterator].transform.rotation;

            //Destroy(backGroundArray[backGroundIterator]);
            if (mapLoadingCount < 3)
            {
                backGroundArray[backGroundIterator] = Instantiate(tunnel, backGroundPos, backGroundRotation);
                backGroundArray[backGroundIterator].SetActive(true);
                mapLoadingCount++;
            }
            else if(mapLoadingCount < 6)
            {
                backGroundArray[backGroundIterator] = Instantiate(backGround[nowStage], backGroundPos, backGroundRotation);
                backGroundArray[backGroundIterator].SetActive(true);
                mapLoadingCount++;
            }
            else
            {
                mapLoadingCount = 0;
                betweenStage = false;
                //return;
            }
            backGroundArray[backGroundIterator].transform.position += new Vector3(450f, 0, 0);
            backGroundIterator++;
            backGroundIterator %= 3;
        }
    }
}
