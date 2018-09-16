﻿using System.Collections;
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
    public GameObject unequipBtn;

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
                DEF = 1 + (Level-1) * 0.3f;
                LUK = 0.5f + (Level-1) * 0.5f;
                break;
            case 1001: //솜사탕(N)
                HP = 0.5f + (Level-1) * 0.8f;
                bombSize = 0.7f + (Level-1) * 0.7f;
                break;
            case 1002: //아메리카노(N)
                nitroEarnSize = 0.5f + (Level-1) * 0.9f;
                nitroSpeed = 3 + (Level-1);
                break;
            //case 1003: //안경(N)
            //    bombSize = 1.5f + (Level-1) * 0.4f;
            //    break;
            case 1003: //항아리(N)
                SPD = -2;
                DEF = 0.5f + (Level-1) * 0.3f;
                STR = 1 + (Level-1) * 0.3f;
                break;
            case 1004: //양말(N)
                SPD = 1 + (Level-1)* 0.5f;
                LUK = 2 + (Level-1)*0.5f;
                nitroSpeed = -7;
                break;
            //case 1006: //열쇠고리(N)
            //    nitroSpeed = 3 + (Level-1)*0.7f;
            //    break;
            case 1005: //배낭(N)
                HP = -2;
                nitroEarnSize = 3 + (Level-1) * 0.4f;
                nitroSpeed = 2 + (Level-1) * 0.8f;
                break;
            case 1006: //래쉬가드(N)
                SPD = 2 + (Level-1);
                nitroSpeed = 1.5f + (Level-1) * 0.3f;
                break;
            //case 1009: //박스테이프(N)
            //    DEF = 1.5f + (Level-1) * 0.2f;
            //    LUK = 0.5f + (Level-1) * 0.5f;
            //    break;
            case 1007: //선인장(N)
                DEF = 1 + (Level-1);
                LUK = -2;
                break;
            case 1008: //운동화(N)
                SPD = 1.5f + (Level-1) * 0.3f;
                nitroSpeed = 2 + (Level-1) * 0.8f;
                break;
            case 1009: //하이힐(N)
                SPD = 1 + (Level-1);
                bombSize = 1 + (Level-1) * 0.3f;
                break;
            case 1010: //돌 조각(N)
                DEF = 0.5f + (Level-1);
                break;
            case 1011: //화분(N)
                HP = 1 + (Level-1) * 1;
                LUK = 2.5f + (Level-1);
                break;
            case 1012: //소화기(N)
                DEF = 0.5f + (Level-1) * 0.5f;
                STR = 1 + (Level-1) * 1.5f;
                break;

            ////////////////////////////////RARE/////////////////////////////////////
            case 1100: //덤벨(R)
                SPD = -1;
                STR = 1 + (Level-1) * 0.9f;
                DEF = 1.5f + (Level-1);
                break;
            case 1101: //티셔츠(R)
                HP = -2;
                DEF = 1 + (Level-1) * 1.3f;
                LUK = 4 + (Level-1) * 1.5f;
                break;
            case 1102: //나뭇잎(R)
                SPD = 1.5f + (Level-1) * 1.3f;
                DEF = -1.5f;
                nitroSpeed = 6 + (Level-1) * 2;
                break;
            //case 1104: //후라이팬(R)
            //    DEF = 3 + (Level-1) * 0.7f;
            //    LUK = 2.5f + (Level-1) * 1.5f;
            //    break;
            case 1103: //갑옷(R)
                SPD = -2;
                DEF = 3 + (Level-1);
                bombSize = 2 + (Level-1);
                break;
            case 1104: //축구공(R)
                SPD = -1.5f;
                STR = 2 + (Level-1);
                bombSize = 1 + (Level-1) * 1.7f;
                break;
            case 1105: //까마귀(R)
                SPD = 1.5f + (Level-1) * 1.4f;
                LUK = 3.5f + (Level-1) * 0.5f;
                break;
            case 1106: //안전모(R)
                SPD = -1.5f;
                DEF = 3.5f + (Level-1) * 1.3f;
                break;
            case 1107: //검(R)
                HP = 1 + (Level-1) * 1.5f;
                STR = 4 + (Level-1);
                break;
            case 1108: //틀니(R)
                SPD = 3 + (Level-1);
                LUK = 4 + (Level-1) * 1.5f;
                nitroEarnSize = 3 + (Level-1);
                break;

            ////////////////////////////////EPIC/////////////////////////////////////
            case 1200: //부적(E)
                LUK = 5 + (Level-1) * 3;
                break;
            case 1201: //드레스(E)
                HP = 3 + (Level-1) * 2.5f;
                SPD = 6 + (Level-1) * 2;
                DEF = -2;
                break;
            //case 1202: //키보드(E)
            //    STR = 3 + (Level-1);
            //    nitroSpeed = 8 + (Level-1) * 2;
            //    bombSize = 2 + (Level-1) * 3;
            //    break;
            case 1202: //오리발(E)
                SPD = 4 + (Level-1) * 2.5f;
                nitroSpeed = 13 + (Level-1);
                break;
            case 1203: //망치(E)
                nitroEarnSize = 2 + (Level-1);
                nitroSpeed = 7 + (Level-1) * 2;
                bombSize = 2 + (Level-1) * 1.5f;
                break;
            case 1204: //선물상자(E)

                HP = 0;
                SPD = 0;
                DEF = 0;
                STR = 0;
                LUK = 0;
                nitroEarnSize = 0;
                bombSize = 0;
                nitroSpeed = 0;

                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        HP = 5 + (Level - 1) * 2;
                        break;
                    case 1:
                        SPD = 5 + (Level - 1) * 2;
                        break;
                    case 2:
                        DEF = 5 + (Level - 1) * 2;
                        break;
                    case 3:
                        STR = 5 + (Level - 1) * 2;
                        break;
                    case 4:
                        LUK = 5 + (Level - 1) * 2;
                        break;
                    default:
                        break;
                }
                rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        nitroEarnSize = 10 + (Level - 1);
                        break;
                    case 1:
                        bombSize = 10 + (Level - 1);
                        break;
                    case 2:
                        nitroSpeed = 10 + (Level - 1);
                        break;
                    default:
                        break;
                }
                break;

            ////////////////////////////////LEGEND/////////////////////////////////////
            //case 1300: //절대반지(L)
            //    HP = 15 + (Level-1) * 4;
            //    DEF = 3 + (Level-1);
            //    nitroSpeed = 20 + (Level-1) * 5;
            //    bombSize = 7 + (Level-1) * 3;
            //    break;
            case 1300: //모래시계(L)
                HP = 17 + (Level-1) * 3;
                LUK = 13 + (Level-1) * 4;
                DEF = 4 + (Level-1) * 3;
                nitroEarnSize = 6 + (Level-1) * 2.5f;
                break;
            case 1301: //산삼(L)
                HP = 20 + (Level-1) * 2;
                STR = 9 + (Level-1);
                DEF = 5 + (Level-1) * 2;
                bombSize = 15 + (Level-1) * 3;
                break;
            case 1302: //왕관(L)
                SPD = 9 + (Level-1) * 3;
                LUK = 16 + (Level-1) * 2;
                STR = 5 + (Level-1) * 1.5f;
                nitroEarnSize = 4 + (Level-1) * 2;
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

    //public int GetUpgradeGold(int index, int (Level-1))
    //{
    //    switch (index)
    //    {
    //        case 1: return normalGold[(Level-1)];
    //        case 2: return rareGold[(Level-1)];
    //        case 3: return epicGold[(Level-1)];
    //        case 4: return legendGold[(Level-1)];
    //        default: return 0;
    //    }
    //}

    //public int GetSellGold(int index, int (Level-1))
    //{
    //    float gold = 0;
    //    switch (index)
    //    {
    //        case 1: gold = normalGold[(Level-1)-1]; break;
    //        case 2: gold =  rareGold[(Level-1)-1]; break;
    //        case 3: gold = epicGold[(Level-1)-1]; break;
    //        case 4: gold = legendGold[(Level-1)-1]; break;
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
        else
        {
            var newEquipItem = Instantiate(unequipBtn);
            newEquipItem.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
            shopManager.beforeEquipBtn = newEquipItem;
        }
    }
    
}
