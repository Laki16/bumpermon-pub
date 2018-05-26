using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [Header("InGameUI")]
    public Text countdownText;

    public GameObject inGamePanel;
    public GameObject checkPanel;

    public GameObject homeBtn;
    public GameObject coins;
    public GameObject lives;

    public int coinTotal;
    public int coin;

    public Sprite livingheart;
    public Sprite deadHeart;


    [Header("OutGameUI")]
    public Text introduceText;
    public Text titleText;
    public Text recordText;

    public GameObject outGamePanel;
    public GameObject optionPanel;

    public GameObject startBtn;
    public GameObject optionBtn;
    public GameObject musicBtn;
    public GameObject devBtn;

    public Sprite optionDown;
    public Sprite optionUp;

    bool isDown;
    bool isMusicOn;
    bool isDev;
    bool isGameStart;


    [Header("GameOverUI")]
    public GameObject gameOverPanel;
    public GameObject highScore;
    public Text coinText;
    public Button continueBtn;
    public Button rankingBtn;
    public Button homeBtn2;
    public GameObject skullFX;
    

    [Header("System")]
    public GameObject blockController;
    public GameObject player;
    //public GameObject enemy;
    public Camera camera;
    float meterBetBlock;
    int timeLeft = 3;
    public EnemyArm leftEnemyArm;
    public EnemyArm rightEnemyArm;
    public GameObject SoundManager;

    [Header("Animation")]
    Animator myCameraAnimator;
    Animator myMenuAnimator;
    Animator myGameOverAnimator;

    // Use this for initialization
    void Start()
    {
        isDown = false;
        isMusicOn = true;
        isGameStart = false;

        myCameraAnimator = camera.GetComponent<Animator>();
        myMenuAnimator = optionPanel.GetComponent<Animator>();
        myGameOverAnimator = gameOverPanel.GetComponent<Animator>();

        LoadRecord();
        LoadSample();
    }

    void LoadRecord(){
        int score = 0;
        if(PlayerPrefs.HasKey("Score")){
            score = PlayerPrefs.GetInt("Score");
        }else{
            PlayerPrefs.SetInt("Score", score);
        }
        recordText.text = ("HIGH SCORE : " + score);
    }

    void LoadSample(){
        
    }

    void LateUpdate()
    {
        if(isDown) titleText.enabled = false;
        else titleText.enabled = true;
    }

    IEnumerator GameStart(){
        for (int i = 3; i > 0; i--){
            countdownText.text = (i + "");
            if (i == 1)
            {
                player.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "";
        //enemy.SetActive(true);
        meterBetBlock = player.transform.position.x;
        myCameraAnimator.enabled = false;
    }

    public void BtnOnStart()
    {
        isGameStart = true;
        isDown = false;
        isDev = false;
        myMenuAnimator.SetBool("Dev", false);
        myMenuAnimator.SetBool("Down", false);

        startBtn.SetActive(false);
        recordText.enabled = false;
        devBtn.SetActive(false);
        homeBtn.SetActive(true);
        titleText.gameObject.SetActive(false);
        introduceText.enabled = false;

        coins.SetActive(false);
        lives.SetActive(true);

        checkPanel.SetActive(true);
        inGamePanel.SetActive(true);
        blockController.SetActive(true);
        myCameraAnimator.Play("StartMoving");
        StartCoroutine(GameStart());
        SoundManager.GetComponent<SoundManager>().isLobbyEnd = true;
    }

    public void BtnOnOption()
    {
        if (isDown) //메뉴 내려와 있을 때 누르면
        {
            if (isGameStart)
            {
                Time.timeScale = 1;
            }
            myMenuAnimator.SetBool("Down", false);
            myMenuAnimator.SetBool("Dev", false);
            myMenuAnimator.SetBool("Restart", false);
            optionBtn.GetComponent<Image>().sprite = optionDown;
            isDown = false;
            isDev = false;
        }
        else
        {
            if (isGameStart)
            {
                Time.timeScale = 0;
            }
            myMenuAnimator.SetBool("Down", true);
            optionBtn.GetComponent<Image>().sprite = optionUp;
            isDown = true;
        }

    }

    public void BtnOnMusic()
    {
        if (isMusicOn)
        {
            //sound off
            musicBtn.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            isMusicOn = false;
        }
        else
        {
            //sound on
            musicBtn.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
            isMusicOn = true;
        }
    }

    public void BtnOnDeveloper()
    {
        if (isDev)
        {
            myMenuAnimator.SetBool("Dev", false);
            isDev = false;
        }
        else
        {
            myMenuAnimator.SetBool("Dev", true);
            isDev = true;
        }
    }
    
    public void BtnOnHome()
    {
        myMenuAnimator.SetBool("Restart", true);
    }

    public void BtnOnYes()
    {
        isDown = false;
        isDev = false;
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        SoundManager.GetComponent<SoundManager>().StopBGM();
        StartCoroutine(SoundManager.GetComponent<SoundManager>().BeginBGM());
    }
    
    public void BtnOnNo()
    {
        myMenuAnimator.SetBool("Restart", false);
    }

    public void BtnOnContinue()
    {
        myGameOverAnimator.SetBool("GameOver", false);
        SoundManager.GetComponent<SoundManager>().StopBGM();
        StartCoroutine(SoundManager.GetComponent<SoundManager>().Continue());
        optionBtn.GetComponent<Button>().interactable = true;
        skullFX.SetActive(false);
        //광고시스템 추가

        leftEnemyArm.Idle();
        rightEnemyArm.Idle();
        StartCoroutine(GameStart());

        //주변 블럭 날리기
        StartCoroutine(player.GetComponent<PlayerController>().NitroShockwave());

        player.GetComponent<Animator>().Play("Idle");
        player.GetComponent<PlayerController>().live = 1;
        player.GetComponent<PlayerController>().checkDead = false;
        player.GetComponent<PlayerController>().speed += player.GetComponent<PlayerController>().minSpeed;
        LiveUp();
    }

    public void BtnOnRanking()
    {
        //랭킹시스템 추가
    }

    public void LiveUp()
    {
        int heartNum = player.GetComponent<PlayerController>().live;
        string temp = "Live" + heartNum.ToString();
        GameObject heart = GameObject.Find(temp);
        heart.GetComponent<Image>().sprite = livingheart;
    }

    public void LiveDown()
    {
        Debug.Log("LiveDown");
        int heartNum = player.GetComponent<PlayerController>().live;
        string temp = "Live" + (heartNum + 1).ToString();
        GameObject heart = GameObject.Find(temp);
        heart.GetComponent<Image>().sprite = deadHeart;
    }

    public void GameOver()
    {
        optionBtn.GetComponent<Button>().interactable = false;
        int curScore = (int)player.transform.position.x + 20;
        int prevScore = PlayerPrefs.GetInt("Score");

        if(curScore >= prevScore){
            PlayerPrefs.SetInt("Score", curScore);
            PlayerPrefs.Save();
            highScore.SetActive(true);
        }else{
            highScore.SetActive(false);
        }

        myGameOverAnimator.SetBool("GameOver", true);
        SoundManager.GetComponent<SoundManager>().StopBGM();
        StartCoroutine(SoundManager.GetComponent<SoundManager>().GameOver());
        skullFX.SetActive(true);
    }
}
