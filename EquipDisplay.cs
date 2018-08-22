using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDisplay : MonoBehaviour {

    public Text itemTitle;
    public Color32 itemTitleColor;
    public Text itemDesc;
    public Text itemStat1;
    public Text itemStat2;
    public Text itemStat3;
    public Text itemStat4;
    public Text itemStat5;
    public Text itemStat6;

    public void Start()
    {
        itemTitle.text = string.Empty;
        itemDesc.text = string.Empty;
        itemStat1.text = string.Empty;
        itemStat2.text = string.Empty;
        itemStat3.text = string.Empty;
        itemStat4.text = string.Empty;
        itemStat5.text = string.Empty;
        itemStat6.text = string.Empty;
    }

    public void UpdateUI(int EquipIndex)
    {
        //설명 적기, 등급에 따라 배경색 업데이트
        switch (EquipIndex)
        {
            case 1001: //헬멧

                break;
            case 1002: //부적
                break;
            case 1003: //덤벨
                break;
            case 1004: //티셔츠
                break;
            case 1005: //솜사탕
                break;
            case 1006: //나뭇잎
                break;
            case 1007: //아메리카노
                break;
            case 1008: //화분
                break;
            case 1009: //안경
                break;
            case 1010: //절대반지
                break;
            case 1011: //항아리
                break;
            case 1012: //양말
                break;
            case 1013: //열쇠고리
                break;
            case 1014: //후라이팬
                break;
            case 1015: //왕관
                break;
            case 1016: //드레스
                break;
            case 1017: //배낭
                break;
            case 1018: //갑옷
                break;
            case 1019: //모래시계
                break;
            case 1020: //래쉬가드
                break;
            case 1021: //키보드
                break;
            case 1022: //박스테이프
                break;
            case 1023: //축구공
                break;
            case 1024: //까마귀
                break;
            case 1025: //안전모
                break;
            case 1026: //오리발
                break;
            case 1027: //검
                break;
            case 1028: //산삼
                break;
            case 1029: //선인장
                break;
            case 1030: //소화기
                break;
            case 1031: //망치
                break;
            case 1032: //운동화
                break;
            case 1033: //하이힐
                break;
            case 1034: //선물상자
                break;
            case 1035: //돌 조각
                break;
            case 1036: //틀니
                break;
            case 1037: //골렘
                break;
            case 1038: //스켈레톤
                break;
            case 1039: //산타
                break;
            case 1040: //고스트
                break;
            default: Debug.Log("unsigned equipment");
                break;
        }
    }
}
