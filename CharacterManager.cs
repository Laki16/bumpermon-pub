using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    [Header("System")]
    public GameManager gameManager;
    public GameObject currentCharacter;
    Character character;
    public GameObject characterPanel;
    public GameObject infoPanel;
    public ShopManager shopManager;
    public GameObject swapPanel;
    private bool isStatus;

    [Header("UI")]
    public Button golemBtn;
    public Button ghostBtn;
    public Button dragonBtn;
    public Button santaBtn;
    public Button skeletonBtn;
    public GameObject lvUpBtn;
    [Space(10)]
    public Image characterImage;
    public Sprite golemImage;
    public Sprite ghostImage;
    public Sprite dragonImage;
    public Sprite santaImage;
    public Sprite skeletonImage;
    public Sprite grayBtn;
    public Sprite greenBtn;
    public Text transText;

    [Space(10)]
    public GameObject golemPlayer;
    public GameObject ghostPlayer;
    public GameObject dragonPlayer;
    public GameObject santaPlayer;
    public GameObject skeletonPlayer;
    [Space(10)]
    public Image hpBar;
    public Image spdBar;
    public Image defBar;
    public Image strBar;
    public Image lukBar;
    //Frame Image
    [Space(10)]
    public Image golemFrame;
    public Image ghostFrame;
    public Image dragonFrame;
    public Image santaFrame;
    public Image skeletonFrame;
    public GameObject LockImage;
    Image selectedFrame;
    [Space(10)]
    public Text currentCoin;
    public Text monsterName;
    public Text requireGold;
    public GameObject coinImg;
    private int levelGold;
    private int curCoin;
    private int maxLevel;
    //IEnumerator
    float hp;
    float spd;
    float def;
    float str;
    float luk;
    bool isBuy;

    private void OnEnable()
    {
        selectedFrame = golemFrame;
        Invoke("UpdateUI", 0.5f);
        //UpdateUI();
	}

    public void BtnOnGolem(){
        characterImage.sprite = golemImage;
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();
        currentCharacter = golemPlayer;
        selectedFrame.color = new Color32(255, 255, 255, 255);
        selectedFrame = golemFrame;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnGhost(){
        characterImage.sprite = ghostImage;
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();
        currentCharacter = ghostPlayer;
        selectedFrame.color = new Color32(255, 255, 255, 255);
        selectedFrame = ghostFrame;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnDragon(){
        characterImage.sprite = dragonImage;
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();
        currentCharacter = dragonPlayer;
        selectedFrame.color = new Color32(255, 255, 255, 255);
        selectedFrame = dragonFrame;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnSanta()
    {
        characterImage.sprite = santaImage;
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();
        currentCharacter = santaPlayer;
        selectedFrame.color = new Color32(255, 255, 255, 255);
        selectedFrame = santaFrame;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnSkeleton()
    {
        characterImage.sprite = skeletonImage;
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();
        currentCharacter = skeletonPlayer;
        selectedFrame.color = new Color32(255, 255, 255, 255);
        selectedFrame = skeletonFrame;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnTrans()
    {
        //스탯창 -> 세부스탯
        if (!isStatus)
        {
            swapPanel.GetComponent<Animator>().Play("Equip");
            isStatus = true;
            transText.text = "Status";
        }
        else //세부스탯 -> 스탯창
        {
            swapPanel.GetComponent<Animator>().Play("Status");
            isStatus = false;
            transText.text = "Detail";
        }
    }

    public void BtnOnLvUp(){
        if (curCoin >= levelGold)
        {
            if(currentCharacter.GetComponent<Character>().Level > 0){
                infoPanel.GetComponent<Animator>().Play("UI_LvUp");
            }
            Debug.Log("Lv Up!");
            currentCharacter.GetComponent<Character>().LevelUp();
            curCoin -= levelGold;
            PlayerPrefs.SetInt("Coin", curCoin);
            PlayerPrefs.Save();

            //Cloud Saving
            CloudVariables.SystemValues[0] = curCoin;
            PlayGamesScript.Instance.SaveData();

            isBuy = true;
            //Lv Up Animation Play!
            UpdateUI();
        }else Debug.Log("Need more golds!");

    }

    public void BtnOnMain(){
        if(currentCharacter.GetComponent<Character>().Level < 1){
            currentCharacter = golemPlayer;
        }
        gameManager.player = currentCharacter;
        characterPanel.GetComponent<Animator>().SetBool("isOpen", false);
    }

    public void UpdateUI(){

        //골드 DB에서 가져올 것
        curCoin = PlayerPrefs.GetInt("Coin");
        currentCoin.text = curCoin.ToString();

        character = currentCharacter.GetComponent<Character>();
        shopManager.currentCharacter = character;

        string name;
        switch(character.MonsterIndex){
            case 1:
                name = "Golem";
                maxLevel = 9;
                break;
            case 2:
                name = "Ghost";
                maxLevel = 7;
                break;
            case 3:
                name = "Dragon";
                maxLevel = 5;
                break;
            case 4:
                name = "Santa";
                maxLevel = 7;
                break;
            case 5:
                name = "Skeleton";
                maxLevel = 9;
                break;
            default:
                name = "";
                break;
        }
        character.SetStatus();
        monsterName.text = "LV." + character.Level + " " + name;

        //Change Frame Color
        if (character.Level > 0)
        {
            selectedFrame.color = new Color32(255, 50, 0, 255);
            LockImage.SetActive(false);
            characterImage.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            LockImage.SetActive(true);
            characterImage.color = new Color32(100, 100, 100, 255);
        }

        if (!isBuy){
            StopAllCoroutines();
            hp = 0;
            spd = 0;
            def = 0;
            str = 0;
            luk = 0;
            StartCoroutine("BarProgress");
        }else{
            hpBar.fillAmount = character.HP / (float)Character.maxHP;
            spdBar.fillAmount = character.SPD / (float)Character.maxSPD;
            defBar.fillAmount = character.DEF / (float)Character.maxDEF;
            strBar.fillAmount = character.STR / (float)Character.maxSTR;
            lukBar.fillAmount = character.LUK / (float)Character.maxLUK;
        }

        levelGold = character.GetRequireGold(character.MonsterIndex, character.Level);
        if (levelGold > curCoin){
            lvUpBtn.GetComponent<Image>().sprite = grayBtn;
            lvUpBtn.GetComponent<Button>().interactable = false;
        }
        else{
            lvUpBtn.GetComponent<Image>().sprite = greenBtn;
            lvUpBtn.GetComponent<Button>().interactable = true;
        }

        if (character.Level == 0){
            requireGold.text = "  Buy\n" + levelGold;
            coinImg.SetActive(true);
        }else if(maxLevel <= character.Level){
            requireGold.text = "     MAX";
            lvUpBtn.GetComponent<Button>().interactable = false;
            lvUpBtn.GetComponent<Image>().sprite = grayBtn;
            coinImg.SetActive(false);
        }
        else{
            requireGold.text = "  Upgrade\n" + levelGold;
            coinImg.SetActive(true);
        }

        shopManager.UpdateUI();
        shopManager.UpdateStatus();

    }

    IEnumerator BarProgress(){
        int count = 0;
        while(true){
            if (count >= 5) break;
            if (hp <= (float)character.HP)
            {
                hp+= 1.0f;
                if (hp > (float)character.HP) count++;
            }
            if (spd <= (float)character.SPD)
            {
                spd+= 1.0f;
                if (spd > (float)character.SPD) count++;
            }
            if (def <= (float)character.DEF)
            {
                def+= 1.0f;
                if (def > (float)character.DEF) count++;
            }
            if (str <= (float)character.STR)
            {
                str+= 1.0f;
                if (str > (float)character.STR) count++;
            }
            if (luk <= (float)character.LUK)
            {
                luk+= 1.0f;
                if (luk > (float)character.LUK) count++;
            }

            hpBar.fillAmount = hp / (float)Character.maxHP;
            spdBar.fillAmount = spd / (float)Character.maxSPD;
            defBar.fillAmount = def / (float)Character.maxDEF;
            strBar.fillAmount = str / (float)Character.maxSTR;
            lukBar.fillAmount = luk / (float)Character.maxLUK;

            yield return null;
        }
    }



}
