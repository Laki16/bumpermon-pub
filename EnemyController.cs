using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [Header("System")]
    GameObject player;
    float minSpeed;
    float maxSpeed;
    long endDistance;
    float speed;
    float distanceBetPlayer;
    float mySpeed;
    public float speedWeight;
    public bool isSpeedDown = false;

    [Header("UI")]
    public Scrollbar enemyBar;
    public Scrollbar playerBar;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController");
        minSpeed = player.GetComponent<PlayerController>().minSpeed;
        maxSpeed = player.GetComponent<PlayerController>().maxSpeed;
        endDistance = player.GetComponent<PlayerController>().endDistance;

        speed = minSpeed;
        //InvokeRepeating("SpeedUp", 0, 1.0f);
        mySpeed = 0.0f;
        speedWeight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        speedWeight += Time.deltaTime;
        mySpeed = (speedWeight / 60) * (speedWeight / 60);
        speed = player.GetComponent<PlayerController>().speed + mySpeed;
        distanceBetPlayer = player.transform.position.x - transform.position.x;
        enemyBar.value = distanceBetPlayer / 1000;
        playerBar.value = distanceBetPlayer / 1000;
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        //enemyBar.value = (transform.position.x / endDistance);
    }
    public void BoostSpeedDown()
    {
        speedWeight -= 1.0f;
        Debug.Log("BoostSpeedDown");
    }
    public IEnumerator NitroSpeedDown(int n)
    {
        isSpeedDown = true;
        while (n-- > 0)
        {
            speedWeight -= 2.0f;
            yield return new WaitForSeconds(1.0f);
        }
        isSpeedDown = false;
    }
    //void SpeedUp()
    //{
    //    if (speed < maxSpeed) speed++;
    //}

}
