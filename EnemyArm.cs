using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArm : MonoBehaviour
{

    [Header("SYSTEM")]
    public float changedPosition;
    public GameObject player;
    public float speed;
    private float movingTime;
    public int randomSeed;

    [Header("Action")]
    public bool isPlacing = false;
    public GameObject mine;
    // Use this for initialization
    void Start()
    {
        //changedPosition = 0.0f;
        StartCoroutine(MoveArm());
        StartCoroutine(PlaceMine(2));
        Random.InitState(randomSeed);
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<PlayerController>().speed;
        transform.Translate(new Vector3(0.1f + changedPosition, 0, 0) * speed * Time.deltaTime);
    }

    public IEnumerator MoveArm()
    {
        while (true)
        {
            movingTime = Random.Range(1, 3);
            changedPosition = 0.3f;
            yield return new WaitForSeconds(movingTime);
            changedPosition = -0.3f;
            yield return new WaitForSeconds(movingTime);
        }
    }

    public IEnumerator PlaceMine(int lane)
    {
        while (true)
        {
            isPlacing = true;
            Debug.Log("PlaceMine");
            GameObject newMine;
            //Play.animation(time)
            //time 만큼
            yield return new WaitForSeconds(3.0f);
            if (lane == 1)
            {
                newMine = Instantiate(mine, new Vector3(transform.position.x, 0, -1), new Quaternion(0, 0, 0, 0));
            }
            else if (lane == 2)
            {
                newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 1), new Quaternion(0, 0, 0, 0));
            }
            isPlacing = false;
        }
    }
}
