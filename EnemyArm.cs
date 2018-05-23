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

    public float xOffset;
    public float zOffset;

    public bool endPlace = false;

    [Header("Action")]
    public bool isPlacing = false;
    public GameObject mine;
    public bool isMoving = false;
    public Vector3 attackPosition;
    private bool endPlacing = true;
    public bool posRecoverEnd = true;
    public bool justMove = true;
    public bool isPosRecover = false;

    private Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        myAnimator = child.GetComponent<Animator>();

        //changedPosition = 0.0f;

        //StartCoroutine(PlaceMine(2));
        Random.InitState(randomSeed);
        StartCoroutine(MoveArm());
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<PlayerController>().speed;
        transform.Translate(new Vector3(0.1f, 0, 0) * speed * Time.deltaTime);
        transform.Translate(new Vector3(changedPosition, 0, 0) * Time.deltaTime);
    }

    public IEnumerator MoveArm()
    {
        while (true)
        {
            isMoving = true;
            if (justMove)
            {
                Debug.Log("justmove");
                //마인 미설치
                movingTime = Random.Range(1, 3);
                changedPosition = 5.0f;
                yield return new WaitForSeconds(movingTime);
                changedPosition = -5.0f;
                yield return new WaitForSeconds(movingTime);
                justMove = false;
            }
            else
            {
                Debug.Log("mine");
                //마인 설치
                int count = 0;
                float timeCount = 0;
                xOffset = 10.0f;
                xOffset += Random.Range(0, speed / 10);
                attackPosition = new Vector3(player.transform.position.x + 50, transform.position.y, transform.position.z);
                //공격지점까지 이동
                while (Vector3.Distance(transform.position, attackPosition) >= 3.0f)
                {
                    changedPosition = 20.0f;
                    count++;
                    yield return null;
                }
                endPlace = false;
                myAnimator.SetTrigger("Lane1");
                changedPosition = -0.1f * speed;
                while (!endPlace)
                {
                    timeCount += Time.deltaTime;
                    if (timeCount >= 3)
                    {
                        endPlace = true;
                        break;
                    }
                    yield return null;
                }
                //위치 회복
                changedPosition = 0.0f;
                posRecoverEnd = false;

                if(!posRecoverEnd)
                {
                    StartCoroutine(ArmSpeedRecovery());
                }
                while(!posRecoverEnd)
                {
                    yield return null;
                }
                justMove = true;
            }
            isMoving = false;
        }
    }

    public IEnumerator ArmSpeedRecovery()
    {
        Debug.Log("Recovery Start");
        //posRecoverEnd = false;
        //isPosRecover = true;
        
        while (Vector3.Distance(transform.position, new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z)) > 1.0f)
        {
            if (transform.position.x > player.transform.position.x + xOffset)
            {
                changedPosition = -10.0f - speed/100;
            }
            else
            {
                changedPosition = 10.0f + speed/100;
            }
            yield return null;
        }
        changedPosition = 0.0f;
        posRecoverEnd = true;
        //isPosRecover = false;
        Debug.Log("Recovery End");
    }

    public IEnumerator PlaceMine(Vector3 attackPosition, int lane)
    {
        if (!isMoving)
        {
            isPlacing = true;
            endPlacing = false;
            this.attackPosition = attackPosition;
            Debug.Log("PlaceMine");
            GameObject newMine;
            //Play.animation(time)
            //time 만큼
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
            endPlacing = true;

            yield return null;
        }
    }

    //public void EndPlace()
    //{
    //    endPlace = true;
    //}

    //public IEnumerator PlaceMine(int lane)
    //{
    //    isPlacing = true;
    //    Debug.Log("PlaceMine");
    //    GameObject newMine;
    //    //Play.animation(time)
    //    //time 만큼
    //    yield return new WaitForSeconds(2.5f);
    //    if (lane == 1)
    //    {
    //        myAnimator.SetTrigger("Lane1");
    //        newMine = Instantiate(mine, new Vector3(transform.position.x, 0, -1), new Quaternion(0, 0, 0, 0));
    //    }
    //    else if (lane == 2)
    //    {
    //        myAnimator.SetTrigger("Lane2");
    //        newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 0), new Quaternion(0, 0, 0, 0));
    //    }
    //    else
    //    {
    //        myAnimator.SetTrigger("Lane1");
    //        newMine = Instantiate(mine, new Vector3(transform.position.x, 0, 1), new Quaternion(0, 0, 0, 0));
    //    }
    //    isPlacing = false;
    //}
}
