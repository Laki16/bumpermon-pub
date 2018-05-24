using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyL;
    public GameObject enemyR;
    public Vector3 attackPosition;
    public bool isAttack = false;
    public GameObject enemyController;

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
    static public int minSpace = 3;
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
        //player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(Attack());
    }

    void Update()
    {
        if (endSpawn)
        {
            if (lane[iterator].transform.position.x + 25.0f < player.transform.position.x)
            {
                SpawnBlock();
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
            difficulty = Random.Range(5, 12);
        }
        else if (500.0f < player.transform.position.x && player.transform.position.x <= 1000.0f)
        {
            difficulty = Random.Range(4, 8);
        }
        else if (1000.0f < player.transform.position.x && player.transform.position.x <= 1500.0f)
        {
            difficulty = Random.Range(3, 5);
        }
        else
        {
            difficulty = Random.Range(minDifficulty, 4);
        }
    }

    public void SpawnBlock()
    {
        endSpawn = false;
        ComputeLane();
        beforeDifficulty += difficulty + space;
        SetDifficulty();
        SetSpace();

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

        //------------------- Attack --------------------------
        enemyController.GetComponent<EnemyController>().MineRequest(beforeDifficulty, difficulty, nowLane);
        //if (!isAttack)
        //{
        //    Debug.Log("Attack Start");
        //    isAttack = true;
        //    //---------------공격 레인 정하기-----------------------
        //    //1레인에 노드가 생겼다면
        //    if (nowLane == 1)
        //    {
        //        StartCoroutine(Attack(false, 2, 4));
        //    }
        //    //2레인에 노드가 생겼다면
        //    else if (nowLane == 0)
        //    {
        //        int num;
        //        num = Random.Range(1, 3);
        //        if (num == 1)
        //        {
        //            //3번레인에 생성(-1)
        //            StartCoroutine(Attack(false, 1, 2));
        //        }
        //        //num == 2면
        //        else
        //        {
        //            //1번레인에 생성(+1)
        //            num = 3;
        //            StartCoroutine(Attack(true, 3, 4));
        //        }
        //    }
        //    //3번레인에 노드가 생겼다면
        //    else
        //    {
        //        int num;
        //        num = Random.Range(1, 3);
        //        if (num == 1)
        //        {
        //            //1번레인에 생성(-1)
        //            StartCoroutine(Attack(true, 1, 2));
        //        }
        //        else
        //        {
        //            //2번레인에 생성(0)
        //            StartCoroutine(Attack(false, 2, 3));
        //        }
        //    }
            //----------------------------------------------------------
        //}
        endSpawn = true;
    }
    //--------------------------------

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

    //IEnumerator Attack(bool left, int n, int m)
    //{
    //    //right
    //    if (!left)
    //    {
    //        if (!enemyR.GetComponent<EnemyArm>().isMoving)
    //        {
    //            Debug.Log("Right");
    //            //공격플래그
    //            enemyR.GetComponent<EnemyArm>().justMove = false;

    //            int num;
    //            int lane;
    //            //1,2 레인중 하나
    //            num = Random.Range(n, m);
    //            if(num == 1)
    //            {
    //                lane = 1;
    //            }
    //            else if(num == 2)
    //            {
    //                lane = 0;
    //            }
    //            else
    //            {
    //                lane = -1;
    //            }
    //            //노드의 가운데 위치, 0, lane 전달
    //            enemyR.GetComponent<EnemyArm>().minePosition = new Vector3(beforeDifficulty + (difficulty / 2), 0, lane);
    //            enemyR.GetComponent<EnemyArm>().mineLane = num;
    //            //확실한 전달을 위한 딜레이 / 플래그전달
    //            yield return new WaitForSeconds(0.1f);
    //            enemyR.GetComponent<EnemyArm>().isAttackInfoChanged = true;

    //            Debug.Log(num);
    //        }
    //    }
    //    //left
    //    else
    //    {
    //        if (!enemyL.GetComponent<EnemyArm>().isMoving)
    //        {

    //            Debug.Log("Left");
    //            //공격플래그
    //            enemyL.GetComponent<EnemyArm>().justMove = false;

    //            int num;
    //            //1,2 레인중 하나
    //            num = Random.Range(n, m);
    //            //노드의 가운데 위치, 0, lane 전달
    //            enemyL.GetComponent<EnemyArm>().minePosition = new Vector3(beforeDifficulty + (difficulty / 2), 0, num - 2);
    //            enemyL.GetComponent<EnemyArm>().mineLane = num;
    //            //확실한 전달을 위한 딜레이 / 플래그전달
    //            yield return new WaitForSeconds(0.1f);
    //            enemyL.GetComponent<EnemyArm>().isAttackInfoChanged = true;

    //            Debug.Log(num);
    //        }
    //    }
    //    isAttack = false;
    //}
}
