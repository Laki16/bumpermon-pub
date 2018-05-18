using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    [Header("System")]
    GameObject player;
    float minSpeed;
    float maxSpeed;
    long endDistance;
    float speed;

    [Header("UI")]
    public Scrollbar enemyBar;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerController");
        minSpeed = player.GetComponent<PlayerController>().minSpeed;
        maxSpeed = player.GetComponent<PlayerController>().maxSpeed;
        endDistance = player.GetComponent<PlayerController>().endDistance;

        speed = minSpeed;
        InvokeRepeating("SpeedUp", 0, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        enemyBar.value = (transform.position.x / endDistance);
	}

    void SpeedUp()
    {
        if (speed < maxSpeed) speed++;
    }

}
