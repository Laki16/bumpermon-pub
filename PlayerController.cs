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
    private bool isBlockLeft = false;
    private bool isBLockRIght = false;

    [Header("Status")]
    [Range(1, 300)]
    public float speed;
    private float groundCount;
    //최소 자동전진 속도
    public float minSpeed;
    //최고속도
    public float maxSpeed = 240.0f;
    //맵의 끝 지점(가정)
    public long endDistance;

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
    [Range(1, 6)]
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
    bool isBoost = false;

    [Header("System")]
    private bool isMouseDown = false;

    [Header("Collision")]
    //앞에 블럭
    public GameObject forwardBlock;
    //앞에 블럭이 있는지
    public bool isBlockForward = true;
    //블럭이 바뀌었는지 확인용 변수
    public bool isBlockChange = true;
    //경직 시간
    public float stunTime = 0.8f;

    [Header("UI")]
    public Scrollbar nitroBar;
    public Scrollbar speedBar;
    public Scrollbar gearBar;
    public Scrollbar playerBar;
    public Text speedText;
    public Text distanceText;
    public GameObject speedHandle;
    bool isChangeColor;
    bool isCoroutineRunning = false;

    [Header("Animation")]
    public float sprintMultiplier;
    Animator myAnimator;

    [Header("Camera")]
    public GameObject camera;
    public Vector3 offset;
    float startTime;
    float endTime;

    void Start()
    {
        lane = 0;
        groundController = GameObject.FindGameObjectWithTag("GroundController");
        blockController = GameObject.FindGameObjectWithTag("BlockController");
        groundCount = 0.0f;
        speed = minSpeed;

        isChangeColor = false;

        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        //-----------------Ray-----------------------
        SideRayCast();
        //-------------------------------------------
        //-----------------spawnGround---------------
        if (transform.position.x - groundCount >= 0)
        {
            groundCount += SpawnGrounds.groundXSize * 10;
            groundController.GetComponent<SpawnGrounds>().SpawnGround();
        }
        //--------------------------------------------
        //-------------------Ray-----------------------
        Raycast();
        if (!isBlockChange)
        {
            //거리측정
            if (Vector3.Distance(transform.position, forwardBlock.transform.position) < 2.0f)
            {
                //벗어나면 && 충돌하고 회피할때 부스트 방지를 위해 최소 스피드이상일때만(스턴시간 수정시 따라서 적절히 수정필요)
                if (!isBlockForward && speed > minSpeed)
                {
                    //부스트
                    isBlockChange = true;
                    if (speed + 15.0f > maxGearSpeed * currentGear - 1)
                    {
                        speed = maxGearSpeed * currentGear - 1;
                    }
                    else
                    {
                        isBoost = true;
                        Debug.Log("Boost");
                        startTime = Time.time;
                        endTime = startTime + 0.7f;
                        //offset += new Vector3(-1, 0, 0);
                        
                        speed += 15.0f;
                    }
                    if (nitro + 10.0f > 100)
                    {
                        nitro = 100.0f;
                    }
                    else
                    {
                        nitro += 15.0f;
                    }
                }
            }
            else
            {
                //벗어나면
                if (!isBlockForward)
                {
                    //no부스트
                    isBlockChange = true;
                }
            }
        }
        //---------------------------------------------
        //---------Increase & Decrease Speed----------
        if (!useNitro)
        {
            if (isMouseDown)
            {
                myAnimator.SetBool("Sprint", true);
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
                myAnimator.SetBool("Sprint", false);
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
        //------------------기어업-----------------------

        if (speed >= currentGear * maxGearSpeed && isMouseDown && !useNitro && currentGear < maxGear)
        {
            upgradeGear += Time.deltaTime;
            if (upgradeGear > 2.0f)
            {
                currentGear++;
                speedHandle.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            }
            if (!isCoroutineRunning)
            {
                StartCoroutine(BlingSpeedColor());
            }
        }
        else
        {
            StopCoroutine(BlingSpeedColor());
            speedHandle.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            upgradeGear = 0.0f;
        }
        //---------------------------------------------
        //-----------------nitro-----------------------
        if (useNitro)
        {
            myAnimator.SetBool("Roll", true);
            speed = preSpeed + currentGear * 30;
            nitroTime -= Time.deltaTime;
            if (nitroTime < 0)
            {
                myAnimator.SetBool("Roll", false);
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
                if (!isBlockLeft)
                {
                    //Debug.Log("Left");
                    lane++;
                }
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
                if (!isBLockRIght)
                {
                    //Debug.Log("Right");
                    lane--;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }
        //---------------------------------------------
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, lane), Time.deltaTime * swipeSpeed);
        //---------------------UI----------------------
        nitroBar.size = nitro / 100.0f;
        speedBar.size = speed / maxSpeed;
        gearBar.value = (currentGear - 1) / 4.0f;
        playerBar.value = (transform.position.x / endDistance);
        speedText.text = ((int)speed + "km");
        distanceText.text = (((int)transform.position.x + 20) + "");
        //---------------------------------------------

        //-----------------Animations------------------
        sprintMultiplier = 1.0f + speed / 240;
        myAnimator.SetFloat("SprintMultiplier", sprintMultiplier);
        //---------------------------------------------

        //-------------------Camera--------------------
        camera.transform.position = transform.position + offset;
        if (isBoost && Time.time <= startTime + .5f)
        {
            offset -= new Vector3(1.0f, 0, 0) * Time.deltaTime;
        }
        else if(Time.time >= startTime + .5f && Time.time <= endTime)
        {
            offset += new Vector3(2.5f, 0, 0) * Time.deltaTime;
        }
        else
        {
            offset = new Vector3(-2.0f, 1.5f, 0);
        }
        //---------------------------------------------

    }


    void Raycast()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.right);
        //Debug.DrawRay(ray.origin, ray.direction * 3, Color.red, 0.01f);
        RaycastHit hit;
        //space 의 2/3 지점 이내일때 피하면 부스트 따라서, ray는 space만큼 쏘면 피했을때 다른 블럭이 잡히지않음
        //Ray 거리는 최소노드길이(difficulty + space)보다 짧아야함****************
        if (Physics.Raycast(ray, out hit, SpawnBlocks.minDifficulty + SpawnBlocks.space))
        {
            isBlockForward = true;
            forwardBlock = hit.collider.gameObject;
            isBlockChange = false;
        }
        else
        {
            isBlockForward = false;
        }
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
                        if (!isBlockLeft)
                            lane++;
                    }
                }
                else
                {
                    if (transform.position.z > -1)
                    {
                        //Debug.Log("Right");
                        if (!isBLockRIght)
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
                    UseNitro();
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

    IEnumerator BlingSpeedColor()
    {
        isCoroutineRunning = true;
        if (isChangeColor)
        {
            speedHandle.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 150.0f / 255.0f);
            isChangeColor = false;
        }
        else
        {
            speedHandle.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            isChangeColor = true;
        }
        yield return new WaitForSeconds(0.1f);

        isCoroutineRunning = false;
    }
    //----------------충돌------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (!useNitro)
        {
            if (currentGear > 1)
            {
                currentGear--;
            }
            speed = -8.0f;
            StartCoroutine(SpeedRecovery());
        }
    }
    //---------------------------------------------
    IEnumerator SpeedRecovery()
    {
        //경직 시간만큼 기다림
        yield return new WaitForSeconds(stunTime);
        speed += minSpeed;
    }

    void SideRayCast()
    {
        Ray rayLeft = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.forward);
        Ray rayRight = new Ray(transform.position + new Vector3(0, 0.3f, 0), -transform.forward);
        RaycastHit hitLeft;
        RaycastHit hitRight;

        if (Physics.Raycast(rayLeft, out hitLeft, 1.0f))
        {
            isBlockLeft = true;
        }
        else
        {
            isBlockLeft = false;
        }
        if (Physics.Raycast(rayRight, out hitRight, 1.0f))
        {
            isBLockRIght = true;
        }
        else
        {
            isBLockRIght = false;
        }
    }

}
