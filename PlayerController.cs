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
    private bool isAttack = false;

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
    //목숨 : 벽에 부딪힐 때와 지뢰를 밟았을 때 줄어든다.
    public int live = 3;
    public bool checkDead = false;
    public bool isShield = false;

    [Header("FX")]
    private bool moveLeft = false;
    public GameObject shieldFX;
    public GameObject shieldEffect;
    public GameObject sprintFX;
    public GameObject justActionLeftFX;
    public GameObject justActionRightFX;
    public GameObject gearUpFX_1;
    public GameObject gearDownFX_1;
    public GameObject gearDownFX_2;
    public GameObject nitroFX;


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
    public GameManager gameManager;
    public EnemyArm leftEnemyArm;
    public EnemyArm rightEnemyArm;
    public GameObject enemyController;

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
    //public Scrollbar nitroBar;
    public Image speedBar;
    public Scrollbar gearBar;
    public Text speedText;
    public Text distanceText;
    bool isChangeColor;
    bool isCoroutineRunning = false;
    public GameObject orb;
    //public Button startBtn;
    //public Button optionBtn;

    [Header("Animation")]
    public float sprintMultiplier;
    Animator myAnimator;

    [Header("Camera")]
    public GameObject camera;
    public CameraShake cameraShake;
    public Vector3 offset;
    float startTime;
    float endTime;

    [Header("Attack")]
    private float attackTime;

    //[Header("PostProcessing")]
    //public PostProcessingProfile profile;
    //BloomModel.Settings bloomsettings;

    void Start()
    {
        lane = 0;
        groundController = GameObject.FindGameObjectWithTag("GroundController");
        blockController = GameObject.FindGameObjectWithTag("BlockController");
        groundCount = 0.0f;
        speed = minSpeed;

        isChangeColor = false;

        myAnimator = GetComponent<Animator>();
        //bloomsettings = profile.bloom.settings;
        //bloomsettings.bloom.intensity = 0;
        GameObject shield;
        shield = Instantiate(shieldFX, transform.position + new Vector3(0, 0.3f, 0), transform.rotation, transform);
        shieldEffect = shield;
        shieldEffect.SetActive(false);
    }

    void Update()
    {
        if (!checkDead && live <= 0)
        {
            checkDead = true;
            speed = 0.0f;
            StartCoroutine(enemyController.GetComponent<EnemyController>().GameOverWait());
            myAnimator.Play("Die");
            leftEnemyArm.Laugh();
            rightEnemyArm.Laugh();
            gameManager.GameOver();
        }

        if (live > 0)
        {
            transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed);
        }
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
        //-------------------Ray----------------------
        if(!useNitro)
        { 
        Raycast();
            if (!isBlockChange)
            {
                //거리측정
                if (forwardBlock != null)
                {
                    if (Vector3.Distance(transform.position, forwardBlock.transform.position) < 2.0f)
                    {
                        //벗어나면 && 충돌하고 회피할때 부스트 방지를 위해 최소 스피드이상일때만(스턴시간 수정시 따라서 적절히 수정필요)
                        if (!isBlockForward && speed > minSpeed)
                        {
                            //부스트
                            isBlockChange = true;
                            if(moveLeft)
                            {
                                StartCoroutine(LeftFX());
                            }
                            else
                            {
                                StartCoroutine(RightFX());
                            }
                            if (speed + 15.0f > maxGearSpeed * currentGear - 1)
                            {
                                speed = maxGearSpeed * currentGear - 1;
                                startTime = Time.time;
                                endTime = startTime + 0.7f;
                            }
                            else
                            {
                                //isBoost = true;
                                startTime = Time.time;
                                endTime = startTime + 0.7f;
                                //offset += new Vector3(-1, 0, 0);

                                speed += 15.0f;
                                //enemy.GetComponent<EnemyController>().BoostSpeedDown();
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
                        //버그(부딪히고 저스트액션 했을때, 노부스트되어야함)
                        else if(!isBlockForward && speed < minSpeed)
                        {
                            isBlockChange = true;
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
            }
        }
        //--------------------------------------------
        //---------Increase & Decrease Speed----------
        if (!useNitro)
        {
            if (isMouseDown)
            {
                myAnimator.SetBool("Sprint", true);
                gearDownFX_1.SetActive(false);
                //sprintFX.SetActive(true);
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
                //sprintFX.SetActive(false);
                if (speed >= minSpeed)
                {
                    speed -= decreaseWeight * currentGear * Time.deltaTime;
                }
                if (unGear > 0 && currentGear > 1)
                {
                    unGear -= currentGear * Time.deltaTime;
                    gearDownFX_1.SetActive(true);
                    if (unGear <= 0)
                    {
                        if (currentGear > 0)
                        {
                            StartCoroutine(GearDownFX());
                            currentGear--;
                        }
                        unGear = 10.0f;
                        gearDownFX_1.SetActive(false);
                    }
                }
            }
        }
        //---------------------------------------------
        //------------------기어업-----------------------

        if (speed >= currentGear * maxGearSpeed && isMouseDown && !useNitro && currentGear < maxGear)
        {
            upgradeGear += Time.deltaTime;
            gearUpFX_1.SetActive(true);
            if (upgradeGear > 2.0f)
            {
                currentGear++;
                speedBar.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            }
            if (!isCoroutineRunning)
            {
                StartCoroutine(BlingSpeedColor());
            }
        }
        else
        {
            gearUpFX_1.SetActive(false);
            StopCoroutine(BlingSpeedColor());
            speedBar.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            upgradeGear = 0.0f;
        }
        //---------------------------------------------
        //-----------------nitro-----------------------
        if (useNitro)
        {
            //Ray shockwaveRay = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.forward);
            //RaycastHit shockwaveHit;
            //if (Physics.Raycast(shockwaveRay, out shockwaveHit, 4.0f, 1 << 12))
            //{
            //    if (nitroTime < 0.3f)
            //        shockwaveHit.collider.gameObject.SetActive(false);
            //}
            //myAnimator.SetBool("Roll", true);
            speed = preSpeed + currentGear * 30;
            nitroTime -= Time.deltaTime;
            //if (!enemy.GetComponent<EnemyController>().isSpeedDown)
            //{
            //    enemy.GetComponent<EnemyController>().StartCoroutine(enemy.GetComponent<EnemyController>().NitroSpeedDown(4));
            //}
            nitro -= Time.deltaTime * 25;

            if (nitroTime <= 0)
            {
                nitroFX.SetActive(false);
                //myAnimator.SetBool("Roll", false);
                //myAnimator.Play("Idle");
                isNitro = false;
                nitro = 0;
                speed = preSpeed;
                currentGear = preGear;
                nitroTime = 4.3f;

                if (!isNitroShockwave)
                {
                    StartCoroutine(NitroShockwave());
                }
            }
        }
        //---------------------------------------------
        //----------------- Attack --------------------
        if (isAttack)
        {
            attackTime -= Time.deltaTime;
            Ray attackRay = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.forward);
            RaycastHit attackHit;
            //13 : mine Layer
            if (Physics.Raycast(attackRay, out attackHit, 2.0f, 1 << 13))
            {
                //폭발
                attackHit.collider.gameObject.SetActive(false);
            }
            if (attackTime <= 0)
            {
                isAttack = false;
            }
        }
        //---------------------------------------------
        //--------------- touch control ---------------
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                isMouseDown = true;
                initialPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isMouseDown = false;
                Calculate(touch.position);
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
            if (transform.position.z < 0.9f)
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
            if (transform.position.z > -0.9f)
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
        orb.GetComponent<OrbFill>().Fill = nitro / 100.0f;
        speedBar.fillAmount = speed / maxSpeed;
        gearBar.value = (currentGear - 1) / 4.0f;
        speedText.text = ((int)speed + "km");
        distanceText.text = (((int)transform.position.x + 20) + "");
        //---------------------------------------------

        //-----------------Animations------------------
        sprintMultiplier = 1.0f + speed / 240;
        myAnimator.SetFloat("SprintMultiplier", sprintMultiplier);
        //---------------------------------------------

        //-------------------Camera--------------------
        camera.transform.position = transform.position + offset;
        //if (isBoost && Time.time <= startTime + .5f)
        if (Time.time <= startTime + .5f)
        {
            offset -= new Vector3(1.0f, 0, 0) * Time.deltaTime * ((20 + speed) / 200);
        }
        else if (Time.time >= startTime + .5f && Time.time <= endTime)
        {
            offset += new Vector3(2.5f, 0, 0) * Time.deltaTime * ((20 + speed) / 200);
        }
        else
        {
            offset = new Vector3(-2.0f, 1.5f, 0);
        }
        //---------------------------------------------

        //---------------PostProcessing----------------
        //if(isBoost || useNitro){
        //    bloomsettings.bloom.intensity = speed / 100.0f;
        //}
        //---------------------------------------------
    }

    public bool isNitroShockwave = false;
    public IEnumerator NitroShockwave()
    {
        isNitroShockwave = true;
        Collider[] cols = Physics.OverlapSphere(transform.position, 20.0f + speed / 15, 1 << 12);
        if (cols != null)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].gameObject.GetComponent<Block>().StartCoroutine(cols[i].gameObject.GetComponent<Block>().SplitMesh(true));
            }
        }
        //useNitro = false;
        isNitroShockwave = false;
        StartCoroutine(AfterNitro());
        yield return null;
    }

    void Raycast()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.right);
        //Debug.DrawRay(ray.origin, ray.direction * 3, Color.red, 0.01f);
        RaycastHit hit;
        //space 의 2/3 지점 이내일때 피하면 부스트 따라서, ray는 space만큼 쏘면 피했을때 다른 블럭이 잡히지않음
        //Ray 거리는 최소노드길이(minDifficulty + minSpace)보다 짧아야함****************
        //if (Physics.Raycast(ray, out hit, SpawnBlocks.minDifficulty + SpawnBlocks.minSpace))
        if (Physics.Raycast(ray, out hit, SpawnBlocks.minDifficulty + SpawnBlocks.minSpace, 1 << 12))
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
        if (!checkDead)
        {
            float disX = Mathf.Abs(initialPos.x - finalPos.x);
            float disY = Mathf.Abs(initialPos.y - finalPos.y);
            if (disX > 0 || disY > 0)
            {
                if (disX > disY)
                {
                    if (initialPos.x > finalPos.x)
                    {
                        if (transform.position.z < 0.9f)
                        {
                            //Debug.Log("Left");
                            if (!isBlockLeft)
                            {
                                lane++;
                                moveLeft = true;
                            }
                        }
                    }
                    else
                    {
                        if (transform.position.z > -0.9f)
                        {
                            //Debug.Log("Right");
                            if (!isBLockRIght)
                            {
                                lane--;
                                moveLeft = false;
                            }
                        }
                    }
                }
                else
                {
                    if (initialPos.y > finalPos.y)
                    {
                        // 밑으로 드래그
                        //Debug.Log("Down");
                        if (!useNitro)
                        {
                            UseNitro();
                        }
                    }
                    else
                    {
                        //위로 드래그
                        //Debug.Log("Up");
                        //myAnimator.ResetTrigger("Idle");
                        //myAnimator.SetTrigger("Attack");
                        Attack();
                    }
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
            myAnimator.Play("Roll");
            nitroFX.SetActive(true);
            leftEnemyArm.Surprise();
            rightEnemyArm.Surprise();
        }
    }

    public void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            attackTime = 1.0f;
            myAnimator.SetTrigger("Attack");
        }
    }
    IEnumerator BlingSpeedColor()
    {
        isCoroutineRunning = true;
        if (isChangeColor)
        {
            speedBar.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 150.0f / 255.0f);
            isChangeColor = false;
        }
        else
        {
            speedBar.GetComponent<Image>().color = new Color(220.0f / 255.0f, 60.0f / 255.0f, 10.0f / 255.0f, 255.0f / 255.0f);
            isChangeColor = true;
        }
        yield return new WaitForSeconds(0.1f);

        isCoroutineRunning = false;
    }
    //----------------충돌------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Item"))
        {
            if (!useNitro)
            {
                if (!isShield)
                {
                    if (live > 0)
                    {
                        live--;
                        gameManager.LiveDown();
                    }
                    myAnimator.Play("Damage");
                    if (currentGear > 1)
                    {
                        currentGear--;
                    }
                    speed = -8.0f;
                    StartCoroutine(SpeedRecovery());
                }
                else
                {
                    myAnimator.Play("Damage");
                    speed = -8.0f;
                    StartCoroutine(SpeedRecovery());
                    isShield = false;
                    shieldEffect.SetActive(false);
                }
            }
        }
    }

    public void GetShield()
    {
        isShield = true;
        shieldEffect.SetActive(true);
    }

    public IEnumerator GetNitro()
    {
        if (!useNitro)
        {
            float nowNitro = nitro;
            isNitro = true;
            UseNitro();
            while (isNitro || useNitro)
            {
                yield return null;
            }
            nitro += nowNitro;
        }
    }
    //---------------------------------------------
    IEnumerator SpeedRecovery()
    {
        //경직 시간만큼 기다림
        yield return new WaitForSeconds(stunTime);
        if (!checkDead)
        {
            speed += minSpeed;
        }
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

    IEnumerator AfterNitro()
    {
        yield return new WaitForSeconds(0.5f);
        useNitro = false;
    }

    void SmashCameraEffect()
    {
        float magnitude = speed / 1500.0f;
        StartCoroutine(cameraShake.Smash(.5f, magnitude));
    }

    IEnumerator LeftFX()
    {
        justActionLeftFX.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        justActionLeftFX.SetActive(false);
    }
    IEnumerator RightFX()
    {
        justActionRightFX.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        justActionRightFX.SetActive(false);
    }
    IEnumerator GearDownFX()
    {
        gearDownFX_2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gearDownFX_2.SetActive(false);
    }
}
