using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Control")]
    GameObject groundController;
    GameObject blockController;
    private Vector2 initialPos;
    private int lane;
    public float swipeSpeed = 10.0f;
    private bool isKeyPressed = false;

    [Header("Status")]
    [Range(1, 300)]
    public float speed;
    private float groundCount;
    //최소 자동전진 속도
    public float minSpeed;
    //최고속도
    public float maxSpeed = 240.0f;

    [Header("Gear System")]
    [Tooltip("현재 기어")]
    public int currentGear;
    [Tooltip("기어별 상한속도")]
    public float maxGearSpeed;
    [Tooltip("기어 풀리는 변수")]
    public float unGear;
    [Tooltip("max 속도일때 이 시간만큼 유지하면 기어업")]
    public float upgradeGear;
    [Tooltip("최대 기어")]
    [Range(1,6)]
    public int maxGear;
    [Tooltip("속도 증가 가중치")]
    public float increaseWeight;
    [Tooltip("속도 감소 가중치")]
    public float decreaseWeight;

    [Header("Nitro System")]
    public float nitro = 0;
    public bool isNitro = false;
    public bool useNitro = false;
    float preSpeed;
    int preGear;
    public float nitroTime = 0.0f;

    [Header("System")]
    private bool isMouseDown = false;

    [Header("UI")]
    public Button nitroBtn;
    public Text speedText;
    void Start()
    {
        lane = 0;
        groundController = GameObject.FindGameObjectWithTag("GroundController");
        blockController = GameObject.FindGameObjectWithTag("BlockController");
        groundCount = 0.0f;
        speed = minSpeed;
        nitro = 90;
    }

    void Update()
    {
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        //-----------------spawnGround---------------
        if (transform.position.x - groundCount >= 0)
        {
            groundCount += SpawnGrounds.groundXSize * 10;
            groundController.GetComponent<SpawnGrounds>().SpawnGround();
        }
        //--------------------------------------------
        //---------Increase & Decrease Speed----------
        if (!useNitro)
        {
            if (isMouseDown)
            {
                unGear = 10.0f;
                if (nitro < 100)
                {
                    nitro += 10 * speed / maxSpeed * Time.deltaTime;
                }
                else
                {
                    isNitro = true;
                }
                if (speed <= maxGearSpeed * currentGear)
                {
                    speed += increaseWeight * currentGear * Time.deltaTime;
                }
            }
            else
            {
                if (speed >= minSpeed)
                {
                    speed -= decreaseWeight * currentGear * Time.deltaTime;
                }
                if (unGear > 0 && currentGear > 1)
                {
                    unGear -= currentGear * Time.deltaTime;
                    if (unGear <= 0)
                    {
                        if (currentGear > 0)
                        {
                            currentGear--;
                        }
                        unGear = 10.0f;
                    }
                }
            }
        }
        //---------------------------------------------
        //------------------기어업---------------------
        if (speed >= currentGear * maxGearSpeed && isMouseDown && !useNitro)
        {
            upgradeGear += Time.deltaTime;
            if (upgradeGear > 2.0f)
            {
                if (currentGear < maxGear)
                {
                    currentGear++;
                }
            }
        }
        else
        {
            upgradeGear = 0.0f;
        }
        //---------------------------------------------

        //-----------------nitro-----------------------
        if (useNitro)
        {
            speed = preSpeed + currentGear * 30;
            nitroTime -= Time.deltaTime;
            if(nitroTime < 0)
            {
                useNitro = false;
                isNitro = false;
                nitro = 0;
                speed = preSpeed;
                currentGear = preGear;
                nitroTime = 5.0f;
            }
        }
        //---------------------------------------------

        //--------------- mouse control ---------------
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            Calculate(Input.mousePosition);
        }
        //---------------------------------------------
        //--------------- keyboard control ------------
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isKeyPressed)
        {
            isKeyPressed = true;
            if (transform.position.z < 1)
            {
                //Debug.Log("Left");
                lane++;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !isKeyPressed)
        {
            isKeyPressed = true;
            if (transform.position.z > -1)
            {
                //Debug.Log("Right");
                lane--;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }
        //---------------------------------------------
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, lane), Time.deltaTime * swipeSpeed);

        //---------------------UI----------------------
        if(isNitro){
            nitroBtn.interactable = true;
        }else{
            nitroBtn.interactable = false;
        }
        speedText.text = ((int)speed+"km");
        //---------------------------------------------

    }

    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 0 || disY > 0)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    if (transform.position.z < 1)
                    {
                        //Debug.Log("Left");
                        lane++;
                    }
                }
                else
                {
                    if (transform.position.z > -1)
                    {
                        //Debug.Log("Right");
                        lane--;
                    }
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    // 밑으로 드래그
                    //Debug.Log("Down");
                }
                else
                {
                    //위로 드래그
                    //Debug.Log("Up");
                    //if(isNitro)
                    //{
                    //    //UseNitro();
                    //}
                }
            }
        }
    }

    public void UseNitro()
    {
        if (isNitro)
        {
            preSpeed = speed;
            preGear = currentGear;
            useNitro = true;
        }
    }
}
