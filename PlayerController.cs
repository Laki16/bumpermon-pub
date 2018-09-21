using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Score")]
    public int nowCombo;
    private static float comboResetTime = 3.0f;
    private float comboTime = comboResetTime;

    [Header("Control")]
    public GameObject groundController;
    public GameObject blockController;
    private Vector2 initialPos;
    private int lane;
    public float swipeSpeed;
    private bool isKeyPressed = false;
    private bool isBlockLeft = false;
    private bool isBlockRight = false;
    private bool isAttack = false;

    [Header("Status")]
    [Range(1, 300)]
    public float speed;
    private float groundCount;
    //최소 속도
    public float minSpeed;
    //최고속도
    public float maxSpeed;
    //최소 자동전진 속도
    public float minAutoSpeed;
    //맵의 끝 지점(가정)
    public long endDistance;
    public bool checkDead = false;
    private bool isShield = false;
    //부딪혔을때 속도 제어용 변수
    private float damagedSpeed = 1.0f;

    [Header("FX")]
    private bool moveLeft = false;
    public GameObject shieldFX;
    public GameObject shieldEffect;
    public GameObject justActionLeftFX;
    public GameObject justActionRightFX;
    public GameObject nitroFX;

    [Header("SFX")]
    public GameObject soundManager;

    [Header("Nitro System")]
    public float nitro;
    private bool isNitro;
    public bool useNitro;
    private float preSpeed;
    //int preGear;
    public float nitroEarnSize;
    public float bombSize;
    public float nitroSpeed;
    public float nitroTime;
    bool isBoost = false;

    [Header("System")]
    private bool isMouseDown = false;
    public GameManager gameManager;
    public EnemyArm leftEnemyArm;
    public EnemyArm rightEnemyArm;
    public GameObject enemyController;

    [Header("Collision")]
    //앞에 블럭
    private GameObject forwardBlock;
    //앞에 블럭이 있는지
    private bool isBlockForward;
    //블럭이 바뀌었는지 확인용 변수
    private bool isBlockChange;
    //경직 시간
    public float stunTime;
    public bool ghostMode;

    [Header("UI")]
    public Image speedBar;
    public Scrollbar gearBar;
    public Text speedText;
    int tempSpeed;
    public Text distanceText;
    bool isChangeColor;
    bool isCoroutineRunning = false;
    public GameObject orb;
    public GameObject comboL;
    public GameObject comboR;
    public GameObject scoreUI;
    public GameObject comboM;
    public GameObject hpBar;
    public int combo;

    [Header("Animation")]
    private float sprintMultiplier;
    public Animator myAnimator;

    [Header("Camera")]
    public GameObject camera;
    public CameraShake cameraShake;
    public Vector3 offset;
    float startTime;
    float endTime;
    bool camMoving = false;
    private Vector3 velocity = Vector3.zero;

    [Header("Attack")]
    private float attackTime;

    //Character
    private Character character;
    public float curHP;
    private float curDEF;
    private float curSTR;
    private float tm = 1; //give life per 1000m 
    bool isStun = false;

    //Equip
    private Equip equip;

    void Start()
    {
        //Character Initial Settings
        character = GetComponent<Character>();
        minSpeed = character.SPD;
        maxSpeed = minSpeed + 100;
        speed = minSpeed;
        minAutoSpeed = minSpeed;
        curHP = character.HP;
        StartCoroutine(HPSetting());
        //Character Initial Nitro Settings
        nitroEarnSize = character.nitroEarnSize;
        bombSize = character.bombSize;
        nitroSpeed = character.nitroSpeed;

        lane = 0;
        groundCount = 0.0f;

        isChangeColor = false;
        
        myAnimator = GetComponent<Animator>();
        if(myAnimator == null)
        {
            Debug.Log("Find to child anim");
            myAnimator = GetComponentInChildren<Animator>();
        }

        GameObject shield;
        shield = Instantiate(shieldFX, transform.position + new Vector3(0, 0.3f, 0), transform.rotation, transform);
        shieldEffect = shield;
        shieldEffect.SetActive(false);
    }

    void Update()
    {
        if(speed <= minAutoSpeed && !isStun){
            speed = Mathf.Lerp(speed, minAutoSpeed, minAutoSpeed/100 * Time.deltaTime);
        }

        if (!checkDead && curHP <= 0)
        {
            checkDead = true;
            speed = 0.0f;
            StartCoroutine(enemyController.GetComponent<EnemyController>().GameOverWait());
            myAnimator.Play("Die");
            leftEnemyArm.Laugh();
            rightEnemyArm.Laugh();
            if (gameManager != null) gameManager.GameOver();
        }

        if (curHP > 0)
        {
            if(speed < minAutoSpeed)
            {
                speed += 1.0f * Time.deltaTime;
            }
            transform.Translate(new Vector3(0.1f, 0, 0) * Time.deltaTime * speed * damagedSpeed);
        }
        //-----------------spawnGround---------------
        if (transform.position.x - groundCount >= 0)
        {
            groundCount += SpawnGrounds.groundXSize * 10;
            groundController.GetComponent<SpawnGrounds>().SpawnGround();
            minAutoSpeed += 5.0f;
        }
        //--------------------------------------------
        //-------------------Ray----------------------
        if (!useNitro)
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
                            if (moveLeft)
                            {
                                StartCoroutine(LeftFX());
                                StartCoroutine(ComboUp());
                            }
                            else
                            {
                                StartCoroutine(RightFX());
                                StartCoroutine(ComboUp());
                            }
                            //밸런스를 위해 부스트를 사용할 때 항상 5만큼 속도를 늘려주는 대신 기본적으로 줄어드는 속도를 증가시켰음
                            if (!camMoving)
                            {
                                endTime = Time.time + 0.5f;
                            }
                            camMoving = true;
                            speed += 3.0f;
                            if(nitro < 100) nitro += nitroEarnSize;
                            if (nitro > 100) nitro = 100;
                        }
                        //버그(부딪히고 저스트액션 했을때, 노부스트되어야함)
                        else if (!isBlockForward && speed < minSpeed)
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
                if (nitro < 100)
                {
                    nitro += 2 * speed / maxSpeed * Time.deltaTime;
                }
                else
                {
                    isNitro = true;
                }
                if (speed <= maxSpeed)
                {
                    speed += Mathf.Pow(speed, 0.3f) * Time.deltaTime;
                }
            }
            else
            {
                myAnimator.SetBool("Sprint", false);
                if (speed >= minAutoSpeed)
                {
                    //속도를 더 빠르게 감소시킨다. 누르고 있지 않으면 답답하게끔
                    speed -= Mathf.Pow(speed, 0.6f) * Time.deltaTime;
                }
            }
        }
        //---------------------------------------------
        //-----------------nitro-----------------------
        if (useNitro)
        { 
            //니트로 스피드가 + a% 되는 식으로 구성
            speed = preSpeed * nitroSpeed / 100;
            nitroTime -= Time.deltaTime;
            nitro -= Time.deltaTime * 22;
        }
        //---------------------------------------------
        //--------------- touch control ---------------
        for (int i = 0; i < Input.touches.Length; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                isMouseDown = true;
                initialPos = Input.touches[i].position;
            }
            if (Input.touches[i].phase == TouchPhase.Ended)
            {
                isMouseDown = false;
                Calculate(Input.touches[i].position);
            }
        }
        //---------------------------------------------
        //---------------mouse control-------------- -
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
            if (lane < 1)
            {
                if (!isBlockLeft)
                {
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
            if (lane > -1)
            {
                if (!isBlockRight)
                {
                    lane--;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow) && !isKeyPressed)
        //{
        //    isKeyPressed = true;
        //    StartCoroutine(ComboUp());
        //}
        if (Input.GetKeyUp(KeyCode.UpArrow) && isKeyPressed)
        {
            isKeyPressed = false;
        }

        //---------------------------------------------
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, lane), Time.deltaTime * swipeSpeed);
        //---------------------UI----------------------
        orb.GetComponent<OrbFill>().Fill = nitro / 100;
        speedBar.fillAmount = speed / maxSpeed;
        if (speed < 0)
        {
            tempSpeed = 0;
            speedText.text = ((int)tempSpeed + "km");
        }
        else
        {
            speedText.text = ((int)speed+1 + "km");
        }
        hpBar.GetComponent<Image>().fillAmount = curHP / Character.maxHP;
        //---------------------------------------------

        //---------------------HP---------------------- 
        curHP = Mathf.Clamp(curHP, 0, character.HP);
        if (!useNitro)
        {
            if (transform.position.x < 1000)
            { //나중에 아이템 값만큼 곱하기
                curHP -= Time.deltaTime * 0.5f;
            }
            else
            {
                curHP -= (transform.position.x / 1000) * Time.deltaTime * 0.5f;
            }
            //-----------------Ray-----------------------
            SideRayCast();
            //-------------------------------------------
        }
        if (transform.position.x > tm * 1000){
            StartCoroutine(LifeUp(tm * 20));
            tm++;
        }
        //---------------------------------------------

        //-----------------Animations------------------
        sprintMultiplier = 1.0f + speed / 240;
        myAnimator.SetFloat("SprintMultiplier", sprintMultiplier);
        //---------------------------------------------

        //-------------------Camera--------------------
        offset.x = Mathf.Clamp(offset.x, -3.0f, -2.0f);
        camera.transform.position = transform.position + offset;
        if (Time.time <= endTime)
        {
            offset -= new Vector3(1.0f, 0, 0) * Time.deltaTime * ((20 + speed) / 200);
        }
        else
        {
            camMoving = false;
            offset = Vector3.SmoothDamp(offset, new Vector3(-2.0f, 1.5f, 0), ref velocity, 0.15f);
        }
        //---------------------------------------------

        if (nitro >= 100)
        {
            orb.GetComponent<OrbColor>().AccentColor = new Color32((byte)255, (byte)215, (byte)0, (byte)255);
        }
        //--------------Combo---------------------------
        if(comboTime > 0)
        {
            comboTime -= Time.deltaTime;
        }
        else
        {
            nowCombo = 0;
        }
        //----------------------------------------------

    }

    public bool isNitroShockwave = false;
    public IEnumerator NitroShockwave(bool isRevival)
    {
        isNitroShockwave = true;
        Collider[] cols = Physics.OverlapSphere(transform.position, bombSize + speed / 15, 1 << 12);
        if (cols != null)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].gameObject.GetComponent<Block>().StartCoroutine(cols[i].gameObject.GetComponent<Block>().SplitMesh(true));
            }
        }
        isNitroShockwave = false;
        if(!isRevival)
        StartCoroutine(AfterNitro());
        yield return null;
    }

    public void ContinueShockwave()
    {
        Debug.Log("Continue bombsize : " + bombSize);
        Collider[] cols = Physics.OverlapSphere(transform.position, bombSize, 1 << 12);
        if (cols != null)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].gameObject.GetComponent<Block>().StartCoroutine(cols[i].gameObject.GetComponent<Block>().SplitMesh(true));
            }
        }
    }

    void Raycast()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.right);
        RaycastHit hit;
        //space 의 2/3 지점 이내일때 피하면 부스트 따라서, ray는 space만큼 쏘면 피했을때 다른 블럭이 잡히지않음
        //Ray 거리는 최소노드길이(minDifficulty + minSpace)보다 짧아야함****************
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
                        if (lane < 1)
                        {
                            if (!isBlockLeft)
                            {
                                lane++;
                                moveLeft = true;
                            }
                        }
                    }
                    else
                    {
                        if (lane > -1)
                        {
                            if (!isBlockRight)
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
                        if (!useNitro)
                        {
                            UseNitro();
                        }
                    }
                    else
                    {
                        //위로 드래그
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
            combo = 0;
            if (speedRecovery != null)
            {
                StopCoroutine(speedRecovery);
            }
            if (speed < minSpeed) preSpeed = minSpeed;
            else preSpeed = speed;
            damagedSpeed = 1.0f;
            useNitro = true;
            StartCoroutine(scoreUI.GetComponent<Score>().FeverOn());
            soundManager.GetComponent<SoundManager>().PlayNitro();
            myAnimator.Play("Roll");
            nitroFX.SetActive(true);
            if (leftEnemyArm != null)
                leftEnemyArm.Surprise();
            if (rightEnemyArm != null)
                rightEnemyArm.Surprise();

            isBlockRight = false;
            isBlockLeft = false;
        }
    }

    //golem roll animation event
    public void NitroAnimEvent()
    {
        StartCoroutine(NitroShockwave(false));
    }

    public void SmashEvent()
    {
        float magnitude = speed / 2100.0f;
        StartCoroutine(cameraShake.Smash(.4f, magnitude));
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
        if (other.gameObject.layer != LayerMask.NameToLayer("Item") && !ghostMode)
        {
            soundManager.GetComponent<SoundManager>().PlayBox();
            if (!useNitro)
            {
                if (!isShield)
                {
                    myAnimator.Play("Damage");
                    preSpeed = speed;
                    speed = 0.0f;
                    nowCombo = 0;
                    float damage;
                    if (transform.position.x < 100) damage = 5 * (1 - curDEF / 100);
                    else damage = (int)(transform.position.x / 100) * (1 - curDEF / 100);
                    LifeDown(damage);
                    isStun = true;
                    speedRecovery = StartCoroutine(SpeedRecovery());
                }
                else
                {
                    myAnimator.Play("Damage");
                    preSpeed = speed;
                    speed = 0.0f;
                    speedRecovery = StartCoroutine(SpeedRecovery());
                    isShield = false;
                    shieldEffect.SetActive(false);
                }
            }
            else
            {
                combo++;
                StartCoroutine(comboM.GetComponent<ComboNitro>().NitroCombo(combo));
            }
        }
        else if(ghostMode)
        {
            combo++;
            StartCoroutine(comboM.GetComponent<ComboNitro>().NitroCombo(combo));
        }
    }

    public void GetShield()
    {
        isShield = true;
        shieldEffect.SetActive(true);
        Invoke("OffShield", 5.0f);
    }
    
    public void OffShield()
    {
        isShield = false;
        shieldEffect.SetActive(false);
    }

    public IEnumerator GetNitro()
    {
        if (!useNitro)
        {
            float nowNitro = nitro;
            nitro = 100.0f;
            isNitro = true;
            UseNitro();
            while (isNitro || useNitro)
            {
                yield return null;
            }
            nitro += nowNitro;
        }
    }

    public IEnumerator LifeUp(float num)
    {
        float temp = curHP + num;
        if (temp < character.HP)
        {
            while (curHP < temp)
            {
                curHP += num * Time.deltaTime;
                yield return null;
            }
        }else{
            while(curHP+1 < character.HP){
                curHP += num * Time.deltaTime;
                yield return null;
            }
        }
    }

    public void LifeDown(float num){
        curHP -= num;
    }
    //---------------------------------------------

    Coroutine speedRecovery;
    IEnumerator SpeedRecovery()
    {
        damagedSpeed = -8.0f;
        //경직 시간만큼 기다림
        yield return new WaitForSeconds(0.8f);

        if (!checkDead)
        {
            damagedSpeed = 1.0f;
            speed += minSpeed;
        }
        isStun = true;
    }

    void SideRayCast()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Block");
        Ray rayLeft = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.forward);
        Ray rayRight = new Ray(transform.position + new Vector3(0, 0.3f, 0), -transform.forward);
        RaycastHit hitLeft;
        RaycastHit hitRight;

        if (Physics.Raycast(rayLeft, out hitLeft, 1.0f, layerMask))
        {
            isBlockLeft = true;
        }
        else
        {
            isBlockLeft = false;
        }
        if (Physics.Raycast(rayRight, out hitRight, 1.0f, layerMask))
        {
            isBlockRight = true;
        }
        else
        {
            isBlockRight = false;
        }
    }

    IEnumerator AfterNitro()
    {
        yield return new WaitForSeconds(0.5f);
        nitroFX.SetActive(false);
        isNitro = false;
        nitro = 0;
        if(preSpeed < minAutoSpeed)
        {
            speed = minAutoSpeed;
        }
        else
        {
            speed = preSpeed;
        }
        nitroTime = 4.3f;
        useNitro = false;
        orb.GetComponent<OrbColor>().AccentColor = new Color32((byte)165, (byte)0, (byte)0, (byte)255);
    }

    void SmashCameraEffect()
    {
    }

    IEnumerator LeftFX()
    {
        justActionLeftFX.GetComponent<ParticleSystem>().Clear();
        justActionLeftFX.GetComponent<ParticleSystem>().Play();
        soundManager.GetComponent<SoundManager>().PlayBoost();
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator RightFX()
    {
        justActionRightFX.GetComponent<ParticleSystem>().Clear();
        justActionRightFX.GetComponent<ParticleSystem>().Play();
        soundManager.GetComponent<SoundManager>().PlayBoost();
        yield return new WaitForSeconds(0.6f);
    }

    public void Restart()
    {
        preSpeed = minSpeed;
        curHP = character.HP;
        checkDead = false;
        speed += minSpeed;
        damagedSpeed = 1.0f;
    }

    IEnumerator ComboUp()
    {
        comboTime = comboResetTime;
        nowCombo++;
        if (moveLeft)
        {
            StartCoroutine(comboR.GetComponent<ComboUI>().ComboUp(nowCombo));
        }
        else
        {
            StartCoroutine(comboL.GetComponent<ComboUI>().ComboUp(nowCombo));
        }
        yield return null;
    }

    IEnumerator HPSetting(){
        float temp = 0.0f;
        while(temp <= character.HP){
            temp++;
            hpBar.GetComponent<Image>().fillAmount = temp / Character.maxHP;
            yield return null;
        }
    }
}
