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

    [Header("Action")]
    public GameObject mine;
    public bool isMoving = false;
    public Vector3 attackPosition;
    public Vector3 minePosition;
    public bool posRecoverEnd = true;
    public bool justMove = true;
    private bool endCountAnimationTime = true;
    public bool endPlace = false;
    public bool isAttackInfoChanged = false;
    public int mineLane = 0;

    private Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        myAnimator = child.GetComponent<Animator>();
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
            Debug.Log("justMove = " + justMove);
            isMoving = true;
            //마인 미설치
            if (justMove)
            {
                movingTime = Random.Range(1, 3);
                changedPosition = 5.0f;
                yield return new WaitForSeconds(movingTime);
                changedPosition = -5.0f;
                yield return new WaitForSeconds(movingTime);
                changedPosition = 0.0f;
                //justMove = false;
            }
            //마인 설치
            else
            {
                endPlace = false;
                isAttackInfoChanged = false;
                if (!endPlace)
                {
                    Debug.Log("PlaceMine");
                    StartCoroutine(PlaceMine());
                }
                while(!endPlace)
                {
                    yield return null;
                }
                Debug.Log("PlaceComplete");
                //위치 회복
                changedPosition = 0.0f;
                posRecoverEnd = false;
                if (!posRecoverEnd)
                {
                    StartCoroutine(ArmSpeedRecovery());
                }
                while (!posRecoverEnd)
                {
                    yield return null;
                }
                justMove = true;
            }
            isMoving = false;
            //공격을 위한 움직임 휴식(3초)
            yield return new WaitForSeconds(5.0f);
        }
    }

    public IEnumerator ArmSpeedRecovery()
    {
        xOffset = 10.0f;
        xOffset += Random.Range(0, speed / 10);
        while (Vector3.Distance(transform.position, new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z)) > 1.0f)
        {
            if (transform.position.x > player.transform.position.x + xOffset)
            {
                changedPosition = -10.0f - speed / 100;
            }
            else
            {
                changedPosition = 10.0f + speed / 100;
            }
            yield return null;
        }
        changedPosition = 0.0f;
        posRecoverEnd = true;
        Debug.Log("Recovery End");
    }

    public IEnumerator PlaceMine()
    {
        //값 전달 확인용 딜레이
        while(!isAttackInfoChanged)
        {
            yield return null;
        }
        //바뀐 minePosition을 적용
        attackPosition = new Vector3(minePosition.x, transform.position.y, transform.position.z);
        //공격 지점까지 이동 (오차 : 1.0f)
        while (Vector3.Distance(transform.position, attackPosition) >= 1.0f)
        {
            changedPosition = 20.0f;
            yield return null;
        }
        //공격 지점도착후 공격 애니메이션 실행(3초간)
        if (mineLane == 2)
        {
            myAnimator.SetTrigger("Lane2");
        }
        else
        {
            myAnimator.SetTrigger("Lane1");
        }
        //공격 동안 멈추기(3초간)
        changedPosition = -0.1f * speed;
        //3초 카운트
        float timeCount = 0;
        endCountAnimationTime = false;
        while (!endCountAnimationTime)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= 3)
            {
                endCountAnimationTime = true;
                break;
            }
            yield return null;
        }
        //마인 생성
        GameObject newMine;
        newMine = Instantiate(mine, minePosition, new Quaternion(0, 0, 0, 0));
        //PlaceMine() 종료 플래그
        endPlace = true;
    }
}
