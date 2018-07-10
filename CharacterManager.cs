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

    [Header("UI")]
    public Button golemBtn;
    public Button ghostBtn;
    public Button dragonBtn;
    public GameObject lvUpBtn;
    [Space(10)]
    public Image characterImage;
    public Sprite golemImage;
    public Sprite ghostImage;
    public Sprite dragonImage;
    public Sprite grayBtn;
    public Sprite greenBtn;
    [Space(10)]
    public GameObject golemPlayer;
    public GameObject ghostPlayer;
    public GameObject dragonPlayer;
    [Space(10)]
    public Image hpBar;
    public Image spdBar;
    public Image defBar;
    public Image strBar;
    public Image lukBar;
    [Space(10)]
    public Text currentGold;
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
        UpdateUI();
	}

    public void BtnOnGolem(){
        characterImage.sprite = golemImage;
        currentCharacter = golemPlayer;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnGhost(){
        characterImage.sprite = ghostImage;
        currentCharacter = ghostPlayer;
        isBuy = false;
        UpdateUI();
    }

    public void BtnOnDragon(){
        characterImage.sprite = dragonImage;
        currentCharacter = dragonPlayer;
        isBuy = false;
        UpdateUI();
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

    void UpdateUI(){
        //골드 DB에서 가져올 것
        curCoin = PlayerPrefs.GetInt("Coin");
        currentGold.text = "" + curCoin;

        character = currentCharacter.GetComponent<Character>();
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
            default:
                name = "";
                break;
        }
        character.SetStatus();
        monsterName.text = "LV." + character.Level + " " + name;

        if(!isBuy){
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
            requireGold.text = "  MAX";
            lvUpBtn.GetComponent<Button>().interactable = false;
            lvUpBtn.GetComponent<Image>().sprite = grayBtn;
            coinImg.SetActive(false);
        }
        else{
            requireGold.text = "  Upgrade\n" + levelGold;
            coinImg.SetActive(true);
        }
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
