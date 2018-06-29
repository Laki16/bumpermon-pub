using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

    //put the GroundController in Inspector
    public GameObject groundController;
    public GameObject player;
    public GameObject boss;

    [Header("Trigger")]
    public bool endStage = false;
    public bool endBoss = true;
    public float stageCount;
    //보스 추가시(보스 비활성화시 false)
    public bool bossMode = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void EndStage()
    {
        endStage = true;
        endBoss = false;
        //보스출현 (물리치면 groundController 의 StartStage() 실행
        if (bossMode)
        {
            StartBoss();
        }
        else
        {
            NextStage();
        }
    }

    public void StartBoss()
    {
        boss.SetActive(true);
    }

    public void NextStage()
    {
        endStage = false;
        endBoss = true;
        groundController.GetComponent<SpawnGrounds>().StageStart();
    }

}
