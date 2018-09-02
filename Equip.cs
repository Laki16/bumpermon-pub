using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip : MonoBehaviour {

    [Header("System")]
    public int equippedCharacter;
    //public int index;
    private ShopManager shopManager;

    [Header("Stats")]
    public int EquipIndex;
    public int Level;
    public float HP;
    public float SPD;
    public float DEF;
    public float STR;
    public float LUK;
    public float nitroEarnSize;
    public float bombSize;
    public float nitroSpeed;
    public bool isEquipped = false;

    [Header("Button")]
    public GameObject infoBtn;
    public GameObject equipBtn;

    private void Start()
    {
        shopManager = GameObject.Find("Equip(Shop)Manager").GetComponent<ShopManager>();    
    }

    public void SetStatus()
    {
        switch (EquipIndex)
        {
            ////////////////////////////////NORMAL/////////////////////////////////////
            case 1000: //헬멧(N)
                DEF = 1 + Level * 0.3f;
                LUK = 0.5f + Level * 0.5f;
                break;
            case 1001: //솜사탕(N)
                HP = 0.5f + Level * 0.8f;
                bombSize = 0.7f + Level * 0.7f;
                break;
            case 1002: //아메리카노(N)
                nitroEarnSize = 0.5f + Level * 0.9f;
                nitroSpeed = 3 + Level;
                break;
            case 1003: //안경(N)
                bombSize = 1.5f + Level * 0.4f;
                break;
            case 1004: //항아리(N)
                SPD = -2;
                DEF = 0.5f + Level * 0.3f;
                STR = 1 + Level * 0.3f;
                break;
            case 1005: //양말(N)
                SPD = 1 + Level* 0.5f;
                LUK = 2 + Level*0.5f;
                nitroSpeed = -7;
                break;
            case 1006: //열쇠고리(N)
                nitroSpeed = 3 + Level*0.7f;
                break;
            case 1007: //배낭(N)
                HP = -2;
                nitroEarnSize = 3 + Level * 0.4f;
                nitroSpeed = 2 + Level * 0.8f;
                break;
            case 1008: //래쉬가드(N)
                SPD = 2 + Level;
                nitroSpeed = 1.5f + Level * 0.3f;
                break;
            case 1009: //박스테이프(N)
                DEF = 1.5f + Level * 0.2f;
                LUK = 0.5f + Level * 0.5f;
                break;
            case 1010: //선인장(N)
                DEF = 1 + Level;
                LUK = -2;
                break;
            case 1011: //운동화(N)
                SPD = 1.5f + Level * 0.3f;
                nitroSpeed = 2 + Level * 0.8f;
                break;
            case 1012: //하이힐(N)
                SPD = 1 + Level;
                bombSize = 1 + Level * 0.3f;
                break;
            case 1013: //돌 조각(N)
                DEF = 0.5f + Level;
                break;

            ////////////////////////////////RARE/////////////////////////////////////
            case 1100: //덤벨(R)
                SPD = -1;
                STR = 1 + Level * 0.9f;
                DEF = 1.5f + Level;
                break;
            case 1101: //티셔츠(R)
                HP = -2;
                DEF = 1 + Level * 1.3f;
                LUK = 4 + Level * 1.5f;
                break;
            case 1102: //나뭇잎(R)
                SPD = 1.5f + Level * 1.3f;
                DEF = -1.5f;
                nitroSpeed = 6 + Level * 2;
                break;
            case 1103: //화분(R)
                HP = 1 + Level * 2;
                LUK = 2.5f + Level;
                break;
            case 1104: //후라이팬(R)
                DEF = 3 + Level * 0.7f;
                LUK = 2.5f + Level * 1.5f;
                break;
            case 1105: //갑옷(R)
                SPD = -2;
                DEF = 3 + Level;
                bombSize = 2 + Level;
                break;
            case 1106: //축구공(R)
                SPD = -1.5f;
                STR = 2 + Level;
                bombSize = 1 + Level * 1.7f;
                break;
            case 1107: //까마귀(R)
                SPD = 1.5f + Level * 1.4f;
                LUK = 3.5f + Level * 0.5f;
                break;
            case 1108: //안전모(R)
                SPD = -1.5f;
                DEF = 3.5f + Level * 1.3f;
                break;
            case 1109: //검(R)
                HP = 1 + Level * 1.5f;
                STR = 4 + Level;
                break;
            case 1110: //소화기(R)
                DEF = 2 + Level;
                STR = 1 + Level * 1.5f;
                break;

            ////////////////////////////////EPIC/////////////////////////////////////
            case 1200: //부적(E)
                LUK = 5 + Level * 3;
                break;
            case 1201: //드레스(E)
                HP = 3 + Level * 2.5f;
                SPD = 6 + Level * 2;
                DEF = -2;
                break;
            case 1202: //키보드(E)
                STR = 3 + Level;
                nitroSpeed = 8 + Level * 2;
                bombSize = 2 + Level * 3;
                break;
            case 1203: //오리발(E)
                SPD = 4 + Level * 2.5f;
                nitroSpeed = 13 + Level;
                break;
            case 1204: //망치(E)
                nitroEarnSize = 2 + Level;
                nitroSpeed = 7 + Level * 2;
                bombSize = 2 + Level * 1.5f;
                break;
            case 1205: //선물상자(E)
                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        HP = 5 + Level*2;
                        break;
                    case 1:
                        SPD = 5 + Level * 2;
                        break;
                    case 2:
                        DEF = 5 + Level * 2;
                        break;
                    case 3:
                        STR = 5 + Level * 2;
                        break;
                    case 4:
                        LUK = 5 + Level * 2;
                        break;
                    default:
                        break;
                }
                rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        nitroEarnSize = 10 + Level;
                        break;
                    case 1:
                        bombSize = 10 + Level;
                        break;
                    case 2:
                        nitroSpeed = 10 + Level;
                        break;
                    default:
                        break;
                }
                break;
            case 1206: //틀니(E)
                SPD = 3 + Level * 2;
                LUK = 4 + Level * 3;
                nitroEarnSize = 3 + Level * 2;
                break;

            ////////////////////////////////LEGEND/////////////////////////////////////
            case 1300: //절대반지(L)
                HP = 15 + Level * 4;
                DEF = 3 + Level;
                nitroSpeed = 20 + Level * 5;
                bombSize = 7 + Level * 3;
                break;
            case 1301: //왕관(L)
                SPD = 9 + Level * 3;
                LUK = 16 + Level * 2;
                STR = 5 + Level * 1.5f;
                nitroEarnSize = 4 + Level * 2;
                break;
            case 1302: //모래시계(L)
                HP = 17 + Level * 3;
                LUK = 13 + Level * 4;
                DEF = 4 + Level * 3;
                nitroEarnSize = 6 + Level * 2.5f;
                break;
            case 1303: //산삼(L)
                HP = 20 + Level * 2;
                STR = 9 + Level;
                DEF = 5 + Level * 2;
                bombSize = 15 + Level * 3;
                break;

            ////////////////////////////////SPECIAL/////////////////////////////////////
            case 1037: //골렘(S)
                break;
            case 1038: //스켈레톤(S)
                break;
            case 1039: //산타(S)
                break;
            case 1040: //고스트(S)
                break;
            default:
                Debug.Log("unassigned equipment");
                break;
        }
    }

    //public int GetUpgradeGold(int index, int level)
    //{
    //    switch (index)
    //    {
    //        case 1: return normalGold[level];
    //        case 2: return rareGold[level];
    //        case 3: return epicGold[level];
    //        case 4: return legendGold[level];
    //        default: return 0;
    //    }
    //}

    //public int GetSellGold(int index, int level)
    //{
    //    float gold = 0;
    //    switch (index)
    //    {
    //        case 1: gold = normalGold[level-1]; break;
    //        case 2: gold =  rareGold[level-1]; break;
    //        case 3: gold = epicGold[level-1]; break;
    //        case 4: gold = legendGold[level-1]; break;
    //    }
    //    gold *= 0.15f;
    //    return (int)gold;
    //}

    public void UpdateFrame(int index)
    {
        transform.Find("Frame").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        equippedCharacter = index;
    }
    
    public void ResetFrame()
    {
        transform.Find("Frame").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void BtnOnEquippedItem()
    {
        shopManager.currentEquip = this;
        if(shopManager.beforeInfoBtn != null) Destroy(shopManager.beforeInfoBtn);
        if (shopManager.beforeEquipBtn != null) Destroy(shopManager.beforeEquipBtn);

        var newInfoBtn = Instantiate(infoBtn);
        newInfoBtn.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
        shopManager.beforeInfoBtn = newInfoBtn;
    }

    public void BtnOnItem()
    {
        shopManager.currentEquip = this;
        if (shopManager.beforeInfoBtn != null) Destroy(shopManager.beforeInfoBtn);
        if (shopManager.beforeEquipBtn != null) Destroy(shopManager.beforeEquipBtn);

        var newInfoBtn = Instantiate(infoBtn);
        newInfoBtn.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
        shopManager.beforeInfoBtn = newInfoBtn;

        if (!isEquipped)
        {
            var newEquipItem = Instantiate(equipBtn);
            newEquipItem.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
            shopManager.beforeEquipBtn = newEquipItem;
        }
    }
    
}
