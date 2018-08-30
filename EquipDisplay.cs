using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDisplay : MonoBehaviour {

    public Text itemTitle;
    //public Image title;
    //public Color title.color;
    public Text itemDesc;
    public Text itemStat1;
    public Text itemStat2;
    public Text itemStat3;
    public Text itemStat4;
    public Text itemStat5;
    public Text itemStat6;

    public void Start()
    {
        //title.color = Title.GetComponent<Image>().color;
        itemTitle.text = string.Empty;
        itemDesc.text = string.Empty;
        itemStat1.text = string.Empty;
        itemStat2.text = string.Empty;
        itemStat3.text = string.Empty;
        itemStat4.text = string.Empty;
        itemStat5.text = string.Empty;
        itemStat6.text = string.Empty;
    }

    public void ResetText()
    {
        itemStat1.text = string.Empty;
        itemStat2.text = string.Empty;
        itemStat3.text = string.Empty;
        itemStat4.text = string.Empty;
        itemStat5.text = string.Empty;
        itemStat6.text = string.Empty;
    }

    public void UpdateUI(int EquipIndex)
    {
        ResetText();
        //설명 적기, 등급에 따라 배경색 업데이트
        switch (EquipIndex)
        {
            ////////////////////////////////NORMAL/////////////////////////////////////
            case 1000: //헬멧(N)
                //title.color = new Color32(150,255,0,255);
                itemTitle.text = "헬멧";
                itemDesc.text = "어느 경우에든지 달릴 때는\n헬멧이 필요한 법이죠.";
                itemStat1.text = "방어력 +1";
                itemStat2.text = "행운 +0.5";
                break;
            case 1001: //솜사탕(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "솜사탕";
                itemDesc.text = "달콤한 솜사탕이네요.\n놀이공원에서 뛰는 느낌이 들어요.";
                itemStat1.text = "체력 +1.5";
                itemStat2.text = "폭발 반경 +2m";
                break;
            case 1002: //아메리카노(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "아메리카노";
                itemDesc.text = "시원한 아메리카노와 함께라면\n더 멀리 갈 수 있을까요?";
                itemStat1.text = "부스트 획득량 +2.5";
                itemStat2.text = "부스트 속도 +7%";
                break;
            case 1003: //안경(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "안경";
                itemDesc.text = "아쉽게도 최첨단 기능은\n들어있지 않답니다.";
                itemStat1.text = "폭발 반경 +2.5m";
                break;
            case 1004: //항아리(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "항아리";
                itemDesc.text = "망치와 함께 쓴다면...\n끝없이 올라가야 할지도 모릅니다.";
                itemStat1.text = "속도 -2";
                itemStat2.text = "방어력 +1";
                itemStat3.text = "힘 +1";
                break;
            case 1005: //양말(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "양말";
                itemDesc.text = "산타의 선물을 받으려면\n머리맡에 양말을 놔둬야겠죠?";
                itemStat1.text = "속도 +1";
                itemStat2.text = "행운 +2";
                itemStat3.text = "부스트 속도 -7%";
                break;
            case 1006: //열쇠고리(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "열쇠고리";
                itemDesc.text = "골렘 모양의 열쇠고리입니다.\n쓸모가 있을까요?";
                itemStat1.text = "부스트 속도 +3%";
                break;
            case 1007: //배낭(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "배낭";
                itemDesc.text = "여행을 떠날 때 든든한 조력자가 되줄\n겁니다.";
                itemStat1.text = "체력 -3";
                itemStat2.text = "속도 -1.5";
                itemStat3.text = "부스트 획득량 +5";
                itemStat4.text = "부스트 속도 +5%";
                break;
            case 1008: //래쉬가드(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "래쉬가드";
                itemDesc.text = "여기는 워터파크가 아닌데 말입니다.";
                itemStat1.text = "속도 +2";
                itemStat2.text = "부스트 속도 -5%";
                break;
            case 1009: //박스테이프(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "박스테이프";
                itemDesc.text = "인형 상자들을 포장하는데 쓰인 걸까요?";
                itemStat1.text = "방어력 +1.5";
                itemStat2.text = "행운 -2";
                break;
            case 1010: //선인장(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "선인장";
                itemDesc.text = "조심히 들고 뛰세요!\n가시가 날카로워요.";
                itemStat1.text = "방어력 +1";
                itemStat2.text = "행운 -2";
                break;
            case 1011: //운동화(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "운동화";
                itemDesc.text = "나이카 운동화네요!\n더 많이 뛸 수 있을 거에요.";
                itemStat1.text = "속도 +1.5";
                itemStat2.text = "부스트 속도 +2%";
                break;
            case 1012: //하이힐(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "하이힐";
                itemDesc.text = "균형 잡기는 힘들지만,\n폭발력이 있습니다.";
                itemStat1.text = "속도 +1";
                itemStat2.text = "폭발 반경 +1m";
                break;
            case 1013: //돌 조각(N)
                //title.color = new Color32(150, 255, 0, 255);
                itemTitle.text = "돌 조각";
                itemDesc.text = "골렘에서 떨어져나온 돌 조각입니다.";
                itemStat1.text = "방어력 +0.5";
                break;

            ////////////////////////////////RARE/////////////////////////////////////
            case 1100: //덤벨(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "덤벨";
                itemDesc.text = "이 무거운걸 들고 뛰신다구요?\n떨어트리지 않게 조심하세요.";
                itemStat1.text = "속도 -2";
                itemStat2.text = "힘 +1";
                itemStat3.text = "방어력 +1.5";
                break;
            case 1101: //티셔츠(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "개발자의 티셔츠";
                itemDesc.text = "킁킁. 앗 이게 무슨 냄새죠?\n좀 씻고 다니셔야겠어요.";
                itemStat1.text = "체력 -2";
                itemStat2.text = "방어력 +5";
                itemStat3.text = "행운 +2";
                break;
            case 1102: //나뭇잎(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "나뭇잎";
                itemDesc.text = "나뭇잎이 쓸모가 없다구요?\n작은 것이 강할 때가 있답니다.";
                itemStat1.text = "속도 +1.5";
                itemStat2.text = "방어력 -1.5";
                itemStat3.text = "부스트 속도 +15%";
                break;
            case 1103: //화분(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "화분";
                itemDesc.text = "아이 엠 그루트";
                itemStat1.text = "체력 +1";
                itemStat2.text = "방어력 +0.5";
                itemStat3.text = "행운 +2.5";
                itemStat4.text = "부스트 획득량 -2";
                break;
            case 1104: //후라이팬(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "후라이팬";
                itemDesc.text = "어딘지 모르게 단단해 보입니다.\n총알도 막을 수 있을 것 같은데요?";
                itemStat1.text = "속도 -1.5";
                itemStat2.text = "방어력 +3";
                itemStat3.text = "행운 +2.5";
                break;
            case 1105: //갑옷(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "갑옷";
                itemDesc.text = "좋은 재질로 만들어진 걸까요?\n가볍고 튼튼해 보이는 갑옷입니다.";
                itemStat1.text = "체력 -1.5";
                itemStat2.text = "속도 -2";
                itemStat3.text = "방어력 +5";
                itemStat4.text = "폭발 반경 +5m";
                break;
            case 1106: //축구공(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "축구공";
                itemDesc.text = "저도 공 차는거 정말 좋아하는데요\n제가 한번 차보겠습니다.";
                itemStat1.text = "속도 -1.5";
                itemStat2.text = "힘 +2";
                itemStat3.text = "폭발 반경 +1m";
                break;
            case 1107: //까마귀(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "까마귀";
                itemDesc.text = "까악 까악\n반짝반짝한거 좋아.";
                itemStat1.text = "속도 +1.5";
                itemStat2.text = "행운 +3.5";
                itemStat3.text = "부스트 획득량 -2";
                break;
            case 1108: //안전모(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "안전모";
                itemDesc.text = "안전모가 필요할지도 모릅니다.\n상자가 떨어질 수도 있으니까요!";
                itemStat1.text = "속도 -1.5";
                itemStat2.text = "방어력 +3.5";
                break;
            case 1109: //검(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "검";
                itemDesc.text = "아직 탈출하지 못한 인형들을 위해\n박스를 베려고 하시는 당신은...";
                itemStat1.text = "체력 -2";
                itemStat2.text = "힘 +5";
                break;
            case 1110: //소화기(R)
                //title.color = new Color32(0, 150, 255, 255);
                itemTitle.text = "소화기";
                itemDesc.text = "빨리 달리면 불이 붙을 수도 있어요!\n그럴 때 유용하답니다.";
                itemStat1.text = "체력 -1.5";
                itemStat2.text = "방어력 +2";
                itemStat3.text = "힘 +1";
                break;


            ////////////////////////////////EPIC/////////////////////////////////////
            case 1200: //부적(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "부적";
                itemDesc.text = "간혹 유령 인형이 출몰한다는 소문이\n있습니다.\n이 부적을 지니고 다니세요!";
                itemStat1.text = "행운 +3";
                break;
            case 1201: //드레스(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "드레스";
                itemDesc.text = "아주 아름다운 장식들로 가득합니다.\n하지만 여기 친구들에게는 영..";
                itemStat1.text = "체력 +3";
                itemStat2.text = "속도 +6";
                itemStat3.text = "방어력 -3";
                break;
            case 1202: //키보드(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "키보드";
                itemDesc.text = "잠시만요!\n샷건은 치지 말아주세요.";
                itemStat1.text = "힘 +4";
                itemStat2.text = "부스트 속도 +10%";
                itemStat3.text = "폭발 반경 +4m";
                break;
            case 1203: //오리발(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "오리발";
                itemDesc.text = "오리발을 신고 뛰어보셨나요?\n아주 빠르다고 합니다.";
                itemStat1.text = "속도 +4";
                itemStat2.text = "부스트 속도 +15%";
                break;
            case 1204: //망치(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "망치";
                itemDesc.text = "항아리와 같이 사용하면...\n높은 곳으로 갈 수 있을까요?";
                itemStat1.text = "부스트 획득량 +3";
                itemStat2.text = "부스트 속도 +5%";
                itemStat3.text = "폭발 반경 +3m";
                break;
            case 1205: //선물상자(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "선물상자";
                itemDesc.text = "산타가 준 선물상자로군요!\n어떤 스탯이 오를지 궁금한데요?";
                itemStat1.text = "랜덤 스탯 +5";
                itemStat2.text = "부스트 속도 +10%";
                break;
            case 1206: //틀니(E)
                //title.color = new Color32(200, 0, 255, 255);
                itemTitle.text = "틀니";
                itemDesc.text = "이를 악물고 뛰는데에는\n틀니가 제격이죠!";
                itemStat1.text = "속도 +3";
                itemStat2.text = "행운 +4";
                itemStat3.text = "부스트 획득량 +3";
                break;

            ////////////////////////////////LEGEND/////////////////////////////////////
            case 1300: //절대반지(L)
                //title.color = new Color32(255, 255, 0, 255);
                itemTitle.text = "절대반지";
                itemDesc.text = "용암에서 건져올린 반지입니다.\n어디선가 말소리가 들리는 듯 하는데..\n착용하면 어떻게 될까요?";
                itemStat1.text = "체력 +5";
                itemStat2.text = "속도 +10";
                itemStat3.text = "방어력 +3";
                itemStat4.text = "부스트 획득량 +6";
                itemStat5.text = "부스트 속도 +20%";
                itemStat6.text = "폭발 반경 +7m";
                break;
            case 1301: //왕관(L)
                //title.color = new Color32(255, 255, 0, 255);
                itemTitle.text = "왕관";
                itemDesc.text = "컨테이너 벨트 끝에는 궁전이\n있다고 합니다.\n그곳에 사는 왕이 쓰는 걸까요?";
                itemStat1.text = "체력 +10";
                itemStat2.text = "속도 +6";
                itemStat3.text = "방어력 +5";
                itemStat4.text = "행운 +16";
                itemStat5.text = "부스트 획득량 +3";
                itemStat6.text = "부스트 속도 +30%";
                break;
            case 1302: //모래시계(L)
                //title.color = new Color32(255, 255, 0, 255);
                itemTitle.text = "모래시계";
                itemDesc.text = "새로운 도전을 위해 모래시계를\n뒤집어보세요.";
                itemStat1.text = "체력 +7";
                itemStat2.text = "속도 +12";
                itemStat3.text = "힘 +7";
                itemStat4.text = "부스트 획득량 +6";
                itemStat5.text = "부스트 속도 +20%";
                itemStat6.text = "폭발 반경 +8m";
                break;
            case 1303: //산삼(L)
                //title.color = new Color32(255, 255, 0, 255);
                itemTitle.text = "산삼";
                itemDesc.text = "앗, 귀한 걸 발견하셨네요!\n이 산삼으로 말할 것 같으면..\n지리산에서 발견된 100년 된 천종산..";
                itemStat1.text = "체력 +20";
                itemStat2.text = "힘 +9";
                itemStat3.text = "방어력 +5";
                itemStat4.text = "행운 +10";
                itemStat5.text = "부스트 속도 15%";
                itemStat6.text = "폭발 반경 +15m";
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
            default: Debug.Log("unassigned equipment");
                break;
        }
    }
}
