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
    public GameObject child;

    [Header("Action")]
    public bool isPlacing = false;
    public GameObject mine;

    private Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        myAnimator = child.GetComponent<Animator>();

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
        isPlacing = true;
        Debug.Log("PlaceMine");
        GameObject newMine;
        //Play.animation(time)
        //time 만큼
        yield return new WaitForSeconds(2.5f);
        if (lane == 1)
        {
            myAnimator.SetTrigger("Lane1");
            newMine = Instantiate(mine, new Vector3(transform.position.x, 0, -1), new Quaternion(0, 0, 0, 0));
        }
        else if (lane == 2)
        {
            myAnimator.SetTrigger("Lane2");
            newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 0), new Quaternion(0, 0, 0, 0));
        }
        else
        {
            myAnimator.SetTrigger("Lane1");
            newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 1), new Quaternion(0, 0, 0, 0));
        }
        isPlacing = false;

    }
}
