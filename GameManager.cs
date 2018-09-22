using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GooglePlayGames;

public class GameManager : MonoBehaviour {

    [Header("InGameUI")]
    public Text countdownText;

    public GameObject inGamePanel;
    public GameObject checkPanel;

    public GameObject homeBtn;
    public GameObject coinUI;
    public GameObject gemUI;
    public GameObject scoreUI;

    public Sprite livingheart;
    public Sprite deadHeart;

    [Header("OutGameUI")]
    public Text introduceText;
    public Text titleText;
    public Text recordText;
    public Text totalCoins;
    public Text totalGems;

    public GameObject outGamePanel;
    public GameObject optionPanel;
    public GameObject characterPanel;
    public GameObject shopPanel;
    public GameObject btmPanel;
    public GameObject storePanel;

    public GameObject startBtn;
    public GameObject optionBtn;
    public GameObject musicBtn;
    public GameObject devBtn;
    public GameObject quitBtn;
    public GameObject rankingBtn;
    public GameObject SNS;

    public Sprite optionDown;
    public Sprite optionUp;
    public Sprite musicOn;
    public Sprite musicOff;

    bool isDown;
    bool isMusicOn;
    bool isDev;
    bool isGameStart;


    [Header("GameOverUI")]
    public GameObject gameOverPanel;
    public GameObject highScore;
    public Text coinText;
    public Button continueBtn;
    public Button homeBtn2;
    public GameObject skullFX;
    bool isContinueAvailable = true;
    public Text timeText;

    [Header("System")]
    public CharacterManager characterManager;
    public ShopManager shopManager;
    public GameObject blockController;
    public PlayGamesScript playGamesScript;
    public SpawnGrounds spawnGrounds;
    public GameObject player;
    public Camera camera;
    float meterBetBlock;
    int timeLeft = 3;
    public EnemyArm leftEnemyArm;
    public EnemyArm rightEnemyArm;
    public GameObject SoundManager;
    public GameObject lArm;
    public GameObject rArm;

    [Header("Animation")]
    Animator myCameraAnimator;
    Animator myMenuAnimator;
    Animator myGameOverAnimator;
    Animator myCharacterAnimator;
    Animator myShopAnimator;
    Animator myStoreAnimator;

    [Header("DB")]
    public int coin;
    public int gem;
    public int score;
    int prevCoins;
    int prevGems;
    float playTime;

    [Header("Ads")]
    public UnityAdsHelper unityAdsHelper;

    // Use this for initialization
    void Start()
    {
        isDown = false;
        isMusicOn = true;
        isGameStart = false;

        StartCoroutine(SoundManager.GetComponent<SoundManager>().BeginBGM());

        myCameraAnimator = camera.GetComponent<Animator>();
        myMenuAnimator = optionPanel.GetComponent<Animator>();
        myGameOverAnimator = gameOverPanel.GetComponent<Animator>();
        myCharacterAnimator = characterPanel.GetComponent<Animator>();
        myShopAnimator = shopPanel.GetComponent<Animator>();
        myStoreAnimator = storePanel.GetComponent<Animator>();

        LoadRecord();
    }

    public void CloudLoadData()
    {
        coin = CloudVariables.SystemValues[0];
        gem = CloudVariables.SystemValues[1];
        score = CloudVariables.SystemValues[2];

        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Gem", gem);
        PlayerPrefs.SetInt("Score", score);
        if (score == 0)
        {
            if (!PlayerPrefs.HasKey("Tutorial"))
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                SceneManager.LoadScene(1);
            }
        }

