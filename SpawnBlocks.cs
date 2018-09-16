using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyController;

    [Header("System")]
    public GameManager gameManager;
    private int beforeLane = 0;
    private int nowLane = 0;
    private int iterator = 0;
    private int nodeIterator = 0;
    private int delNodeIterator = 0;
    private bool endSpawn = true;
    private GameObject player;
    private float curLUK;

    [Header("Block")]
    public GameObject block;
    public static float blockXSize = 1.0f;
    //maxBlocks >= maxNodes * maxDifficulty 이어야 함
    private static int maxBlocks = 210;
    private static int maxNodes = 25;
    private int existNode = 0;
    private GameObject[] lane = new GameObject[maxBlocks];
    private GameObject[] node = new GameObject[maxNodes];

    [Header("Difficulty")]
    static public int minSpace = 4;
    static public int maxSpace = 10;
    public int space;
    private int difficulty;
    static public int maxDifficulty = 8;
    //1일시, 마인이 나오는 위치는 노드의 중간위치이므로 사실상 가로로 2칸이 이어진 장애물이 나오게된다. (피하는거 가능한가? 안되면 추후 늘리기 (최소3으로))
    static public int minDifficulty = 1;
    private int beforeDifficulty;

    [Header("Coin")]
    public GameObject genCoin;
    static int maxCoins = 400;
    int coinIterator = 0;
    private GameObject[] genCoins = new GameObject[maxCoins];
    public Material yellow;
    public Material red;

    //Equip
    public ShopManager shopManager;
    private Equip equip;

    void Awake()
    {
        InitBlock();
        InitCoin();
        endSpawn = true;
    }

    void Start()
    {
        player = gameManager.player;
        curLUK = player.GetComponent<Character>().LUK;
        for(int i=0; i<player.GetComponent<EquippedItem>().equippedItem.Count; i++)
        {
            if(player.GetComponent<EquippedItem>().equippedItem[i] != null)
            {
                equip = player.GetComponent<EquippedItem>().equippedItem[i];
                curLUK += equip.LUK;
            }
        }

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
            difficulty = Random.Range(7, maxDifficulty);
        }
        else if (200.0f < player.transform.position.x && player.transform.position.x <= 500.0f)
        {
            difficulty = Random.Range(5, maxDifficulty);
        }
        else if (500.0f < player.transform.position.x && player.transform.position.x <= 1000.0f)
        {
            difficulty = Random.Range(4, 6);
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
        int rand = Random.Range(0, 3);
        if (rand == 0) SpawnCoin();

        //node[nodeIterator] = lane[iterator];
        for (int i = 0; i < difficulty; i++)
        {
            lane[iterator].transform.position = new Vector3(beforeDifficulty, -1, -0.5f);
            lane[iterator].transform.position += new Vector3(blockXSize * i, 0, nowLane);
            lane[iterator].SetActive(true);
            lane[iterator].GetComponent<SkinnedMeshRenderer>().enabled = true;
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

    void InitCoin(){
        for (int i = 0; i < maxCoins; i++)
        {
            genCoins[i] = Instantiate(genCoin, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    void SpawnCoin(){
        //Debug.Log("Spawn Coins");
        int genRandom;
        if (nowLane == -1) genRandom = Random.Range(0, 2);
        else if (nowLane == 1) genRandom = Random.Range(-1, 1);
        else{
            genRandom = Random.Range(0, 2);
            if (genRandom == 0) genRandom = -1;
        }
        for (int i = 0; i < difficulty + space; i++)
        {
            genCoins[coinIterator].GetComponent<Renderer>().enabled = true;
            genCoins[coinIterator].transform.position = new Vector3(beforeDifficulty, .2f, 0);
            genCoins[coinIterator].transform.position += new Vector3(blockXSize * i, 0, genRandom);

            float collorRand = Random.Range(0f, 100f);
            if (collorRand <= curLUK)
            {
                genCoins[coinIterator].GetComponent<Renderer>().material = red;
                genCoins[coinIterator].GetComponent<CoinMoving>().isRed = false;
            }
            else
            {
                genCoins[coinIterator].GetComponent<Renderer>().material = yellow;
                genCoins[coinIterator].GetComponent<CoinMoving>().isRed = true;
            }

            genCoins[coinIterator].SetActive(true);
            coinIterator++;
            coinIterator %= maxCoins;
        }
    }
}
