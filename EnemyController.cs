using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("SYSTEM")]
    public GameObject lArm;
    public GameObject rArm;
    public GameObject blockController;

    [Header("Trigger")]
    public bool lArmJustMove = true;
    public bool rArmJustMove = true;
    //마인 설치 가능여부
    public bool readyForMine = true;

    [Header("MineInfo")]
    Vector3 minePosition;

    void Start()
    {
        StartCoroutine(LArmControl());
        StartCoroutine(RArmControl());
    }
    
    public IEnumerator LArmControl()
    {
        while (true)
        {
            //평소
            if (lArmJustMove)
            {
                //오버헤드 방지
                while (lArmJustMove)
                {
                    yield return null;
                }
            }
            //마인 설치 명령오면
            else
            {
                lArm.GetComponent<EnemyArm>().PlaceMine(minePosition);
                while(!lArmJustMove)
                {
                    yield return null;
                }
                readyForMine = true;
            }
        }
    }
    public IEnumerator RArmControl()
    {
        while (true)
        {
            //평소
            if (rArmJustMove)
            {
                //오버헤드 방지
                while (rArmJustMove)
                {
                    yield return null;
                }
            }
            //마인 설치 명령오면
            else
            {
                rArm.GetComponent<EnemyArm>().PlaceMine(minePosition);
                while (!rArmJustMove)
                {
                    yield return null;
                }
                readyForMine = true;
            }
        }
    }

    //마인 설치 요구
    public void MineRequest(int beforeDifficulty, int difficulty, int lane)
    {
        if (readyForMine)
        {
            readyForMine = false;
            //공격 레인 결정
            int mineLane;
            if (lane == 1)
            {
                mineLane = Random.Range(-1, 1);
            }
            else if (lane == 0)
            {
                mineLane = Random.Range(1, 3); if (mineLane == 2) { mineLane = -1; }
            }
            else
            {
                mineLane = Random.Range(0, 2);
            }

            //공격 위치 결정
            minePosition = new Vector3(beforeDifficulty + (difficulty / 2), 0.3f, mineLane);
            if (mineLane == 1) { lArmJustMove = false; }
            else { rArmJustMove = false; }
        }
    }

    public IEnumerator GameOverWait()
    {
        StopAllCoroutines();
        lArmJustMove = true;
        rArmJustMove = true;
        readyForMine = true;
        StartCoroutine(LArmControl());
        StartCoroutine(RArmControl());
        yield return null;
    }
}