        LoadRecord();
        UpdateUI();
    }

    public void CloudSaveData()
    {
        score = PlayerPrefs.GetInt("Score");
        coin = PlayerPrefs.GetInt("Coin");
        gem = PlayerPrefs.GetInt("Gem");

        CloudVariables.SystemValues[0] = coin;
        CloudVariables.SystemValues[1] = gem;
        CloudVariables.SystemValues[2] = score;

        PlayGamesScript.Instance.SaveData();
    }

    void LoadRecord(){
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            coin = PlayerPrefs.GetInt("Coin");
            gem = PlayerPrefs.GetInt("Gem");
        }
        else
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.SetInt("Gem", 0);
        }

        recordText.text = ("HIGH SCORE : " + score);
        totalCoins.text = ("" + coin);
        totalGems.text = ("" + gem);
    }
    
    void UpdateUI() //사용x
    {
        totalCoins.text = CloudVariables.SystemValues[0].ToString();
        totalGems.text = CloudVariables.SystemValues[1].ToString();
        recordText.text = "HIGH SCORE : " + CloudVariables.SystemValues[2].ToString();
    }

    void LateUpdate()
    {
        if(isDown) titleText.enabled = false;
        else titleText.enabled = true;
    }

    IEnumerator GameStart()
    {
        //player의 능력치를 가져옴
        player.GetComponent<Character>().SetStatus();
        //item능력치 적용
        shopManager.ApplyEquipmentToCharacter();

        spawnGrounds.playerController = player.GetComponent<PlayerController>();
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
        meterBetBlock = player.transform.position.x;
        myCameraAnimator.enabled = false;
    }

    IEnumerator GameContinue()
    {
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "";
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
        btmPanel.SetActive(false);
        devBtn.SetActive(false);
        quitBtn.SetActive(false);
        homeBtn.SetActive(true);
        rankingBtn.SetActive(false);
        titleText.gameObject.SetActive(false);
        introduceText.enabled = false;
        SNS.SetActive(false);

        coinUI.SetActive(false);
        gemUI.SetActive(false);

        checkPanel.SetActive(true);
        inGamePanel.SetActive(true);
        blockController.SetActive(true);
        myCameraAnimator.Play("StartMoving");
        StartCoroutine(GameStart());
        //팔 로비 애니메이션으로 대체할것
        lArm.GetComponent<EnemyArm>().player = player;
        rArm.GetComponent<EnemyArm>().player = player;
        SoundManager.GetComponent<SoundManager>().isLobbyEnd = true;
        //DB
        coin = 0;
        gem = 0;

        Destroy(characterPanel);
        Destroy(shopPanel);
        Destroy(storePanel);
    }

    public void BtnOnOption()
    {
        if (isDown) //메뉴 내려와 있을 때 누르면
        {
            if (isGameStart)
            {
                Time.timeScale = 1;
            }
            scoreUI.SetActive(true);
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
            SoundManager.GetComponent<SoundManager>().Mute(true);
            musicBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            musicBtn.GetComponent<Image>().sprite = musicOff;
            isMusicOn = false;
        }
        else
        {
            //sound on
            SoundManager.GetComponent<SoundManager>().Mute(false);
            musicBtn.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
            musicBtn.GetComponent<Image>().sprite = musicOn;
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
    
    public void BtnOnMainScreen()
    {
        scoreUI.SetActive(false);
        myMenuAnimator.SetBool("Restart", true);
    }

    public void BtnOnHome()
    {
        PlayerPrefs.Save();
        CloudSaveData();
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
        scoreUI.SetActive(true);
        myMenuAnimator.SetBool("Restart", false);
    }

    public void BtnOnStore()
    {
        shopManager.UpdateShopUI();
        myStoreAnimator.SetBool("Up", true);
    }
    
    public void BtnOnStoreToMain()
    {
        myStoreAnimator.SetBool("Up", false);
        //update gold, gem
        coin = PlayerPrefs.GetInt("Coin");
        gem = PlayerPrefs.GetInt("Gem");
        totalCoins.text = coin.ToString();
        totalGems.text = gem.ToString();

        characterManager.currentCoin.text = coin.ToString();
        shopManager.UpdateShopUI();
    }

    public void BtnOnContinue()
    {
        isContinueAvailable = false;
        myGameOverAnimator.SetBool("GameOver", false);
        SoundManager.GetComponent<SoundManager>().StopBGM();
        StartCoroutine(SoundManager.GetComponent<SoundManager>().Continue());
        optionBtn.GetComponent<Button>().interactable = true;
        skullFX.SetActive(false);

        //광고시스템 추가
        Time.timeScale = 0;
        unityAdsHelper.ShowAd();

        leftEnemyArm.Idle();
        rightEnemyArm.Idle();
        StartCoroutine(GameContinue());

        //주변 블럭 날리기
        player.GetComponent<PlayerController>().ContinueShockwave();
        if(player.GetComponent<Animator>() == null)
        {
            player.GetComponentInChildren<Animator>().Play("Idle");
        }
        else
        {
            player.GetComponent<Animator>().Play("Idle");
        }
        player.GetComponent<PlayerController>().Restart();
    }

    public void GameOver()
    {

        if (!isContinueAvailable){
            continueBtn.GetComponent<Image>().sprite = deadHeart;
            continueBtn.interactable = false;
        }
        optionBtn.GetComponent<Button>().interactable = false;
        int curScore = scoreUI.GetComponent<Score>().score;
        int prevScore = PlayerPrefs.GetInt("Score");
        
        playTime = Time.time;
        int min = (int)playTime / 60;
        int sec = (int)playTime - min * 60;
        float res = playTime - min*60 - sec;
        res = (int)(res * 100);

        string m, s, r;
        if (min < 10)
        {
            m = "0";
            m += min.ToString();
        } else m = min.ToString();
        if (sec < 10)
        {
            s = "0";
            s += sec.ToString();
        } else s = sec.ToString();
        if (res < 10)
        {
            r = "0";
            r += res.ToString();
        }
        else r = res.ToString();
        timeText.text = ("" + m + "' " + s + "'' " + r);

        if (isContinueAvailable)
        {
            int pCoin = PlayerPrefs.GetInt("Coin");
            int pGem = PlayerPrefs.GetInt("Gem");
            pCoin += coin;
            pGem += gem;
            PlayerPrefs.SetInt("Coin", pCoin);
            PlayerPrefs.SetInt("Gem", pGem);

            prevCoins = coin;
            prevGems = gem;
        }
        else
        {
            int pCoin = PlayerPrefs.GetInt("Coin");
            int pGem = PlayerPrefs.GetInt("Gem");
            pCoin += (coin - prevCoins);
            pGem += (gem - prevGems);
            PlayerPrefs.SetInt("Coin", pCoin);
            PlayerPrefs.SetInt("Gem", pGem);
        }
        coinText.text = ("" + coin);

        if (curScore >= prevScore){
            PlayerPrefs.SetInt("Score", curScore);
            highScore.SetActive(true);
        }else{
            highScore.SetActive(false);
        }

        myGameOverAnimator.SetBool("GameOver", true);
        SoundManager.GetComponent<SoundManager>().StopBGM();
        StartCoroutine(SoundManager.GetComponent<SoundManager>().GameOver());
        skullFX.SetActive(true);
    }

    public void BtnOnCharacter()
    {
        myCharacterAnimator.SetBool("isOpen", true);
        characterManager.UpdateUI();
    }

    public void BtnOnShop()
    {
        shopManager.UpdateShopUI();
        myShopAnimator.SetBool("isOpen", true);
    }

    public void BtnOnMain()
    {
        myShopAnimator.SetBool("isOpen", false);
    }

    public void BtnOnQuit()
    {
        Application.Quit();
    }
}